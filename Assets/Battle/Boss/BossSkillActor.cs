using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor : SkillActorBase<BossSkillActor>
	{
		public BossSkillLocalKey LocalKey { get { return Data.Key; } }
		protected readonly BossSkillBalanceData Data;
		protected readonly Boss Owner;

		protected BossSkillActor(BossSkillBalanceData data, Battle context, Boss owner)
			: base(context)
		{
			Data = data;
			Owner = owner;
		}
	}

	public sealed class BossNoneSkillActor : BossSkillActor
	{
		public BossNoneSkillActor(Battle context, Boss owner)
			: base(new BossSkillBalanceData(), context, owner)
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

		protected BossSingleDelayedPerformSkillActor(BossSkillBalanceData data, Battle context, Boss owner, Tick performTick) : base(data, context, owner)
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

		public BossGrantStatusConditionSkillActor(BossSkillBalanceData data, Battle context, Boss owner) : base(data, context, owner)
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
