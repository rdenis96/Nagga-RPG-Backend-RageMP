using Domain.Models.Interfaces;
using System;

namespace Domain.Models.Factions
{
    public class FactionInfo : IPlayerChatBase, IEquatable<FactionInfo>
    {
        public Enums.Factions.Factions FactionId { get; set; }
        public int Rank { get; set; }
        public int Warns { get; set; }
        public DateTime MuteEndDate { get; set; }
        public bool IsMuted { get; set; }
        public string ChatColor { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as FactionInfo);
        }

        public bool Equals(FactionInfo other)
        {
            return other != null &&
                   FactionId == other.FactionId &&
                   Rank == other.Rank &&
                   Warns == other.Warns &&
                   MuteEndDate == other.MuteEndDate &&
                   IsMuted == other.IsMuted &&
                   ChatColor == other.ChatColor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FactionId, Rank, Warns, MuteEndDate, IsMuted, ChatColor);
        }
    }
}
