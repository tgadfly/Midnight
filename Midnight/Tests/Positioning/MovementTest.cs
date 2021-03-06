﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Midnight.Tests.TestInstances;
using Midnight.Core;
using Midnight.Utils;

namespace Midnight.Tests.Positioning
{
	[TestClass]
	public class MovementTest
	{
		[TestMethod]
		public void Movement ()
		{
			Engine engine = new Engine();
			var logger = new Logger(engine);

			var field = engine.Field;
			var player = engine.Chiefs[0];
			var enemy = engine.Chiefs[1];

			var Light  = player.Cards.Factory.Create<TankLight>();
			var Medium = player.Cards.Factory.Create<TankMedium>();
			var Heavy  = player.Cards.Factory.Create<TankHeavy>();
			var Spg    = player.Cards.Factory.Create<TankSpg>();

			var manage = new Manage(engine);

			foreach (var tank in player.Cards.GetAll()) {
				tank.GetLocation().ToReserve();
			}

			manage.SetResources(player, 50);
			manage.StartGame(player);

			// Light
			manage.Deploy(Light, field.GetCell(0, 0));
			var firstLightMove = manage.Move(Light, field.GetCell(1, 0));
			Assert.IsTrue(firstLightMove.IsValid());
			Assert.IsTrue(Light.GetLocation().IsBattlefield());
			Assert.AreEqual(field.GetCell(1, 0), Light.GetFieldLocation().GetCell());

			var secondLightMove = manage.Move(Light, field.GetCell(2, 0));
			Assert.IsFalse(secondLightMove.IsValid());
			Assert.AreEqual(field.GetCell(1, 0), Light.GetFieldLocation().GetCell());

			// Medium
			manage.Deploy(Medium, field.GetCell(0, 1));
			var MediumMove = manage.Move(Medium, field.GetCell(1, 1));
			Assert.IsFalse(MediumMove.IsValid());
			Assert.AreEqual(Status.PointsAreUsed, MediumMove.GetStatus());
			Assert.AreEqual(field.GetCell(0, 1), Medium.GetFieldLocation().GetCell());

			// Heavy
			manage.Deploy(Heavy, field.GetCell(0, 2));
			var HeavyMove = manage.Move(Heavy, field.GetCell(1, 2));
			Assert.IsFalse(HeavyMove.IsValid());
			Assert.AreEqual(Status.PointsAreUsed, HeavyMove.GetStatus());
			Assert.AreEqual(field.GetCell(0, 2), Heavy.GetFieldLocation().GetCell());

			// Move without deploy
			var SpgMove = manage.Move(Spg, field.GetCell(0, 0));
			Assert.IsFalse(SpgMove.IsValid());
			Assert.AreEqual(Status.NotAtBattlefield, SpgMove.GetStatus());
			Assert.IsTrue(Spg.GetLocation().IsReserve());

			manage.EndTurn(player);
			manage.EndTurn(enemy);

			// Light can jump
			var LightJump = manage.Move(Light, field.GetCell(3, 0));
			Assert.IsTrue(LightJump.IsValid());
			Assert.AreEqual(field.GetCell(3, 0), Light.GetFieldLocation().GetCell());

			// Medium diagonal
			var MediumDiagonalMove = manage.Move(Medium, field.GetCell(1, 0));
			Assert.AreEqual(field.GetCell(1, 0), Medium.GetFieldLocation().GetCell());

			// Heavy move
			var nextHeavyMove = manage.Move(Heavy, field.GetCell(1, 2));
			Assert.IsTrue(nextHeavyMove.IsValid());
			Assert.AreEqual(field.GetCell(1, 2), Heavy.GetFieldLocation().GetCell());

		}
	}
}
