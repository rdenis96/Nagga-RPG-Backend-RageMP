using Domain.Enums.Admins;
using System;

namespace Domain.Models.Admins
{
    public class AdminInfo : IEquatable<AdminInfo>
    {
        public AdminLevels AdminLevel { get; set; }
        public string AdminName
        {
            get => nameof(AdminLevel);
        }
        public string ChatColor { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as AdminInfo);
        }

        public bool Equals(AdminInfo other)
        {
            return other != null &&
                   AdminLevel == other.AdminLevel &&
                   AdminName == other.AdminName &&
                   ChatColor == other.ChatColor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AdminLevel, AdminName, ChatColor);
        }
    }
}
