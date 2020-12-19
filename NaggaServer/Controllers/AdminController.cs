using Domain.Enums.Admins;
using GTANetworkAPI;
using Helper.Chat.Enums;
using Helper.Common;
using NaggaServer.Constants;
using NaggaServer.Constants.Chat;
using NaggaServer.Helpers;
using NaggaServer.Models.Delegates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NaggaServer.Controllers
{
    public class AdminController : Script
    {
        private readonly RealtimeHelper _realtimeHelper;

        public event OnPlayerInfoUpdate PlayerInfoUpdate;

        public AdminController()
        {
            _realtimeHelper = RealtimeHelper.Instance;
        }

        [Command(Commands.GotoCoordonates, Alias = Commands.GotoCoordonatesAlias, GreedyArg = true)]
        public async Task GotoCoordonates(Player player, string position)
        {
            await _realtimeHelper.ExecuteActionOnSelf(player, (playerInfo) =>
            {
                if (playerInfo.Admin.AdminLevel > AdminLevels.None)
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
                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendPersonalAdminCommandMessage(AdminMessages.GoToCoordonates, onlineAdmins, valueToDisplay);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(ServerMessages.CommandException);
                    }
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            });
        }

        [Command(Commands.SetTempSkin, Alias = Commands.SetTempSkinAlias)]
        public async Task SetTempSkin(Player player, int val)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > AdminLevels.None)
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
        public async Task SetHealth(Player player, string target, int health)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > AdminLevels.None)
                    {
                        await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetInfoPlayer) =>
                        {
                            targetPlayer.Health = health;
                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.SetHealth, PlayerMessages.SetHealth, onlineAdmins, health.ToString());
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
        public async Task SetArmor(Player player, string target, int armor)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                try
                {
                    if (playerInfo.Admin.AdminLevel > AdminLevels.None)
                    {
                        await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetInfoPlayer) =>
                        {
                            targetPlayer.Armor = armor;
                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.SetArmor, PlayerMessages.SetArmor, onlineAdmins, armor.ToString());
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

        [Command(Commands.Fly, Alias = Commands.FlyAlias)]
        public async Task Fly(Player player)
        {
            var canGetValue = _realtimeHelper.OnlinePlayers.TryGetValue(player.Id, out var playerInfo);
            if (canGetValue)
            {
                if (playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    player.TriggerEvent("activateNoClip");
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
        }

        [Command(Commands.AdminChat, Alias = Commands.AdminChatAlias, GreedyArg = true)]
        public async Task AdminChat(Player player, string message)
        {
            await _realtimeHelper.ExecuteActionOnSelf(player, (playerInfo) =>
            {
                if (playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    var adminMessage = $"{ChatColors.None.GetDescription()}{message}";
                    var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                    var sender = $"{ChatColors.ChatAdmins.GetDescription()}[{AdminHelper.GetAdminLevelForChat(playerInfo.Admin.AdminLevel)}] {player.Name}";
                    AdminHelper.SendMessageToAdmins(adminMessage, onlineAdmins, sender);
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            });
        }

        [Command(Commands.Freeze, Alias = Commands.FreezeAlias, Description = "/freeze <id/name>")]
        public async Task Freeze(Player player, string target)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        targetPlayer.SetData("isFreezed", true);
                        targetPlayer.TriggerEvent("freeze", true);

                        var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                        player.SendAdminCommandMessage(targetPlayer, AdminMessages.Freeze, PlayerMessages.Freeze, onlineAdmins);
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }

        [Command(Commands.UnFreeze, Alias = Commands.UnFreezeAlias, Description = "/unfreeze <id/name>")]
        public async Task UnFreeze(Player player, string target)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        if (targetPlayer.GetData<bool>("isFreezed"))
                        {
                            targetPlayer.SetData("isFreezed", false);
                            targetPlayer.TriggerEvent("freeze", false);

                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.UnFreeze, PlayerMessages.UnFreeze, onlineAdmins);
                        }
                        else
                        {
                            player.SendChatMessage(AdminMessages.NotFreezed);
                        }
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }

        [Command(Commands.Mute, Alias = Commands.MuteAlias, GreedyArg = true)]
        public async Task Mute(Player player, string target, int minutes, string reason)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        targetPlayerInfo.Mute.IsMuted = true;
                        targetPlayerInfo.Mute.ExpirationTime = DateTime.UtcNow.AddMinutes(minutes);
                        targetPlayerInfo.Mute.Reason = reason;
                        Interlocked.CompareExchange(ref PlayerInfoUpdate, null, null)?.Invoke(targetPlayerInfo);

                        var playerMessage = PlayerMessages.Mute(player.Name, reason, targetPlayerInfo.Mute.ExpirationTime);
                        var adminMessage = AdminMessages.Mute(player.Name, targetPlayer.Name, reason, targetPlayerInfo.Mute.ExpirationTime);
                        var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                        AdminHelper.SendMessageToAdmins(adminMessage, onlineAdmins);

                        player.SendAdminCommandMessage(targetPlayer, adminMessage, playerMessage, onlineAdmins);
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }

        [Command(Commands.UnMute, Alias = Commands.UnMuteAlias, GreedyArg = true)]
        public async Task UnMute(Player player, string target)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        if (targetPlayerInfo.Mute.IsMuted == true)
                        {
                            targetPlayerInfo.Mute.IsMuted = false;
                            targetPlayerInfo.Mute.ExpirationTime = DateTime.UtcNow;
                            targetPlayerInfo.Mute.Reason = string.Empty;
                            Interlocked.CompareExchange(ref PlayerInfoUpdate, null, null)?.Invoke(targetPlayerInfo);

                            var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                            player.SendAdminCommandMessage(targetPlayer, AdminMessages.UnMute, PlayerMessages.UnMute, onlineAdmins);
                        }
                        else
                        {
                            player.SendChatMessage($"Jucatorul {ChatColors.Orange.GetDescription()}{targetPlayer} {ChatColors.None.GetDescription()}nu are mute!");
                        }
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }

        [Command(Commands.Goto, Alias = Commands.GotoAlias)]
        public async Task Goto(Player player, string target)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        player.Position = new Vector3(targetPlayer.Position.X, targetPlayer.Position.Y, targetPlayer.Position.Z);
                        var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                        player.SendAdminCommandMessage(targetPlayer, AdminMessages.Goto, PlayerMessages.Goto, onlineAdmins);
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }

        [Command(Commands.Kick, Alias = Commands.KickAlias)]
        public async Task Kick(Player player, string target)
        {
            try
            {
                var playerInfo = _realtimeHelper.GetOnlinePlayerInfo(player.Id);
                if (playerInfo != null && playerInfo.Admin.AdminLevel > AdminLevels.None)
                {
                    await _realtimeHelper.ExecuteActionOnPlayer(player, target, (targetPlayer, targetPlayerInfo) =>
                    {
                        var onlineAdmins = _realtimeHelper.GetAllOnlineClientAdmins();
                        player.SendAdminCommandMessage(targetPlayer, AdminMessages.Kick, PlayerMessages.Kick, onlineAdmins);

                        player.Kick();
                    });
                }
                else
                {
                    player.SendChatMessage(AdminMessages.CommandNotAuthorized);
                }
            }
            catch
            {
                player.SendChatMessage(ServerMessages.CommandException);
            }
        }
    }
}