using Domain.Enums.Admins;
using GTANetworkAPI;
using Helper.Chat.Enums;
using Helper.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaggaServer.Helpers
{
    public static class AdminHelper
    {
        public static void SendMessageToAdmins(string message, List<Player> admins, string sender = null)
        {
            //if (sender != null)
            //{
            //    Parallel.ForEach(admins, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (admin) =>
            //    {
            //        admin.SendChatMessage(message);
            //    });
            //}
            //else
            //{
            //    Parallel.ForEach(admins, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (admin) =>
            //    {
            //        admin.SendChatMessage(sender, message);
            //    });
            //}
            Parallel.ForEach(admins, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (admin) =>
            {
                admin.SendChatMessage(sender, message);
            });
        }

        public static void SendAdminCommandMessage(this Player player, Player target, string messageForAdmin, string messageForTarget, List<Player> adminsToReceive, string valueToDisplay = null)
        {
            var playerColor = ChatColors.Orange.GetDescription();
            var adminTagColor = ChatColors.Green.GetDescription();
            var chatColor = ChatColors.None.GetDescription();

            var message = $"{adminTagColor}[ADMIN] {playerColor}{player.Name}({player.Id}) {chatColor}{messageForAdmin} {playerColor}{target.Name}({target.Id})";
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                message = $"{message} {chatColor}cu valoarea {playerColor}{valueToDisplay}";
            }
            var adminMessage = message;

            message = $"{playerColor}{player.Name}({player.Id}) {chatColor}{messageForTarget}";
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                message = $"{message} cu valoarea {playerColor}{valueToDisplay}";
            }
            var targetMessage = message;

            SendMessageToAdmins(adminMessage, adminsToReceive);
            target.SendChatMessage(targetMessage);
        }

        public static void SendPersonalAdminCommandMessage(this Player player, string message, List<Player> adminsToReceive, string valueToDisplay = null)
        {
            var playerColor = ChatColors.Orange.GetDescription();
            var adminTagColor = ChatColors.Green.GetDescription();
            var chatColor = ChatColors.None.GetDescription();

            var messageToAdmin = $"{adminTagColor}[ADMIN] {playerColor}{player.Name}({player.Id}) {chatColor}{message}";
            if (string.IsNullOrWhiteSpace(valueToDisplay) == false)
            {
                messageToAdmin = $"{message} cu valoarea {playerColor}{valueToDisplay}";
            }

            SendMessageToAdmins(messageToAdmin, adminsToReceive);
        }

        public static string GetAdminLevelForChat(AdminLevels adminLevel)
        {
            switch (adminLevel)
            {
                case AdminLevels.Trial:
                case AdminLevels.Advanced:
                case AdminLevels.Coordonator:
                case AdminLevels.Owner:
                case AdminLevels.Developer:
                    return $"{nameof(adminLevel)}";

                case AdminLevels.SuperAdvanced:
                    return "Super Advanced";

                default:
                    return string.Empty;
            }
        }
    }
}