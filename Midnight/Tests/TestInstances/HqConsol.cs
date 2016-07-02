﻿using Midnight.Cards.Enums;
using Midnight.Cards.Types;
using Sun.CardProtos.Enums;

namespace Midnight.Tests.TestInstances
{
	public class HqConsol : Hq
	{
		public static readonly Proto proto = new Proto<HqConsol>() {
			id = "th_consolidated",
			level = 1,
			type = Type.HQ,
			subtype = Subtype.Consolidated,
			country = Country.USSR,

			power = 2,
			toughness = 25,
			increase = 5,
		};

		public override Proto GetProto ()
		{
			return proto;
		}
	}
}