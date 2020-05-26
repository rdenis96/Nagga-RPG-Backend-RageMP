using System;

namespace Domain.Repositories.Players
{
    public interface IPlayerChatBase
    {
        DateTime MuteEndDate { get; set; }
        bool IsMuted { get; set; }

    }
}
