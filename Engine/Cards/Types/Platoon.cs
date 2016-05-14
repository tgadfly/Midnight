﻿using Midnight.Engine.Abilities;
using Midnight.Engine.Cards.Enums;

namespace Midnight.Engine.Cards.Types
{
	public abstract class Platoon : ForefrontCard
	{

		public abstract class AttackPlatoon : Platoon { }
		public abstract class DefensePlatoon : Platoon { }

		public void ToSupport ()
		{
			ToLocation(Location.support);
		}

		public override bool IsActivePlatoon ()
		{
			return IsAtSupport();
		}

		public static Subtype[] subtypeOrder = {
			Subtype.scout,
			Subtype.communications,
			Subtype.artillery,
			Subtype.medic,
			Subtype.intendancy,
		};

		public override CardAbility[] CreateAbilities ()
		{
			return new CardAbility[] { };
		}
	}
}