﻿using Domain.Enums.Players;
using Domain.Models.Players;
using GTANetworkAPI;
using NaggaServer.Models.Players;
using System;

namespace NaggaServer.Helpers.Statistics
{
    public static class PlayerStatisticsHelper
    {
        public static StatsViewModel GetPlayerStatistics(Player player, PlayerInfoWrapper playerInfo)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - playerInfo.IdCard.BirthDate.Year;
            if (playerInfo.IdCard.BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            StatsViewModel stats = new StatsViewModel
            {
                Name = player.Name,
                Faction = "Grove Street",
                NameTag = $"{player.Nametag}({player.Id})",
                Age = age,
                BankMoney = $"{playerInfo.BankMoney}$",
                Money = $"{playerInfo.Money}$",
                Job = $"Trucker",
                Level = playerInfo.Level,
                PhoneNumber = playerInfo.PhoneNumber,
                RespectPoints = $"{playerInfo.RespectPoints}/{playerInfo.Level * 2}",
                Sex = Enum.GetName(typeof(Gender), playerInfo.IdCard.Sex)
            };
            return stats;
        }
    }
}
