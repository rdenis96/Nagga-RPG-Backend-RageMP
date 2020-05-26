using Domain.Models.Players;
using GTANetworkAPI;
using NaggaServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaggaServer.Helpers
{
    public class RealtimeHelper
    {
        private static RealtimeHelper instance;
        private static object syncRoot = new object();


        public static Dictionary<int, PlayerInfoWrapper> OnlinePlayers;
        public static Dictionary<int, PlayerInfoWrapper> OnlineAdmins;

        public static List<Player> OnlinePlayersClient { get; set; }
        public static List<Player> OnlineAdminsClient { get; set; }

        private RealtimeHelper()
        {
            OnlinePlayers = new Dictionary<int, PlayerInfoWrapper>();
            OnlineAdmins = new Dictionary<int, PlayerInfoWrapper>();

            OnlinePlayersClient = new List<Player>();
            OnlineAdminsClient = new List<Player>();

            PlayerController.PlayerSignedIn += OnPlayerSignedIn;
            PlayerController.PlayerSignedOut += OnPlayerSignedOut;
        }

        public static RealtimeHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            var temp = new RealtimeHelper();

                            System.Threading.Thread.MemoryBarrier();
                            instance = temp;
                        }
                    }
                }

                return instance;
            }
        }

        public static void ExecuteActionOnPlayer(Player player, string target, Action<Player> func)
        {
            var isTargetOnline = false;
            var isTargetId = Int32.TryParse(target, out int targetId);
            if (isTargetId)
            {
                isTargetOnline = RealtimeHelper.OnlinePlayers.ContainsKey(targetId);
            }
            else
            {
                isTargetOnline = NAPI.Pools.GetAllPlayers().Any(x => x.Name.Equals(target));
            }

            if (isTargetOnline)
            {
                var targetModel = isTargetId ? NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Id == targetId) : NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Name == target);
                if (targetModel != null)
                {
                    func(targetModel);
                }
            }
            else
            {
                player.SendChatMessage("Jucatorul nu este online sau numele/id-ul e gresit!");
            }
        }

        private static void OnPlayerSignedIn(Player player, PlayerInfoWrapper dbPlayer)
        {
            OnlinePlayers.Add(player.Id, dbPlayer);
            OnlinePlayersClient.Add(player);

            if (dbPlayer.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
            {
                OnlineAdmins.Add(player.Id, dbPlayer);
                OnlineAdminsClient.Add(player);
            }
        }

        private static void OnPlayerSignedOut(Player player)
        {
            OnlinePlayers.Remove(player.Id);
            var playerToRemove = OnlinePlayersClient.FirstOrDefault(x => x.Id == player.Id);
            if (playerToRemove != null)
            {
                OnlinePlayersClient.Remove(playerToRemove);
            }

            var playerPair = OnlineAdmins.FirstOrDefault(x => x.Key == player.Id);
            if (playerPair.Value != null)
            {
                var dbPlayer = playerPair.Value;
                if (dbPlayer.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                {
                    OnlineAdmins.Remove(player.Id);
                    var adminToRemove = OnlineAdminsClient.FirstOrDefault(x => x.Id == player.Id);
                    if (adminToRemove != null)
                    {
                        OnlineAdminsClient.Remove(adminToRemove);
                    }
                }
            }
        }
    }
}
