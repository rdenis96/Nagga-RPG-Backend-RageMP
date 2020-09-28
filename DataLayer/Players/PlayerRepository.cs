using DataLayer.EntityContexts;
using DataLayer.Factions;
using Domain.Models.Factions;
using Domain.Models.Players;
using Domain.Repositories.Factions;
using Domain.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IFactionInfoRepository _factionInfoRepository;
        public PlayerRepository()
        {
            _factionInfoRepository = new FactionInfoRepository();
        }
        public PlayerInfoWrapper GetWrapperByUsername(string username)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Where(x => x.Username == username).Include(x => x.Faction).FirstOrDefault();
                return result;
            }
        }

        public bool ExistsPlayer(string username)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Any(x => x.Username == username);
                return result;
            }
        }

        public PlayerInfoWrapper GetByUsernameAndPassword(string username, string password)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).Include(x => x.Faction).FirstOrDefault();
                return result;
            }
        }

        public void Create(PlayerInfoWrapper entity)
        {
            using (var context = new MysqlContext())
            {
                context.PlayersInfos.Add(entity);
                context.SaveChanges();

                _factionInfoRepository.Create(entity.Id);
                SetCivilFactionForPlayer(entity);
            }
        }

        public PlayerInfoWrapper Update(PlayerInfoWrapper entity)
        {
            bool changesSaved = false;
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                changesSaved = context.SaveChanges() > 0;
            }
            return changesSaved ? entity : null;
        }

        public IEnumerable<PlayerInfoWrapper> GetAll()
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.ToList();
                return result ?? new List<PlayerInfoWrapper>();
            }
        }

        public bool Delete(PlayerInfoWrapper entity)
        {
            bool changesSaved = false;
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                changesSaved = context.SaveChanges() > 0;
            }

            return changesSaved;
        }

        public PlayerInfoWrapper GetById(int id)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Find(id);
                return result;
            }
        }

        private void SetCivilFactionForPlayer(PlayerInfoWrapper player)
        {
            FactionInfo factionInfo = _factionInfoRepository.GetByMemberId(player.Id);
            player.FactionInfoId = factionInfo.Id;
            Update(player);
        }
    }
}
