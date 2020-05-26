using GTANetworkAPI;
using Helper.Chat;
using Helper.Chat.Constants;
using NaggaServer.Constants;
using NaggaServer.Helpers;
using System;

namespace NaggaServer.Controllers
{
    public class AdminController : Script
    {
        public AdminController()
        {
        }

        [Command(Commands.Goto, Alias = Commands.GotoAlias, GreedyArg = true)]
        public void GoTo(Player player, string position)
        {
            var canGetValue = RealtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                if (playerInfo.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                {
                    var extractCoord = position.Split(",");
                    var areCoord = extractCoord.Length == 3;
                    if (areCoord)
                    {
                        bool canParseX = float.TryParse(extractCoord[0], out float posX);
                        bool canParseY = float.TryParse(extractCoord[1], out float posY);
                        bool canParseZ = float.TryParse(extractCoord[2], out float posZ);

                        if (canParseX && canParseY && canParseZ)
                        {
                            player.Position = new Vector3(posX, posY, posZ);

                            string valueToDisplay = string.Join(",", posX, posY, posZ);
                            player.SendPersonalAdminCommandMessage(AdminMessages.GoTo, RealtimeHelper.OnlineAdminsClient, valueToDisplay);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
        }

        [Command(Commands.SetTempSkin, Alias = Commands.SetTempSkinAlias)]
        public void SetTempSkin(Player player, int val)
        {
            var canGetValue = RealtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                    {
                        player.SetSkin((PedHash)val);
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

        [Command(Commands.SetHealth, Alias = Commands.SetHealthAlias)]
        public void SetHealth(Player player, string target, int health)
        {
            var canGetValue = RealtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                    {
                        RealtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer) =>
                        {
                            targetPlayer.Health = health;
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.SetHealth, PlayerMessages.SetHealth, RealtimeHelper.OnlineAdminsClient, health.ToString());
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

        [Command(Commands.SetArmor, Alias = Commands.SetArmorAlias)]
        public void SetArmor(Player player, string target, int armor)
        {
            var canGetValue = RealtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > Domain.Enums.Admins.AdminLevels.None)
                    {
                        RealtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer) =>
                        {
                            targetPlayer.Armor = armor;
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.SetArmor, PlayerMessages.SetArmor, RealtimeHelper.OnlineAdminsClient, armor.ToString());
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

    }
}

