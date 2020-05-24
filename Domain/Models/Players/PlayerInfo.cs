using Domain.Enums.Licenses;
using Domain.Models.Admins;
using Domain.Models.Characters;
using Domain.Models.Common;
using Domain.Models.Factions;
using GTANetworkAPI;
using System;
using System.Collections.Generic;

namespace Domain.Models.Players
{
	public class PlayerInfoWrapper : PlayerInfo, IEquatable<PlayerInfoWrapper>
	{
		public int SkinId { get; set; }
		public Skin Skin { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public int PhoneNumber { get; set; }
		public long Money { get; set; }
		public long BankMoney { get; set; }
		public Vector3 Position { get => new Vector3(PositionWrapper.X, PositionWrapper.Y, PositionWrapper.Z); }
		public Vector3 Rotation { get => new Vector3(RotationWrapper.X, RotationWrapper.Y, RotationWrapper.Z); }
		public Vector3Wrapper PositionWrapper { get; set; }
		public Vector3Wrapper RotationWrapper { get; set; }

		public IdentityCard IdCard { get; set; }
		public AdminInfo Admin { get; set; }
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
				   SkinId == other.SkinId &&
				   EqualityComparer<Skin>.Default.Equals(Skin, other.Skin) &&
				   Health == other.Health &&
				   Armor == other.Armor &&
				   PhoneNumber == other.PhoneNumber &&
				   Money == other.Money &&
				   BankMoney == other.BankMoney &&
				   EqualityComparer<Vector3>.Default.Equals(Position, other.Position) &&
				   EqualityComparer<Vector3>.Default.Equals(Rotation, other.Rotation) &&
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
			hash.Add(SkinId);
			hash.Add(Skin);
			hash.Add(Health);
			hash.Add(Armor);
			hash.Add(PhoneNumber);
			hash.Add(Money);
			hash.Add(BankMoney);
			hash.Add(Position);
			hash.Add(Rotation);
			hash.Add(IdCard);
			hash.Add(Admin);
			hash.Add(Faction);
			hash.Add(Licenses);
			hash.Add(TimePlayed);
			return hash.ToHashCode();
		}
	}
}
