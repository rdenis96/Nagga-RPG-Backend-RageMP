using Domain.Models.Players;

namespace Domain.Repositories.Players
{
    public interface IPlayerRepository : IRepository<PlayerInfoWrapper>
    {
        PlayerInfoWrapper GetWrapperByUsername(string username);
        bool ExistsPlayer(string username);
        PlayerInfoWrapper GetByUsernameAndPassword(string username, string password);
    }
}
