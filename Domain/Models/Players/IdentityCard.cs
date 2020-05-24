using Domain.Enums.Players;
using System;

namespace Domain.Models.Players
{
    public class IdentityCard : IEquatable<IdentityCard>
    {
        public string RealName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Sex { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as IdentityCard);
        }

        public bool Equals(IdentityCard other)
        {
            return other != null &&
                   RealName == other.RealName &&
                   BirthDate == other.BirthDate &&
                   Sex == other.Sex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RealName, BirthDate, Sex);
        }
    }
}
