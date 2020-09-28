using Helper.Chat;
using Helper.Chat.Enums;
using System;

namespace NaggaServer.Constants.Chat
{
    public static class PlayerMessages
    {
        #region Messages With Value

        public const string SetHealth = " ti-a setat viata ";

        public const string SetArmor = " ti-a setat armura ";

        public const string CreateVehicle = " ti-a creat vehiculul ";

        #endregion

        #region Messages Without Value

        public const string Freeze = " te-a blocat pe loc!";
        public const string UnFreeze = " te-a deblocat de pe loc!";

        public const string UnMute = " ti-a scos mute-ul!";

        public const string Goto = " s-a teleportat la tine!";

        public const string Kick = " ti-a dat kick!";

        #endregion

        public static string Muted(string reason, DateTime expirationTime)
        {
            return $"Nu poti vorbi deoarece ai mute! Motiv: {reason} | Data de expirare: {expirationTime}";
        }

        public static string Mute(string adminName, string reason, DateTime expirationTime)
        {
            return $@"{ChatHelper.GetChatColor(ChatColors.Orange)}Administratorul {adminName} 
                                                {ChatHelper.GetChatColor(ChatColors.None)}ti-a dat mute cu motivul: 
                                                {ChatHelper.GetChatColor(ChatColors.Orange)}{reason} 
                                                {ChatHelper.GetChatColor(ChatColors.None)}pana la data de 
                                                {ChatHelper.GetChatColor(ChatColors.Orange)} {expirationTime}!";
        }
    }
}

