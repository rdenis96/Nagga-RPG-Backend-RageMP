using DataLayer.Common;
using DataLayer.EntityContexts;
using Domain.Models.Factions;
using Domain.Models.Players;
using Domain.Repositories.Factions;
using Domain.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories.Players
{
    public class PlayerRepository : BaseRepository<PlayerInfoWrapper>, IPlayerRepository
    {
        private readonly IFactionInfoRepository _factionInfoRepository;

        public PlayerRepository(IFactionInfoRepository factionInfoRepository)
        {
            _factionInfoRepository = factionInfoRepository;
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

        public override void Create(PlayerInfoWrapper entity)
        {
            using (var context = new MysqlContext())
            {
                context.PlayersInfos.Add(entity);
                context.SaveChanges();

                _factionInfoRepository.CreateByPlayerId(entity.Id);
                SetCivilFactionForPlayer(entity);
            }
        }

        public override IEnumerable<PlayerInfoWrapper> GetAll()
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.ToList();
                return result ?? new List<PlayerInfoWrapper>();
            }
        }

        public override PlayerInfoWrapper GetById(int id)
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