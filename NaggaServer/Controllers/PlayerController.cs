using BusinessLogic.Workers.Players;
using GTANetworkAPI;
using Helper;
using NaggaServer.Constants;
using NaggaServer.Models.Accounts;
using System.Text.Json;

namespace NaggaServer.Controllers
{
    public class PlayerController : Script
    {
        private readonly PlayersWorker _playersWorker;
        public PlayerController()
        {
            _playersWorker = new PlayersWorker();
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
            var dbPlayer = _playersWorker.GetPlayerInfo(loginModel.Username, encryptedPass);
            if (dbPlayer != null && dbPlayer.IsLogged == false)
            {
                player.Name = dbPlayer.Username;
            }
            player.TriggerEvent("onPlayerLoginResponse", dbPlayer);
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
                //insert into DB
                player.TriggerEvent("onPlayerRegisterResponse", true);
            }
        }
    }
}
