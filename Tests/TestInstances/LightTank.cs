﻿using Midnight.Cards;
using Midnight.Cards.Enums;
using Midnight.Cards.Vehicles;

namespace Midnight.Tests.TestInstances
{
	public class LightTank : LightVehicle
	{
		public static readonly Proto proto = new Proto() {
			id = "tv_light_tank",
			level = 1,
			type = Type.vehicle,
			subtype = Subtype.light,
			country = Country.usa,

			power = 1,
			defense = 0,
			toughness = 2,
			increase = 1,
			cost = 1,
		};

		public override Proto GetProto ()
		{
			return proto;
		}
	}
}