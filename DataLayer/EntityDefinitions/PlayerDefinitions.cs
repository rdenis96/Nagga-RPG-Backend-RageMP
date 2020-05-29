using Domain.Models.Players;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntityDefinitions
{
    internal static class PlayerDefinitions
    {
        public static void Set(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerInfoWrapper>().ToTable("playerinfowrappers");

            modelBuilder.Entity<PlayerInfoWrapper>().HasKey(p => p.Id);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Username).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Password).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.RegisterDate).IsRequired().HasColumnType("datetime");
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.LastActiveDate).IsRequired().HasColumnType("datetime");

            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Level).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.RespectPoints).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.SelectedSkin).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().HasOne(p => p.Skin).WithMany().HasForeignKey(p => p.SkinId).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Health).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Armor).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.PhoneNumber).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Money).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.BankMoney).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().OwnsOne(p => p.PositionWrapper,
               g =>
               {
                   g.Property(p => p.X).IsRequired();
                   g.Property(p => p.Y).IsRequired();
                   g.Property(p => p.Z).IsRequired();
               });
            modelBuilder.Entity<PlayerInfoWrapper>().OwnsOne(p => p.RotationWrapper,
               g =>
               {
                   g.Property(p => p.X).IsRequired();
                   g.Property(p => p.Y).IsRequired();
                   g.Property(p => p.Z).IsRequired();
               });
            modelBuilder.Entity<PlayerInfoWrapper>().OwnsOne(p => p.IdCard,
               g =>
               {
                   g.Property(p => p.RealName).IsRequired();
                   g.Property(p => p.BirthDate).IsRequired();
                   g.Property(p => p.Sex).IsRequired();
               });
            modelBuilder.Entity<PlayerInfoWrapper>().OwnsOne(p => p.Admin,
               g =>
               {
                   g.Property(p => p.AdminLevel).IsRequired();
                   g.Ignore(p => p.AdminName);
               });

            modelBuilder.Entity<PlayerInfoWrapper>().HasOne(p => p.Faction).WithMany().HasForeignKey(p => p.FactionInfoId).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Licenses).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.TimePlayed).IsRequired();
        }
    }
}
