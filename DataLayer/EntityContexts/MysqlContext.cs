using DataLayer.EntityDefinitions;
using Domain.Constants;
using Domain.Models.Characters;
using Domain.Models.Factions;
using Domain.Models.Players;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntityContexts
{
    public class MysqlContext : DbContext
    {
        public MysqlContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseMySql(ConnectionStrings.MysqlConnectionDatabase);
            }

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<PlayerInfoWrapper> PlayersInfos { get; set; }
        public DbSet<Skin> Skins { get; set; }
        public DbSet<FactionInfo> FactionInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PlayerDefinitions.Set(modelBuilder);
            SkinDefinitions.Set(modelBuilder);
            FactionInfoDefinitions.Set(modelBuilder);
        }
    }
}
