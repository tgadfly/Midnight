﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Midnight.Cards.Enums;
using Midnight.Cards.Types;
using Midnight.ChiefOperations;
using Midnight.Core;
using Midnight.Tests.TestInstances;
using Midnight.Triggers;
using Midnight.Utils;
using Midnight.Actions;

namespace Midnight.Tests.Triggers
{
	[TestClass]
	public class FinalHqDeathTest
	{
		[TestMethod]
		public void FinalHqDeath ()
		{
			Engine engine = new Engine();
			Logger logger = new Logger(engine);
			Manage manage = new Manage(engine);

			engine.Triggers.Register<FinalDeckOut>();
			engine.Triggers.Register<FinalHqDeath>();

			var final = new FinalListener(engine);

			var player = engine.Chiefs[0];
			var enemy  = engine.Chiefs[1];

			var HQ = player.Cards.Factory.CreateDefaultHq<HqGuards>();
			var Spg1 = enemy.Cards.Factory.Create<TankBigSpg>();
			var Spg2 = enemy.Cards.Factory.Create<TankBigSpg>();

			manage.Position(Spg1, engine.Field.GetCell(2, 2));
			manage.Position(Spg2, engine.Field.GetCell(1, 2));

			manage.StartGame(enemy);

			manage.Fight(Spg1, HQ);

			Assert.IsFalse(HQ.IsDead());
			Assert.AreEqual(null, final.action);

			manage.Fight(Spg2, HQ);

			Assert.IsTrue(HQ.IsDead());
			Assert.AreNotEqual(null, final.action);
			Assert.AreEqual(1, final.count);
			Assert.AreEqual(final.action.trigger, Final.Trigger.HqDeath);
		}
	}
}
