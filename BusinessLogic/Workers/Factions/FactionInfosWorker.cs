using DataLayer.Factions;
using Domain.Models.Factions;
using Domain.Repositories.Factions;
using Helper.Factions;
using System;

namespace BusinessLogic.Workers.Factions
{
    public class FactionInfosWorker
    {
        private readonly IFactionInfoRepository _factionInfoRepository;
        public FactionInfosWorker()
        {
            _factionInfoRepository = new FactionInfoRepository();
        }

        public void SetFaction(FactionInfo factionInfo, string faction)
        {
            var factionModel = FactionHelper.GetFactionByName(faction);
            factionInfo.FactionId = factionModel.FactionId;
            factionInfo.Warns = 0;
            factionInfo.Rank = 1;
            factionInfo.IsMuted = false;
            factionInfo.MuteEndDate = DateTime.MinValue;

            _factionInfoRepository.Update(factionInfo);
        }
    }
}
