﻿using System.Collections.Generic;
using Midnight.Cards;
using Midnight.Abilities.Aggression;
using Midnight.Core;
using Midnight.Cards.Types;
using System.Linq;

namespace Midnight.ChiefOperations.IoOptions.Collectors
{
	internal class AttacksCollector : Collector<Aggression, AttackOptions>
	{
		public AttacksCollector (Card card) : base(card) { }

		protected override AttackOptions GetOptions (Aggression ability)
		{
			var attacks = new List<TargetOption>();

            foreach (var target in GetAllowedTargets())
            {
                var emulated = card.GetChief().GetEmulated();
                emulated.Attack(new Io.Target
                {
                    SourceId = card.Id,
                    TargetId = target.Id
                });
				attacks.Add(new TargetOption { TargetId = target.Id, Predictions = emulated.GetDamagePredictions()});
			}

			return attacks.Count == 0 ? null : new AttackOptions { Targets = attacks.ToArray() };
		}

		private List<FieldCard> GetAllowedTargets ()
		{
			var weapon = card.Abilities.Get<Weapon>();

		    return card.GetChief().GetOpponent().cards
                .GetAll().OfType<FieldCard>()
                .Where(fieldCard => weapon.Validate(fieldCard) == Status.Success)
                .ToList();
		}
	}
}
