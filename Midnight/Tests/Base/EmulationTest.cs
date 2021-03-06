﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Midnight.Abilities.Passive;
using Midnight.Cards;
using Midnight.Cards.Types;
using Midnight.ChiefOperations;
using Midnight.Core;
using Midnight.Tests.TestInstances;
using Midnight.Triggers;
using System.Linq;

namespace Midnight.Tests.Base
{
	[TestClass]
	public class EmulationTest
	{
		private struct Bank
		{
			public Engine engine;
			public Chief player;
			public Chief enemy;
			public Hq HQ;
			public Card Light;
			public Card Heavy;
			public Card Spatg;
		}

		private Bank CreateBank ()
		{
			var bank = new Bank();
			bank.engine = new Engine();
			bank.player = bank.engine.Chiefs[0];
			bank.enemy  = bank.engine.Chiefs[1];
			bank.HQ     = bank.player.Cards.Factory.CreateDefaultHq<HqGuards>();
			bank.Light  = bank.player.Cards.Factory.Create<TankLight>();
			bank.Heavy  = bank.enemy .Cards.Factory.Create<TankHeavy>();
			bank.Spatg  = bank.enemy .Cards.Factory.Create<TankSpatg>();
			bank.engine.Turn.StartWith(bank.player);
			return bank;
		}

		private Bank CloneBank (Bank source)
		{
			var bank = new Bank();
			bank.engine = source.engine.Clone();
			bank.player = bank.engine.Chiefs[0];
			bank.enemy  = bank.engine.Chiefs[1];
			bank.HQ     = bank.player.Cards.GetHq();
			bank.Light  = bank.player.Cards.GetAll().Find(c => c is TankLight);
			bank.Heavy  = bank.enemy .Cards.GetAll().Find(c => c is TankHeavy);
			bank.Spatg  = bank.enemy .Cards.GetAll().Find(c => c is TankSpatg);
			return bank;
		}

		[TestMethod]
		public void BaseEmulation ()
		{
			var source = CreateBank();
			var cloned = CloneBank(source);

			Assert.AreNotSame(source.engine, cloned.engine);
			Assert.AreNotSame(source.player, cloned.player);
			Assert.AreNotSame(source.enemy, cloned.enemy);
			Assert.AreNotSame(source.engine.Turn.GetOwner(), cloned.engine.Turn.GetOwner());
			Assert.AreEqual(source.engine.Turn.GetOwner().Index, cloned.engine.Turn.GetOwner().Index);
		}

		[TestMethod]
		public void CorrectCards ()
		{
			var source = CreateBank();
			var cloned = CloneBank(source);
			
			Assert.AreNotSame(source.HQ, cloned.HQ);
			Assert.AreNotSame(source.Light, cloned.Light);
			Assert.AreNotSame(source.Heavy, cloned.Heavy);
			Assert.AreNotSame(source.Spatg, cloned.Spatg);

			Assert.AreEqual(source.HQ.Id, cloned.HQ.Id);
			Assert.AreEqual(source.Spatg.Id, cloned.Spatg.Id);

			Assert.AreSame(source.HQ, source.engine.Cache.Get(source.HQ.Id));
			Assert.AreSame(cloned.HQ, cloned.engine.Cache.Get(cloned.HQ.Id));

			Assert.AreNotSame(source.HQ.GetLocation(), cloned.HQ.GetLocation());
			Assert.AreNotSame(source.HQ.Abilities, cloned.HQ.Abilities);
			Assert.AreNotSame(source.HQ.Modifiers, cloned.HQ.Modifiers);
		}

		[TestMethod]
		public void NoCloneInfluenceOnSource ()
		{
			var source = CreateBank();
			var cloned = CloneBank(source);

			var manage = new Manage(cloned.engine);
				
			manage.Damage(2, cloned.HQ, cloned.HQ);

			cloned.HQ.Abilities.Add(new FirstStrike());

			Assert.AreEqual(2, cloned.HQ.GetDamage());
			Assert.AreEqual(0, source.HQ.GetDamage());
			Assert.IsTrue (cloned.HQ.Abilities.Has<FirstStrike>());
			Assert.IsFalse(source.HQ.Abilities.Has<FirstStrike>());

			manage.Kill(cloned.HQ);

			Assert.IsTrue(cloned.HQ.IsDead());
			Assert.IsFalse(source.HQ.IsDead());
		}

		[TestMethod]
		public void NoSourceInfluenceOnClone ()
		{
			var source = CreateBank();
			var cloned = CloneBank(source);

			var manage = new Manage(source.engine);

			manage.Damage(2, source.HQ, source.HQ);

			source.HQ.Abilities.Add(new FirstStrike());

			Assert.AreEqual(2, source.HQ.GetDamage());
			Assert.AreEqual(0, cloned.HQ.GetDamage());
			Assert.IsTrue(source.HQ.Abilities.Has<FirstStrike>());
			Assert.IsFalse(cloned.HQ.Abilities.Has<FirstStrike>());

			manage.Kill(source.HQ);

			Assert.IsTrue(source.HQ.IsDead());
			Assert.IsFalse(cloned.HQ.IsDead());
		}
	}
}
