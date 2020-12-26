using Domain.Models.Factions;

namespace Domain.Repositories.Factions
{
    public interface IFactionInfoRepository : IRepository<FactionInfo>
    {
        void CreateByPlayerId(int playerId);

        FactionInfo GetByMemberId(int playerId);
    }
}