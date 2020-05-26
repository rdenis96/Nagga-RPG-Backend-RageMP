using DataLayer.Factions;
using Domain.Repositories.Factions;

namespace BusinessLogic.Workers.Factions
{
    public class FactionInfosWorker
    {
        private readonly IFactionInfoRepository _factionInfoRepository;
        public FactionInfosWorker()
        {
            _factionInfoRepository = new FactionInfoRepository();
        }

    }
}
