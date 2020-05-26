using GTANetworkAPI;
using Helper.Chat.Enums;
using System.Collections.Generic;
using System.Text;

namespace Helper.Chat
{
    public static class ChatHelper
    {
        public static void SendAdminCommandMessage(this Player player, Player target, string messageForAdmin, string messageForTarget, List<Player> adminsToReceive, string valueToDisplay = null)
        {
            var playerColor = GetChatColor(ChatColors.Orange);
            var adminTagColor = GetChatColor(ChatColors.Green);
            var chatColor = GetChatColor(ChatColors.None);

            StringBuilder composedMessage = new StringBuilder();
            composedMessage.Append(adminTagColor);
            composedMessage.Append("[ADMIN] ");
            composedMessage.Append(playerColor);
            composedMessage.Append(player.Name);
            composedMessage.Append(chatColor);
            composedMessage.Append(messageForAdmin);
            composedMessage.Append(playerColor);
            composedMessage.Append(target.Name);
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                composedMessage.Append(chatColor);
                composedMessage.Append(" cu valoarea ");
                composedMessage.Append(playerColor);
                composedMessage.Append(valueToDisplay);
            }
            var adminMessage = composedMessage.ToString();

            composedMessage.Clear();

            composedMessage.Append(playerColor);
            composedMessage.Append(player.Name);
            composedMessage.Append(chatColor);
            composedMessage.Append(messageForTarget);
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                composedMessage.Append(" cu valoarea ");
                composedMessage.Append(playerColor);
                composedMessage.Append(valueToDisplay);
            }
            var targetMessage = composedMessage.ToString();

            adminsToReceive.ForEach(x => x.SendChatMessage(adminMessage));
            target.SendChatMessage(targetMessage);
        }

        public static void SendPersonalAdminCommandMessage(this Player player, string message, List<Player> adminsToReceive, string valueToDisplay = null)
        {
            var playerColor = GetChatColor(ChatColors.Orange);
            var adminTagColor = GetChatColor(ChatColors.Green);
            var chatColor = GetChatColor(ChatColors.None);

            StringBuilder composedMessage = new StringBuilder();
            composedMessage.Append(adminTagColor);
            composedMessage.Append("[ADMIN] ");
            composedMessage.Append(playerColor);
            composedMessage.Append(player.Name);
            composedMessage.Append(chatColor);
            composedMessage.Append(message);
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                composedMessage.Append(" cu valoarea ");
                composedMessage.Append(playerColor);
                composedMessage.Append(valueToDisplay);
            }
            var adminMessage = composedMessage.ToString();

            adminsToReceive.ForEach(x => x.SendChatMessage(adminMessage));
        }

        private static string GetChatColor(ChatColors color)
        {
            switch (color)
            {
                case ChatColors.None:
                default:
                    return "~w~";
                case ChatColors.Red:
                    return "~r~";
                case ChatColors.Orange:
                    return "~o~";
                case ChatColors.Green:
                    return "~g~";
            }
        }
    }

}
