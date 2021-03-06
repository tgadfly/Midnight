﻿using Midnight.Abilities.Passive;
using Midnight.Cards.Vehicles;
using Sun.CardProtos;
using Sun.CardProtos.Enums;

namespace Midnight.Instances.Germany.Vehicles
{
	public class StPz2 : SpgVehicle
	{

		public static readonly Proto Proto = new ParameterizedProto<StPz2>() {
			ID = "gv_sturmpanzerII",
			Level = 5,
			Type = Type.Vehicle,
			Subtype = Subtype.Spatg,
			Country = Country.Germany,

			Power = 2,
			Defense = 0,
			Toughness = 4,
			Increase = 0,
			Cost = 4,
		};
		
		public class GermanySupply : Supply
		{
			public override bool IsActive ()
			{
				return Chief.Cards.HasHq(Country.Germany);
			}
		}

		public override Proto GetProto ()
		{
			return Proto;
		}

		public override void InitAbilities ()
		{
			base.InitAbilities();

			Abilities.Add(new GermanySupply());
		}
	}
}