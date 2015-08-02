using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor : SkillActorBase<BossSkillActor>
	{
		public BossSkillLocalKey LocalKey { get { return Data.Key; } }
		protected readonly Boss Boss;
		protected readonly BossSkillBalanceData Data;

		protected BossSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context)
		{
			Boss = boss;
			Data = data;
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
			_performJob = Context.AddBossSkill(_performTick, Perform);
			_stopJob = Context.AddBossSkill(Data.Duration, Stop);
		}

		protected abstract void Perform();

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
			base.DoCancel();
		}
	}

	public class BossGrantStatusConditionSkillActor : BossSkillActor
	{
		private readonly StatusConditionTest _statusConditionTest;

		public BossGrantStatusConditionSkillActor(Battle context, Boss boss, BossSkillBalanceData data) : base(context, boss, data)
		{
			_statusConditionTest = data.Arguments["StatusConditionTest"].ToObject<StatusConditionTest>();
		}

		protected override void DoStart()
		{
			Context.Party.GetAliveLeaderOrMember().TestAndGrant(_statusConditionTest, Data.Duration);
			Context.AddBeforeTurn(Data.Duration, Stop);
		}
	}
}
