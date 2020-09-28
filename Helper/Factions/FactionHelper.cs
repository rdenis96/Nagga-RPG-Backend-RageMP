using Domain.Enums.Factions;
using Domain.Models.Factions;
using System;
using System.Collections.Generic;

namespace Helper.Factions
{
    public static class FactionHelper
    {
        private const string CivilColor = "#FFFFFF";
        private const string PoliceColor = "#0000FF";
        private const string FbiColor = "#187BCD";
        private const string NgColor = "#152238";
        private const string SmurdColor = "#DC143C";

        private static Dictionary<FactionType, Faction> _factions;
        static FactionHelper()
        {
            _factions = new Dictionary<FactionType, Faction>();
            _factions.Add(FactionType.None, new Faction
            {
                FactionId = FactionType.None,
                Name = "Civil",
                Color = CivilColor
            });
            _factions.Add(FactionType.PD, new Faction
            {
                FactionId = FactionType.PD,
                Name = "Police Department",
                Color = PoliceColor
            });
            _factions.Add(FactionType.FBI, new Faction
            {
                FactionId = FactionType.FBI,
                Name = "Federal Beaureu Of Investigations",
                Color = FbiColor
            });
            _factions.Add(FactionType.NG, new Faction
            {
                FactionId = FactionType.NG,
                Name = "National Guard",
                Color = NgColor
            });
            _factions.Add(FactionType.SMURD, new Faction
            {
                FactionId = FactionType.SMURD,
                Name = "SMURD",
                Color = SmurdColor
            });
        }
        public static Faction GetFactionById(FactionType factionId)
        {
            var canGetFaction = _factions.TryGetValue(factionId, out Faction faction);
            if (canGetFaction)
            {
                return faction;
            }
            return null;
        }

        public static Faction GetFactionByName(string factionName)
        {
            FactionType factionType;

            bool isNumeric = int.TryParse(factionName, out int factionId);
            if (isNumeric)
            {

                factionType = (FactionType)factionId;
            }
            else
            {
                factionName = factionName.ToUpper();
                factionType = (FactionType)Enum.Parse(typeof(FactionType), factionName);
            }
            var faction = GetFactionById(factionType);
            return faction;
        }

    }
}
