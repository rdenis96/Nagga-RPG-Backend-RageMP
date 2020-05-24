using DataLayer.EntityDefinitions;
using Domain.Constants;
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
                optionsBuilder.UseSqlServer(ConnectionStrings.MysqlConnectionDatabase);
            }

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<PlayerInfoWrapper> PlayersInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PlayerDefinitions.Set(modelBuilder);
        }
    }
}
