﻿using Midnight.Cards;
using Midnight.Cards.Enums;
using Midnight.Cards.Vehicles;

namespace Midnight.Tests.TestInstances
{
	public class MediumTank : MediumVehicle
	{
		public static readonly Proto proto = new Proto() {
			id = "tv_medium_tank",
			level = 1,
			type = Type.vehicle,
			subtype = Subtype.medium,
			country = Country.germany,

			power = 2,
			defense = 0,
			toughness = 5,
			increase = 1,
			cost = 2,
		};

		public override Proto GetProto ()
		{
			return proto;
		}
	}
}