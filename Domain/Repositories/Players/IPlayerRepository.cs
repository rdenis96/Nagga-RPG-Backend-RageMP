using Domain.Models.Interfaces;
using Domain.Models.Players;

namespace Domain.Repositories.Players
{
    public interface IPlayerRepository : IRepository<PlayerInfoWrapper>
    {
        PlayerInfoWrapper GetWrapperByUsername(string username);
        bool ExistsPlayer(string username);
        PlayerInfo GetByUsernameAndPassword(string username, string password);
    }
}
