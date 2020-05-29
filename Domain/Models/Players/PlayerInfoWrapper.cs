using Domain.Enums.Characters;
using Domain.Enums.Licenses;
using Domain.Models.Admins;
using Domain.Models.Characters;
using Domain.Models.Common;
using Domain.Models.Factions;
using System;
using System.Collections.Generic;

namespace Domain.Models.Players
{
	public class PlayerInfoWrapper : PlayerInfo, IEquatable<PlayerInfoWrapper>
	{
		public int Level { get; set; }
		public int RespectPoints { get; set; }
		public SkinType SelectedSkin { get; set; }
		public int SkinId { get; set; }
		public Skin Skin { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public int PhoneNumber { get; set; }
		public long Money { get; set; }
		public long BankMoney { get; set; }
		public Vector3Wrapper PositionWrapper { get; set; }
		public Vector3Wrapper RotationWrapper { get; set; }

		public IdentityCard IdCard { get; set; }
		public AdminInfo Admin { get; set; }
		public int FactionInfoId { get; set; }
		public FactionInfo Faction { get; set; }
		public LicensesTypes Licenses { get; set; }
		public double TimePlayed { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as PlayerInfoWrapper);
		}

		public bool Equals(PlayerInfoWrapper other)
		{
			return other != null &&
				   base.Equals(other) &&
				   Level == other.Level &&
				   RespectPoints == other.RespectPoints &&
				   SelectedSkin == other.SelectedSkin &&
				   SkinId == other.SkinId &&
				   EqualityComparer<Skin>.Default.Equals(Skin, other.Skin) &&
				   Health == other.Health &&
				   Armor == other.Armor &&
				   PhoneNumber == other.PhoneNumber &&
				   Money == other.Money &&
				   BankMoney == other.BankMoney &&
				   EqualityComparer<IdentityCard>.Default.Equals(IdCard, other.IdCard) &&
				   EqualityComparer<AdminInfo>.Default.Equals(Admin, other.Admin) &&
				   EqualityComparer<FactionInfo>.Default.Equals(Faction, other.Faction) &&
				   Licenses == other.Licenses &&
				   TimePlayed == other.TimePlayed;
		}

		public override int GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.Add(base.GetHashCode());
			hash.Add(Level);
			hash.Add(RespectPoints);
			hash.Add(SelectedSkin);
			hash.Add(SkinId);
			hash.Add(Skin);
			hash.Add(Health);
			hash.Add(Armor);
			hash.Add(PhoneNumber);
			hash.Add(Money);
			hash.Add(BankMoney);
			hash.Add(IdCard);
			hash.Add(Admin);
			hash.Add(Faction);
			hash.Add(Licenses);
			hash.Add(TimePlayed);
			return hash.ToHashCode();
		}
	}
}
