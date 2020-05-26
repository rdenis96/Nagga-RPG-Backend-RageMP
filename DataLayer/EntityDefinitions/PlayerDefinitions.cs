using Domain.Enums.Admins;
using Domain.Enums.Characters;
using Domain.Enums.Licenses;
using Domain.Enums.Players;
using Domain.Models.Players;
using Microsoft.EntityFrameworkCore;
using System;

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

            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.SelectedSkin).IsRequired().HasDefaultValue(SkinType.Faction);
            modelBuilder.Entity<PlayerInfoWrapper>().HasOne(p => p.Skin).WithMany().HasForeignKey(p => p.SkinId).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Health).IsRequired().HasDefaultValue(100);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Armor).IsRequired().HasDefaultValue(100);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.PhoneNumber).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Money).IsRequired().HasDefaultValue(50000);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.BankMoney).IsRequired().HasDefaultValue(100000);
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
                   g.Property(p => p.RealName).IsRequired().HasDefaultValue("No Name");
                   g.Property(p => p.BirthDate).IsRequired().HasDefaultValue(DateTime.MinValue);
                   g.Property(p => p.Sex).IsRequired().HasDefaultValue(Gender.Neutral);
               });
            modelBuilder.Entity<PlayerInfoWrapper>().OwnsOne(p => p.Admin,
               g =>
               {
                   g.Property(p => p.AdminLevel).IsRequired().HasDefaultValue(AdminLevels.None);
                   g.Ignore(p => p.AdminName);
               });

            modelBuilder.Entity<PlayerInfoWrapper>().HasOne(p => p.Faction).WithMany().HasForeignKey(p => p.FactionInfoId).IsRequired();
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.Licenses).IsRequired().HasDefaultValue(LicensesTypes.None);
            modelBuilder.Entity<PlayerInfoWrapper>().Property(p => p.TimePlayed).IsRequired().HasDefaultValue(0);
        }
    }
}
