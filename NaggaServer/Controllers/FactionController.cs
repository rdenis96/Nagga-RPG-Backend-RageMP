using BusinessLogic.Workers.Factions;
using GTANetworkAPI;
using Heimdal.Backend.CompositionRoot;
using NaggaServer.Constants;
using NaggaServer.Constants.Chat;
using NaggaServer.Helpers;
using System;
using System.Threading.Tasks;

namespace NaggaServer.Controllers
{
    public class FactionController : Script
    {
        private readonly CompositionRoot _compositionRoot;
        private readonly RealtimeHelper _realtimeHelper;
        private readonly FactionInfosWorker _factionInfosWorker;

        public FactionController()
        {
            _compositionRoot = CompositionRoot.Instance;
            _realtimeHelper = RealtimeHelper.Instance;
            _factionInfosWorker = _compositionRoot.GetImplementation<FactionInfosWorker>();
        }

        #region Commands

        [Command(Commands.MakeLeader, Alias = Commands.MakeLeaderAlias)]
        public async Task MakeLeader(Player player, string target, string faction)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel >= Domain.Enums.Admins.AdminLevels.Coordonator)
                    {
                        await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetInfoPlayer) =>
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

        #endregion Commands
    }
}