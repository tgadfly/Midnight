﻿using Midnight.ActionManager.Events;
using Midnight.Actions;
using Midnight.Cards.Types;
using Midnight.ChiefOperations;
using Midnight.Emitter;

namespace Midnight.Triggers
{
	public class FinalHqDeath : Trigger, IListener<After<Death>>
	{
		public void On (After<Death> ev)
		{
			var card = ev.Action.Card;
			var chief = card.GetChief();

			if (card is Hq && IsOwner(chief) && HasNoAliveHq(chief))
            {
				Engine.Actions.Delay(new Final(chief, Final.Trigger.HqDeath));
			}
		}

		private bool HasNoAliveHq (Chief chief)
		{
			return chief.Cards.GetAliveHqs().Count == 0;
		}
	}
}
