using DataLayer.Common;
using DataLayer.EntityContexts;
using Domain.Enums.Factions;
using Domain.Models.Factions;
using Domain.Repositories.Factions;
using Helper.Characters.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Factions
{
    public class FactionInfoRepository : BaseRepository<FactionInfo>, IFactionInfoRepository
    {
        public void CreateByPlayerId(int playerId)
        {
            using (var context = new MysqlContext())
            {
                var factionInfo = GenerateFactionInfoForPlayer(playerId);
                context.FactionInfos.Add(factionInfo);
                context.SaveChanges();
            }
        }

        public FactionInfo GetByMemberId(int playerId)
        {
            using (var context = new MysqlContext())
            {
                var factionInfo = context.FactionInfos.Where(x => x.MemberId == playerId).FirstOrDefault();
                return factionInfo;
            }
        }

        public override IEnumerable<FactionInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public override FactionInfo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Create(FactionInfo entity)
        {
            throw new NotImplementedException();
        }

        private FactionInfo GenerateFactionInfoForPlayer(int playerId)
        {
            var factionInfo = new FactionInfo
            {
                FactionId = FactionType.None,
                MemberId = playerId,
                SkinId = (long)PredefinedSkins.CivilSkin,
                MuteEndDate = DateTime.MinValue,
                Rank = 0,
                Warns = 0,
                IsMuted = false
            };
            return factionInfo;
        }
    }
}