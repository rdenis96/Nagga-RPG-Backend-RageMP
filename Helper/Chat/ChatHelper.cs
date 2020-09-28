using Helper.Chat.Enums;

namespace Helper.Chat
{
    public static class ChatHelper
    {
        public static string GetChatColor(ChatColors color)
        {
            switch (color)
            {
                case ChatColors.None:
                default:
                    return "!{FFFFFF}";
                case ChatColors.Red:
                    return "!{FF0000}";
                case ChatColors.Orange:
                    return "!{#FFA500}";
                case ChatColors.Green:
                    return "!{#00FF00}";
                case ChatColors.ChatAdmins:
                    return "!{#009BFF}";
            }
        }
    }

}
