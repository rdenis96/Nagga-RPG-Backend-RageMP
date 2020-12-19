using BusinessLogic.Workers.Players;
using Domain.Models.Players;
using GTANetworkAPI;
using Heimdal.Backend.CompositionRoot;
using NaggaServer.Controllers;
using NaggaServer.Models.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace NaggaServer.Helpers
{
    public class RealtimeHelper
    {
        private readonly CompositionRoot _compositionRoot;
        private readonly PlayersWorker _playersWorker;
        private static RealtimeHelper _instance;
        private static object _syncRoot = new object();

        public Dictionary<int, PlayerInfoWrapper> OnlinePlayers;
        public Dictionary<int, PlayerInfoWrapper> OnlineAdmins;

        public Dictionary<int, Timer> PlayersPlayedTimeTimers { get; set; }

        public static event OnPlayerInfoUpdate PlayerInfoUpdate
        {
            add { PlayerController.PlayerInfoUpdate += value; }
            remove { PlayerController.PlayerInfoUpdate -= value; }
        }

        public static event OnPlayerSignedIn PlayerSignedIn
        {
            add { PlayerController.PlayerSignedIn += value; }
            remove { PlayerController.PlayerSignedIn -= value; }
        }

        public static event OnPlayerSignedOut PlayerSignedOut
        {
            add { PlayerController.PlayerSignedOut += value; }
            remove { PlayerController.PlayerSignedOut -= value; }
        }

        private RealtimeHelper()
        {
            PlayerInfoUpdate += UpdatePlayerInfo;
            PlayerSignedIn += OnPlayerSignedIn;
            PlayerSignedOut += OnPlayerSignedOut;

            _compositionRoot = CompositionRoot.Instance;

            _playersWorker = _compositionRoot.GetImplementation<PlayersWorker>();
            OnlinePlayers = new Dictionary<int, PlayerInfoWrapper>();
            OnlineAdmins = new Dictionary<int, PlayerInfoWrapper>();

            PlayersPlayedTimeTimers = new Dictionary<int, Timer>();
        }

        public static RealtimeHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            var temp = new RealtimeHelper();

                            System.Threading.Thread.MemoryBarrier();
                            _instance = temp;
                        }
                    }
                }

                return _instance;
            }
        }

        public static List<Player> GetAllPlayers(Func<Player, bool> filter = null)
        {
            var result = NAPI.Pools.GetAllPlayers();
            if (filter != null)
            {
                result = result.Where(filter).ToList();
            }
            return result;
        }

        public List<Player> GetAllOnlineClientAdmins()
        {
            var onlineAdmins = new List<Player>();

            NAPI.Pools.GetAllPlayers().ForEach(player =>
            {
                var playerInfo = GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > 0)
                {
                    onlineAdmins.Add(player);
                }
            });
            return onlineAdmins;
        }

        public Player GetPlayerById(int playerId)
        {
            var player = GetAllPlayers().FirstOrDefault(x => x.Id == playerId);
            return player;
        }

        public PlayerInfoWrapper GetOnlinePlayerInfo(int playerId)
        {
            var player = OnlinePlayers.FirstOrDefault(x => x.Key == playerId);
            return player.Value;
        }

        public async Task ExecuteActionOnPlayer(Player player, string target, Action<Player, PlayerInfoWrapper> func)
        {
            var isTargetOnline = false;
            var isTargetId = int.TryParse(target, out int targetId);
            if (isTargetId)
            {
                isTargetOnline = OnlinePlayers.ContainsKey(targetId);
            }
            else
            {
                isTargetOnline = GetAllPlayers().Any(x => x.Name.Equals(target));
            }

            if (isTargetOnline)
            {
                var targetModel = isTargetId ? GetAllPlayers().FirstOrDefault(x => x.Id == targetId) : GetAllPlayers().FirstOrDefault(x => x.Name == target);
                if (targetModel != null)
                {
                    var targetInfoModel = OnlinePlayers.FirstOrDefault(x => x.Key == targetModel.Id).Value;
                    func(targetModel, targetInfoModel);
                }
            }
            else
            {
                player.SendChatMessage("Jucatorul nu este online sau numele/id-ul e gresit!");
            }
        }

        public async Task ExecuteActionOnSelf(Player player, Action<PlayerInfoWrapper> func)
        {
            var playerInfo = OnlinePlayers.FirstOrDefault(x => x.Key == player.Id).Value;
            if (playerInfo != null)
            {
                func(playerInfo);
            }
        }

        public void OnPlayerSignedIn(Player player, PlayerInfoWrapper dbPlayer)
        {
            OnlinePlayers.Add(player.Id, dbPlayer);

            if (dbPlayer.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
            {
                OnlineAdmins.Add(player.Id, dbPlayer);
            }

            StartPlayedTimeCounting(dbPlayer);

            var positionToSpawn = new Vector3(dbPlayer.PositionWrapper.X, dbPlayer.PositionWrapper.Y, dbPlayer.PositionWrapper.Z);
            NAPI.Player.SpawnPlayer(player, positionToSpawn);
        }

        public void OnPlayerSignedOut(Player player)
        {
            var playerInfo = OnlinePlayers.FirstOrDefault(x => x.Key == player.Id).Value;
            StopPlayedTimeCounting(playerInfo.Id);
            _playersWorker.Update(playerInfo);

            OnlinePlayers.Remove(player.Id);

            var playerPair = OnlineAdmins.FirstOrDefault(x => x.Key == player.Id);
            if (playerPair.Value != null)
            {
                var dbPlayer = playerPair.Value;
                if (dbPlayer.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                {
                    OnlineAdmins.Remove(player.Id);
                }
            }
        }

        private void StartPlayedTimeCounting(PlayerInfoWrapper player)
        {
            Timer timer = new Timer();
            PlayersPlayedTimeTimers.Add(player.Id, timer);

            timer.Interval = 1000;
            timer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                player.TimePlayed += 1000;
            };
            timer.Start();
        }

        private void StopPlayedTimeCounting(int playerInfoId)
        {
            bool canGetTimer = PlayersPlayedTimeTimers.TryGetValue(playerInfoId, out Timer timer);
            if (canGetTimer)
            {
                timer.Stop();
            }
        }

        private void UpdatePlayerInfo(PlayerInfoWrapper playerInfo)
        {
            _playersWorker.Update(playerInfo);
        }
    }
}