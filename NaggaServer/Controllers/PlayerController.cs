using BusinessLogic.Workers.Players;
using Domain.Enums.Characters;
using Domain.Models.Players;
using GTANetworkAPI;
using Heimdal.Backend.CompositionRoot;
using Helper;
using Helper.Characters.Constants;
using NaggaServer.Constants;
using NaggaServer.Constants.Chat;
using NaggaServer.Helpers;
using NaggaServer.Helpers.Statistics;
using NaggaServer.Models.Accounts;
using NaggaServer.Models.Delegates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NaggaServer.Controllers
{
    public class PlayerController : Script
    {
        private readonly CompositionRoot _compositionRoot;
        private readonly PlayersWorker _playersWorker;
        private readonly RealtimeHelper _realtimeHelper;

        public static event OnPlayerInfoUpdate PlayerInfoUpdate;

        public static event OnPlayerSignedIn PlayerSignedIn;

        public static event OnPlayerSignedOut PlayerSignedOut;

        public PlayerController()
        {
            _compositionRoot = CompositionRoot.Instance;
            _playersWorker = _compositionRoot.GetImplementation<PlayersWorker>();

            _realtimeHelper = RealtimeHelper.Instance;
        }

        #region Commands

        [Command(Commands.GetCoordonates, Alias = Commands.GetCoordonatesAlias)]
        public async Task GetCoordonates(Player player)
        {
            player.SendChatMessage($"Coordonates: X : {player.Position.X} | Y : {player.Position.Y} | Z : {player.Position.Z}");
        }

        [Command(Commands.Stats, Alias = Commands.StatsAlias)]
        [RemoteEvent("keypressStats")]
        public async Task Stats(Player player)
        {
            var playerInfo = _realtimeHelper.OnlinePlayers.GetValueOrDefault(player.Id);
            if (playerInfo != null)
            {
                var stats = PlayerStatisticsHelper.GetPlayerStatistics(player, playerInfo);
                player.TriggerEvent("showStats", stats);
            }
            else
            {
                player.SendChatMessage(ServerMessages.CommandError);
            }
        }

        #endregion Commands

        #region ServerEvents

        [ServerEvent(Event.PlayerConnected)]
        public async Task OnPlayerConnected(Player player)
        {
            NAPI.Entity.SetEntityTransparency(player, 0);
            player.Dimension = (uint)(player.Id + 1);
            player.TriggerEvent("onPlayerConnected");
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public async Task OnPlayerDisconnected(Player player, DisconnectionType disconnectionType, string reason)
        {
            SetPlayerInfoOnSignOut(player);
        }

        [ServerEvent(Event.PlayerSpawn)]
        public async Task OnPlayerSpawn(Player player)
        {
            var dbPlayer = _playersWorker.GetWrapperByUsername(player.Name);
            SetPlayerInfoOnSpawn(player, dbPlayer);
        }

        [ServerEvent(Event.ChatMessage)]
        public async Task OnPlayerSendChatMessage(Player player, string message)
        {
            var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
            if (playerInfo.Mute.IsMuted == true)
            {
                if (playerInfo.Mute.ExpirationTime > DateTime.UtcNow)
                {
                    player.SendChatMessage(PlayerMessages.Muted(playerInfo.Mute.Reason, playerInfo.Mute.ExpirationTime));
                }
                else
                {
                    await Task.Run(() =>
                    {
                        playerInfo.Mute.IsMuted = false;
                        playerInfo.Mute.Reason = string.Empty;
                        Interlocked.CompareExchange(ref PlayerInfoUpdate, null, null)?.Invoke(playerInfo);
                    });
                    SendChatMessage(player, message);
                }
            }
            else
            {
                SendChatMessage(player, message);
            }
        }

        #endregion ServerEvents

        #region RemoteEvent

        [RemoteEvent("OnPlayerLoginAttempt")]
        public async Task OnPlayerLoginAttempt(Player player, string loginViewModel)
        {
            LoginViewModel loginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginViewModel);

            var encryptedPass = EncryptHelper.ComputeSha512Hash(loginModel.Password);

            var dbPlayer = _playersWorker.GetPlayerInfoByUsernameAndPassword(loginModel.Username, encryptedPass);
            if (dbPlayer != null)
            {
                var isLogged = _realtimeHelper.OnlinePlayers.ContainsKey(player.Id);
                if (isLogged == false)
                {
                    SetPlayerInfoOnSignIn(player, dbPlayer);
                }
                else
                {
                    return;
                }
            }
            player.TriggerEvent("onPlayerLoginResponse", dbPlayer);
        }

        [RemoteEvent("OnPlayerRegisterAttempt")]
        public async Task OnPlayerRegisterAttempt(Player player, string registerViewModel)
        {
            RegisterViewModel registerModel = JsonConvert.DeserializeObject<RegisterViewModel>(registerViewModel);
            var existsPlayer = _playersWorker.ExistsPlayer(registerModel.Username);
            if (existsPlayer)
            {
                player.TriggerEvent("onPlayerRegisterResponse", false);
            }
            else
            {
                var encryptedPass = EncryptHelper.ComputeSha512Hash(registerModel.Password);
                _playersWorker.Create(registerModel.Username, registerModel.Email, encryptedPass);
                player.TriggerEvent("onPlayerRegisterResponse", true);
            }
        }

        #endregion RemoteEvent

        #region PrivateMethods

        private void SetPlayerInfoOnSignIn(Player player, PlayerInfoWrapper dbPlayer)
        {
            dbPlayer.LastActiveDate = DateTime.UtcNow;
            Interlocked.CompareExchange(ref PlayerSignedIn, null, null)?.Invoke(player, dbPlayer);
            Interlocked.CompareExchange(ref PlayerInfoUpdate, null, null)?.Invoke(dbPlayer);
        }

        private void SetPlayerInfoOnSpawn(Player player, PlayerInfoWrapper dbPlayer)
        {
            var isLogged = _realtimeHelper.OnlinePlayers.ContainsKey(player.Id);
            if (isLogged)
            {
                player.Name = dbPlayer.Username;
                player.Position = new Vector3(dbPlayer.PositionWrapper.X, dbPlayer.PositionWrapper.Y, dbPlayer.PositionWrapper.Z);
                player.Rotation = new Vector3(dbPlayer.RotationWrapper.X, dbPlayer.RotationWrapper.Y, dbPlayer.RotationWrapper.Z);
                switch (dbPlayer.SelectedSkin)
                {
                    case SkinType.Faction:
                        player.SetSkin((PedHash)dbPlayer.Faction.SkinId);
                        break;

                    case SkinType.Personal:
                        player.SetSkin(PredefinedSkins.CivilSkin);
                        break;
                }
                NAPI.Entity.SetEntityTransparency(player, 255);
            }
        }

        private void SetPlayerInfoOnSignOut(Player player)
        {
            Interlocked.CompareExchange(ref PlayerSignedOut, null, null)?.Invoke(player);
        }

        private void SendChatMessage(Player player, string message)
        {
            var onlinePlayers = NAPI.Pools.GetAllPlayers();
            Parallel.ForEach(onlinePlayers, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (clientPlayer) =>
            {
                clientPlayer.SendChatMessage(player.Name, message);
            });
        }

        #endregion PrivateMethods
    }
}