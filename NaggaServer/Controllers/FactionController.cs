using BusinessLogic.Workers.Factions;
using GTANetworkAPI;
using NaggaServer.Constants;
using NaggaServer.Constants.Chat;
using NaggaServer.Helpers;
using System;

namespace NaggaServer.Controllers
{
    public class FactionController : Script
    {
        private readonly RealtimeHelper _realtimeHelper;
        private readonly FactionInfosWorker _factionInfosWorker;
        public FactionController()
        {
            _realtimeHelper = RealtimeHelper.Instance;
            _factionInfosWorker = new FactionInfosWorker();
        }

        #region Commands
        [Command(Commands.MakeLeader, Alias = Commands.MakeLeaderAlias)]
        public void MakeLeader(Player player, string target, string faction)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel >= Domain.Enums.Admins.AdminLevels.Coordonator)
                    {
                        _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetInfoPlayer) =>
                        {
                            _factionInfosWorker.SetFaction(targetInfoPlayer.Faction, faction);
                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.SetHealth, PlayerMessages.SetHealth, onlineAdmins);
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
