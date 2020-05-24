using DataLayer.Repositories.Players;
using Domain.Models.Players;
using Domain.Repositories.Players;

namespace BusinessLogic.Workers.Players
{
    public class PlayersWorker
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayersWorker()
        {
            _playerRepository = new PlayerRepository();
        }

        public bool ExistsPlayer(string username)
        {
            var result = _playerRepository.ExistsPlayer(username);
            return result;
        }

        public PlayerInfo GetPlayerInfo(string username, string password)
        {
            var result = _playerRepository.GetByUsernameAndPassword(username, password);
            return result;
        }

        public PlayerInfoWrapper GetWrapperByUsername(string username)
        {
            var result = _playerRepository.GetWrapperByUsername(username);
            return result;
        }
    }
}
