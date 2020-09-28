using Helper.Chat;
using Helper.Chat.Enums;
using System;

namespace NaggaServer.Constants.Chat
{
    public static class AdminMessages
    {
        public readonly static string CommandNotAuthorized;

        #region Commands on players

        public const string SetHealth = " i-a setat viata jucatorului ";

        public const string SetArmor = " i-a setat armura jucatorului ";

        public const string CreateVehicle = " a creat vehicul pentru jucatorul ";

        #endregion

        public const string Freeze = " l-a blocat pe loc pe jucatorul ";
        public const string UnFreeze = " l-a deblocat de pe loc pe jucatorul ";

        public const string UnMute = " i-a scos mute-ul jucatorului ";

        public const string Goto = " s-a teleportat la jucatorul ";

        public const string Kick = " i-a dat kick jucatorului ";

        #region Commands on self

        public const string GoToCoordonates = " s-a teleportat la coordonatele ";

        #endregion

        #region Commands error messages

        public const string NotFreezed = "Jucatorul nu este blocat!";

        #endregion
        static AdminMessages()
        {
            CommandNotAuthorized = $"{ChatHelper.GetChatColor(ChatColors.Red)}Nu esti autorizat sa accesezi aceasta comanda!";
        }

        public static string Mute(string adminName, string playerName, string reason, DateTime expirationTime)
        {
            return $@"{ChatHelper.GetChatColor(ChatColors.Orange)}Administratorul {adminName} 
                                                {ChatHelper.GetChatColor(ChatColors.None)}i-a dat mute jucatorului 
                                                {ChatHelper.GetChatColor(ChatColors.Orange)}{playerName} 
                                                {ChatHelper.GetChatColor(ChatColors.None)}cu motivul 
                                                {ChatHelper.GetChatColor(ChatColors.Orange)}{reason} 
                                                {ChatHelper.GetChatColor(ChatColors.None)}pana la data de 
                                                {ChatHelper.GetChatColor(ChatColors.Orange)} {expirationTime}!";
        }
    }
}
