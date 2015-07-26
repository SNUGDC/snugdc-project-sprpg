using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor : SkillActorBase
	{
		public BossSkillLocalKey LocalKey { get { return Data.Key; } }
		protected readonly Battle Context;
		protected readonly Boss Boss;
		protected readonly BossSkillBalanceData Data;

		public new Action<BossSkillActor> OnStop;

		protected BossSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
		{
			Context = context;
			Boss = boss;
			Data = data;

			base.OnStop += skillActor => OnStop.CheckAndCall((BossSkillActor)skillActor);
		}
	}

	public sealed class BossNoneSkillActor : BossSkillActor
	{
		public BossNoneSkillActor(Battle context, Boss boss)
			: base(context, boss, new BossSkillBalanceData())
		{}

		protected override void DoStart()
		{
			// do nothing
		}
	}

	public abstract class BossSingleDelayedPerformSkillActor : BossSkillActor
	{
		protected Battle Battle { get { return Battle._; } }
		private readonly Tick _performTick;
		private Job _performJob;
		private Job _stopJob;

		protected BossSingleDelayedPerformSkillActor(Battle context, Boss owner, BossSkillBalanceData data, Tick performTick) : base(context, owner, data)
		{
			Debug.Assert(data.Duration > performTick, "data.Duration > performTick");
			_performTick = performTick;
		}

		protected override void DoStart()
		{
			_performJob = Battle.AddPlayerPerform(_performTick, Perform);
			_stopJob = Battle.AddPlayerPerform(Data.Duration, Stop);
		}

		protected abstract void Perform();

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
			base.DoStop();
		}
	}

	public class BossGrantStatusConditionSkillActor : BossSkillActor
	{
		private readonly BossGrantStatusConditionArgument _argument;

		public BossGrantStatusConditionSkillActor(Battle context, Boss boss, BossSkillBalanceData data) : base(context, boss, data)
		{
			_argument = data.Arguments.ToObject<BossGrantStatusConditionArgument>();
		}

		protected override void DoStart()
		{
			Context.Party.GetAliveLeaderOrMember().TestAndGrant(_argument.Percentage, _argument.StatusCondition, Data.Duration);
			Context.AddBeforeTurn(Data.Duration, Stop);
		}
	}
}
