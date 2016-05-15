﻿using Midnight.Abilities.Positioning;
using Midnight.ActionManager;
using Midnight.Battlefield;
using Midnight.Cards;
using Midnight.Cards.Types;

namespace Midnight.Actions
{
	public class Step : GameAction<Step>
	{
		private FieldCard card;
		private Cell cell;

		public Step (FieldCard card, Cell cell)
		{
			this.card = card;
			this.cell = cell;
		}

		public override void Configure ()
		{
			card.abilities.Get<Movement>().Activate(cell);
			card.GetFieldLocation().ToCell(cell);

			// todo: spotted
		}
	}
}