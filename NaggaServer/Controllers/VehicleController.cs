using GTANetworkAPI;
using NaggaServer.Constants;
using NaggaServer.Constants.Chat;
using NaggaServer.Helpers;
using System;

namespace NaggaServer.Controllers
{
    public class VehicleController : Script
    {
        private readonly RealtimeHelper _realtimeHelper;
        public VehicleController()
        {
            _realtimeHelper = RealtimeHelper.Instance;
        }

        #region Commands
        [Command(Commands.CreateVehicle, Alias = Commands.CreateVehicleAlias)]
        public void CreateVehicle(Player player, string target)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel >= Domain.Enums.Admins.AdminLevels.Trial)
                    {
                        _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetInfoPlayer) =>
                        {
                            var vehiclePosition = new Vector3(player.Position.X, player.Position.Y + 10, player.Position.Z);
                            var vehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Infernus2, vehiclePosition, 0, 141, 122, "B96DFR");
                            NAPI.Player.SetPlayerIntoVehicle(targetPlayer, vehicle, 0);
                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.CreateVehicle, PlayerMessages.CreateVehicle, onlineAdmins, vehicle.DisplayName);
                        });
                    }
                    else
                    {
                        player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                    }
                }
                catch (Exception)
                {
                    player.SendChatMessage(ServerMessages.CommandError);
                }
            }
        }
        #endregion
    }

}
