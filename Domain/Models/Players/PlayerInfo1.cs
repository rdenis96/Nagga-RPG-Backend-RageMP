using System;

namespace Domain.Models.Players
{
    public class PlayerInfo : IEquatable<PlayerInfo>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLogged { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastActiveDate { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlayerInfo);
        }

        public bool Equals(PlayerInfo other)
        {
            return other != null &&
                   Id == other.Id &&
                   Username == other.Username &&
                   Password == other.Password &&
                   IsLogged == other.IsLogged &&
                   RegisterDate == other.RegisterDate &&
                   LastActiveDate == other.LastActiveDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Password, IsLogged, RegisterDate, LastActiveDate);
        }
    }
}
