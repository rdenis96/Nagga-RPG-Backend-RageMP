using BusinessLogic.Workers.Players;
using Domain.Enums.Characters;
using Domain.Models.Players;
using GTANetworkAPI;
using Helper;
using Helper.Characters.Constants;
using NaggaServer.Constants;
using NaggaServer.Helpers;
using NaggaServer.Models.Accounts;
using NaggaServer.Models.Delegates;
using System;
using System.Text.Json;

namespace NaggaServer.Controllers
{
    public class PlayerController : Script
    {
        private readonly PlayersWorker _playersWorker;
        private readonly RealtimeHelper _realtime;

        public static event OnPlayerSignedIn PlayerSignedIn;
        public static event OnPlayerSignedOut PlayerSignedOut;

        public PlayerController()
        {
            _playersWorker = new PlayersWorker();
            _realtime = RealtimeHelper.Instance;
        }
        [Command(Commands.Stats, Alias = Commands.StatsAlias)]
        public void Stats(Player player)
        {
            var playerInfo = _playersWorker.GetWrapperByUsername(player.Name);
            player.TriggerEvent("showStats", playerInfo);
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.TriggerEvent("onPlayerConnected");
        }

        [RemoteEvent("OnPlayerLoginAttempt")]
        public void OnPlayerLoginAttempt(Player player, string loginViewModel)
        {
            LoginViewModel loginModel = JsonSerializer.Deserialize<LoginViewModel>(loginViewModel);

            var encryptedPass = EncryptHelper.ComputeSha256Hash(loginModel.Password);

            var dbPlayer = _playersWorker.GetPlayerInfoByUsernameAndPassword(loginModel.Username, encryptedPass);
            if (dbPlayer != null)
            {
                var isLogged = RealtimeHelper.OnlinePlayers.ContainsKey(player.Id);
                if (isLogged == false)
                {
                    SetPlayerInfoOnSignIn(player, dbPlayer);
                    _playersWorker.Update(dbPlayer);
                    var positionToSpawn = new Vector3(dbPlayer.PositionWrapper.X, dbPlayer.PositionWrapper.Y, dbPlayer.PositionWrapper.Z);
                    NAPI.Player.SpawnPlayer(player, positionToSpawn);
                    player.TriggerEvent("onPlayerLoginResponse", dbPlayer);
                }
                else
                {
                    player.TriggerEvent("onPlayerLoginResponse", dbPlayer);
                    player.Kick("Userul este deja logat!");
                    return;
                }
            }

        }

        [RemoteEvent("OnPlayerRegisterAttempt")]
        public void OnPlayerRegisterAttempt(Player player, string registerViewModel)
        {
            RegisterViewModel registerModel = JsonSerializer.Deserialize<RegisterViewModel>(registerViewModel);
            var existsPlayer = _playersWorker.ExistsPlayer(registerModel.Username);
            if (existsPlayer)
            {
                player.TriggerEvent("onPlayerRegisterResponse", false);
            }
            else
            {
                var encryptedPass = EncryptHelper.ComputeSha256Hash(registerModel.Password);
                _playersWorker.Create(registerModel.Username, registerModel.Email, encryptedPass);
                player.TriggerEvent("onPlayerRegisterResponse", true);
            }
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Player player)
        {
            SetPlayerInfoOnSignOut(player);
        }

        [ServerEvent(Event.PlayerSpawn)]
        public void OnPlayerSpawn(Player player)
        {
            var dbPlayer = _playersWorker.GetWrapperByUsername(player.Name);
            SetPlayerInfoOnSpawn(player, dbPlayer);
        }

        private void SetPlayerInfoOnSignIn(Player player, PlayerInfoWrapper dbPlayer)
        {
            dbPlayer.LastActiveDate = DateTime.UtcNow;
            PlayerSignedIn?.Invoke(player, dbPlayer);
        }

        private void SetPlayerInfoOnSpawn(Player player, PlayerInfoWrapper dbPlayer)
        {
            var isLogged = RealtimeHelper.OnlinePlayers.ContainsKey(player.Id);
            if (isLogged)
            {
                player.Name = dbPlayer.Username;
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

            }

        }

        private void SetPlayerInfoOnSignOut(Player player)
        {
            PlayerSignedOut?.Invoke(player);
        }


    }
}
