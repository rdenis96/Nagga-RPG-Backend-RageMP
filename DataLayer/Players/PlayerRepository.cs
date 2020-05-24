using DataLayer.EntityContexts;
using Domain.Models.Players;
using Domain.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerInfoWrapper GetWrapperByUsername(string username)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Where(x => x.Username == username).FirstOrDefault();
                return result;
            }
            //PlayerInfo player = new PlayerInfo
            //{
            //    Admin = new AdminInfo
            //    {
            //        AdminLevel = AdminLevels.Owner,
            //        ChatColor = "#000000"
            //    },
            //    Armor = 100,
            //    Health = 100,
            //    Money = 10000000,
            //    BankMoney = 100000000,
            //    Faction = new FactionInfo
            //    {
            //        FactionId = Factions.FBI,
            //        ChatColor = "#dsfdsf",
            //        IsMuted = false,
            //        MuteEndDate = DateTime.MinValue,
            //        Rank = 6,
            //        Warns = 0
            //    },
            //    IdCard = new IdentityCard
            //    {
            //        BirthDate = new DateTime(1996, 11, 05),
            //        RealName = "Radu Denis",
            //        Sex = Gender.Male
            //    },
            //    IsLogged = true,
            //    Licenses = LicensesTypes.Driving | LicensesTypes.Flying | LicensesTypes.Sailing | LicensesTypes.Weapons,
            //    Name = "DFR",
            //    Password = "pass",
            //    PhoneNumber = 7273,
            //    PositionWrapper = new Vector3Wrapper(200, 200, 200),
            //    RotationWrapper = new Vector3Wrapper(200, 200, 200),
            //    Skin = new Skin(),
            //    SkinId = 2,
            //    TimePlayed = 200000
            //};
        }

        public bool ExistsPlayer(string username)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Any(x => x.Username == username);
                return result;
            }
        }

        public PlayerInfo GetByUsernameAndPassword(string username, string password)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                return result;
            }
        }

        public void Create(PlayerInfoWrapper entity)
        {
            using (var context = new MysqlContext())
            {
                context.PlayersInfos.Add(entity);
                context.SaveChanges();
            }
        }

        public PlayerInfoWrapper Update(PlayerInfoWrapper entity)
        {
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return GetWrapperByUsername(entity.Username);
            }
        }

        public IEnumerable<PlayerInfoWrapper> GetAll()
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.ToList();
                return result ?? new List<PlayerInfoWrapper>();
            }
        }

        public bool Delete(PlayerInfoWrapper entity)
        {
            bool changesSaved = false;
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                changesSaved = context.SaveChanges() > 0;
            }

            return changesSaved;
        }

        public PlayerInfoWrapper GetById(int id)
        {
            using (var context = new MysqlContext())
            {
                var result = context.PlayersInfos.Find(id);
                return result;
            }
        }
    }
}
