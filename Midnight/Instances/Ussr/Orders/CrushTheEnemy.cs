﻿using Midnight.Abilities.Activating;
using Midnight.ActionManager;
using Midnight.Actions;
using Midnight.Cards;
using Midnight.Cards.Types;
using Sun.CardProtos;
using Sun.CardProtos.Enums;

namespace Midnight.Instances.Ussr.Orders
{
	public class CrushTheEnemy : Order
	{
		// Нанесите 1 повреждение выбранному штабу или технике

		public static readonly Proto Proto = new ParameterizedProto<CrushTheEnemy>
		{
			ID = "so_takbilotakbudet",
			Level = 1,
			Type = Type.Order,
			Country = Country.USSR,

			Cost = 0,
		};

		public class CrushTheEnemyAbility : SpecificAbility
		{
			protected override GameAction[] Actions (ForefrontCard target)
			{
				return new GameAction[] { new DealDamage(1, Card, target) };
			}

			protected override Search Targets (Search search)
			{
				return search
					.Enemy().Forefront()
					.Vehicle().Hq();
			}
		}

		public override Proto GetProto ()
		{
			return Proto;
		}

		public override void InitAbilities ()
		{
			base.InitAbilities();

			Abilities.Add(new CrushTheEnemyAbility());
		}
	}
}
