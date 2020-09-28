using System;

namespace Domain.Models.Common
{
    public class Mute : IEquatable<Mute>
    {
        public bool IsMuted { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Reason { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Mute);
        }

        public bool Equals(Mute other)
        {
            return other != null &&
                   IsMuted == other.IsMuted &&
                   ExpirationTime == other.ExpirationTime &&
                   Reason == other.Reason;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsMuted, ExpirationTime, Reason);
        }
    }
}
