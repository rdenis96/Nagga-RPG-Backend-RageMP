using Domain.Models.Factions;

namespace Domain.Repositories.Factions
{
    public interface IFactionInfoRepository : IRepository<FactionInfo>
    {
        void Create(int playerId);
        FactionInfo GetByMemberId(int playerId);
    }
}
