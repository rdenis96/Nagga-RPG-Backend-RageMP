using Domain.Models.Factions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntityDefinitions
{
    internal static class FactionInfoDefinitions
    {
        public static void Set(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactionInfo>().ToTable("factioninfos");

            modelBuilder.Entity<FactionInfo>().HasKey(p => p.Id);
            modelBuilder.Entity<FactionInfo>().Property(p => p.FactionId).IsRequired();
            modelBuilder.Entity<FactionInfo>().Property(p => p.MemberId).IsRequired();
            modelBuilder.Entity<FactionInfo>().Property(p => p.SkinId).IsRequired();
            modelBuilder.Entity<FactionInfo>().Property(p => p.Rank).IsRequired();
            modelBuilder.Entity<FactionInfo>().Property(p => p.Warns).IsRequired();
            modelBuilder.Entity<FactionInfo>().Property(p => p.MuteEndDate).IsRequired().HasColumnType("datetime");
            modelBuilder.Entity<FactionInfo>().Property(p => p.IsMuted).IsRequired();
        }
    }
}
