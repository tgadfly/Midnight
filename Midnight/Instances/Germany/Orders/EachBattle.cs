﻿using Midnight.Abilities.Activating;
using Midnight.ActionManager;
using Midnight.Actions;
using Midnight.Cards;
using Midnight.Cards.Enums;
using Midnight.Cards.Types;
using Sun.CardProtos.Enums;

namespace Midnight.Instances.Germany.Orders
{
	public class EachBattle : Order
	{
		// Нанесите 2 повреждения выбранной технике.
		// Восстановите 2 прочности вашему штабу.

		public static readonly Proto proto = new Proto<EachBattle>() {
			id = "go_tagderwehrmacht",
			level = 1,
			type = Type.Order,
			country = Country.Germany,

			cost = 4,
		};

		public class EachBattleAbility : SpecificAbility
		{
			protected override GameAction[] Actions (ForefrontCard target)
			{
				return new GameAction[] {
					new DealDamage(2, card, target),
					new HealDamage(2, card, chief.cards.GetHq())
				};
			}

			protected override Search Targets (Search search)
			{
				return search
					.Enemy().Forefront()
					.Vehicle();
			}
		}

		public override Proto GetProto ()
		{
			return proto;
		}

		public override void InitAbilities ()
		{
			base.InitAbilities();

			abilities.Add(new EachBattleAbility());
		}
	}
}
