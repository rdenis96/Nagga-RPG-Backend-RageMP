using DataLayer.Repositories.Players;
using Domain.Enums.Admins;
using Domain.Enums.Licenses;
using Domain.Enums.Players;
using Domain.Models.Admins;
using Domain.Models.Players;
using Domain.Repositories.Players;
using Helper.Locations;
using System;

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

        public PlayerInfoWrapper GetPlayerInfoByUsernameAndPassword(string username, string password)
        {
            var result = _playerRepository.GetByUsernameAndPassword(username, password);
            return result;
        }

        public PlayerInfoWrapper GetWrapperByUsername(string username)
        {
            var result = _playerRepository.GetWrapperByUsername(username);
            return result;
        }

        public void Create(string username, string email, string password)
        {
            var player = GeneratePlayerInfoWrapper(username, email, password);
            _playerRepository.Create(player);
        }

        public PlayerInfoWrapper Update(PlayerInfoWrapper playerInfo)
        {
            var result = _playerRepository.Update(playerInfo);
            return result;
        }

        private PlayerInfoWrapper GeneratePlayerInfoWrapper(string username, string email, string password)
        {
            var currentDate = DateTime.UtcNow;
            var playerInfo = new PlayerInfoWrapper
            {
                Username = username,
                Password = password,
                Email = email,
                RegisterDate = currentDate,
                LastActiveDate = DateTime.MinValue,
                SkinId = 0,
                Health = 100,
                Armor = 100,
                PhoneNumber = 1234,
                Money = 2000000,
                BankMoney = 5000000,
                PositionWrapper = SpawnLocationsHelper.CivilSpawnPosition,
                RotationWrapper = SpawnLocationsHelper.CivilSpawnRotation,
                IdCard = new IdentityCard
                {
                    RealName = "No Name",
                    BirthDate = DateTime.MinValue,
                    Sex = Gender.Neutral
                },
                Admin = new AdminInfo
                {
                    AdminLevel = AdminLevels.None,
                    ChatColor = "#FFFFFF"
                },
                FactionInfoId = 0,
                Licenses = LicensesTypes.All,
                TimePlayed = 0

            };
            return playerInfo;
        }
    }
}
