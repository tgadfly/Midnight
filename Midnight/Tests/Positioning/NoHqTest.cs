﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Midnight.Cards.Enums;
using Midnight.Cards.Types;
using Midnight.Core;
using Midnight.Tests.TestInstances;
using Midnight.Triggers;
using Midnight.Utils;

namespace Midnight.Tests.Positioning
{
	[TestClass]
	public class NoHqTest
	{
		[TestMethod]
		public void NoHq ()
		{
			Engine engine = new Engine();
			Logger logger = new Logger(engine);
			Manage manage = new Manage(engine);

			var field = engine.Field;
			var player = engine.Chiefs[0];
			var enemy = engine.Chiefs[1];

			var plCards = new FieldCard[] {
				player.Cards.Factory.Create<TankLight>(),
				player.Cards.Factory.Create<TankMedium>(),
				player.Cards.Factory.Create<TankHeavy>(),
				player.Cards.Factory.Create<TankSpg>()
			};

			var enCards = new FieldCard[] {
				enemy.Cards.Factory.Create<TankLight>(),
				enemy.Cards.Factory.Create<TankMedium>(),
				enemy.Cards.Factory.Create<TankHeavy>(),
				enemy.Cards.Factory.Create<TankSpatg>()
			};

			manage.SetResources(player, 50);
			manage.SetResources(enemy, 50);

			manage.Draw(plCards);
			manage.Draw(enCards);

			manage.StartGame(player);

			Assert.IsTrue(plCards[0].GetLocation().IsReserve());

			manage.Deploy(plCards[0], field.GetCell(0, 0));
			Assert.IsTrue(plCards[0].GetLocation().IsBattlefield());
			Assert.AreEqual(plCards[0], field.GetCell(0, 0).GetCard());

			manage.EndTurn(player);

			manage.Deploy(enCards[2], field.GetCellSoft(-1, 2));
			Assert.IsTrue(enCards[2].GetFieldLocation().IsBattlefield());
			Assert.AreEqual(enCards[2], field.GetCellSoft(-1, 2).GetCard());
			Assert.AreEqual(enCards[2], field.GetCell(4, 2).GetCard());

			var deploy = manage.Deploy(plCards[1], field.GetCell(0, 1));
			Assert.AreEqual(Status.NotTurnOfSource, deploy.GetStatus());
			Assert.IsTrue(plCards[1].GetFieldLocation().IsReserve());

			manage.EndTurn(enemy);

			var deploys = new[] {
				manage.Deploy(plCards[1], field.GetCell(0, 1)),
				manage.Deploy(plCards[2], field.GetCell(0, 1)),
				manage.Deploy(plCards[3], field.GetCell(3, 1)),
			};

			Assert.IsTrue(deploys[0].IsValid());
			Assert.AreEqual(Status.CellIsNotAllowed, deploys[1].GetStatus());
			Assert.AreEqual(Status.CellIsNotAllowed, deploys[2].GetStatus());

			Assert.IsTrue(plCards[1].GetFieldLocation().IsBattlefield());
			Assert.IsTrue(plCards[2].GetFieldLocation().IsReserve());
			Assert.IsTrue(plCards[3].GetFieldLocation().IsReserve());
			Assert.AreEqual(null, plCards[3].GetFieldLocation().GetCell());

			manage.EndTurn(player);

			manage.Deploy(enCards[0], field.GetCellSoft(-1, 0));
			manage.Deploy(enCards[1], field.GetCellSoft(-1, 1));

			Assert.AreEqual(field.GetCellSoft(-1, 0), enCards[0].GetFieldLocation().GetCell());
			Assert.AreEqual(field.GetCellSoft(-1, 1), enCards[1].GetFieldLocation().GetCell());
		}
	}
}
