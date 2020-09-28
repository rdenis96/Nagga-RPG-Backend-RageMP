using Domain.Enums.Factions;
using System;

namespace Domain.Models.Factions
{
    public class Faction : IEquatable<Faction>
    {
        public FactionType FactionId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Faction);
        }

        public bool Equals(Faction other)
        {
            return other != null &&
                   FactionId == other.FactionId &&
                   Name == other.Name &&
                   Color == other.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FactionId, Name, Color);
        }
    }
}
