using System;

namespace Domain.Models.Interfaces
{
    public interface IPlayerChatBase
    {
        DateTime MuteEndDate { get; set; }
        bool IsMuted { get; set; }
        string ChatColor { get; set; }

    }
}
