using Domain.Enums.Factions;
using Domain.Repositories.Players;
using System;

namespace Domain.Models.Factions
{
    public class FactionInfo : IPlayerChatBase, IEquatable<FactionInfo>
    {
        public int Id { get; set; }
        public FactionType FactionId { get; set; }
        public int MemberId { get; set; }
        public long SkinId { get; set; }
        public int Rank { get; set; }
        public int Warns { get; set; }
        public DateTime MuteEndDate { get; set; }
        public bool IsMuted { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as FactionInfo);
        }

        public bool Equals(FactionInfo other)
        {
            return other != null &&
                   Id == other.Id &&
                   FactionId == other.FactionId &&
                   MemberId == other.MemberId &&
                   SkinId == other.SkinId &&
                   Rank == other.Rank &&
                   Warns == other.Warns &&
                   MuteEndDate == other.MuteEndDate &&
                   IsMuted == other.IsMuted;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FactionId, MemberId, SkinId, Rank, Warns, MuteEndDate, IsMuted);
        }
    }
}
