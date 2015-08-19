using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public sealed class NullSkillActor : SkillActor
	{
		public NullSkillActor(Battle context, Character owner)
			: base(new SkillBalanceData(), context, owner)
		{ }

		public NullSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{ }

		protected override void DoStart()
		{
			// immediately stop.
			Stop();
		}

		protected override void DoStop()
		{ }
	}

	public abstract class SingleDelayedPerformSkillActor : SkillActor
	{
		private readonly Tick _duration;
		private readonly Tick _performTick;
		private Job _performJob;
		private Job _stopJob;

		protected SingleDelayedPerformSkillActor(SkillBalanceData data, Battle context, Character owner)
			: this(data, context, owner, BattleBalance._.Data.Character.DefaultSkillDuration, BattleBalance._.Data.Character.DefaultSkillDelay)
		{ }

		protected SingleDelayedPerformSkillActor(SkillBalanceData data, Battle context, Character owner, Tick duration, Tick performTick) : base(data, context, owner)
		{
			_duration = duration;
			_performTick = performTick;
		}

		protected override void DoStart()
		{
			_performJob = Context.AddPlayerSkill(_performTick, Perform);
			_stopJob = Context.AddPlayerSkill(_duration, Stop);
		}

		protected abstract void Perform();

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
			base.DoCancel();
		}
	}

	public class AttackSkillActor : SingleDelayedPerformSkillActor 
	{
		public readonly AttackSkillArguments Arguments;

		public AttackSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			Arguments = new AttackSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, Arguments.Damage);
		}
	}

	public sealed class HealSkillActor : SingleDelayedPerformSkillActor 
	{
		public readonly HealSkillArguments Arguments;

		public HealSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			Arguments = new HealSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			Owner.Heal(Arguments.Amount);
		}
	}

	public sealed class MassHealSkillActor : SingleDelayedPerformSkillActor
	{
		public readonly HealSkillArguments Arguments;

		public MassHealSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			Arguments = new HealSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			foreach (var member in Context.Party)
				member.Heal(Arguments.Amount);
		}
	}

	public sealed class MassCureSkillActor : SingleDelayedPerformSkillActor
	{
		public MassCureSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{ }

		protected override void Perform()
		{
			foreach (var member in Context.Party)
				member.CureAll();
		}
	}

	public sealed class EvasionSkillActor : SkillActor
	{
		public readonly FiniteSkillArguments Arguments;

		private Job _performJob;
		private Job _stopJob;

		public EvasionSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			Arguments = new FiniteSkillArguments(data.Arguments);
		}

		protected override void DoStart()
		{
			_performJob = Context.AddPlayerSkill((Tick)3, Perform);
			_stopJob = Context.AddPlayerSkill((Tick)7, Stop);
		}

		private void Perform()
		{
			Owner.Evade(Arguments.Duration);
		}

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
		}
	}

	public class AttackAndGrantStatusConditionSkillActor : SingleDelayedPerformSkillActor
	{
		private readonly AttackAndGrantStatusConditionArguments _arguments;

		public AttackAndGrantStatusConditionSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<AttackAndGrantStatusConditionArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
			Context.Boss.TestAndGrant(_arguments.StatusConditionTest);
		}
	}

	public class AttackAndTestStunSkillActor : SingleDelayedPerformSkillActor
	{
		private readonly AttackAndTestStunArguments _arguments;

		public AttackAndTestStunSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<AttackAndTestStunArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
			Context.Boss.TestAndGrant(_arguments.StunTest);
		}
	}

	public sealed class ArcherAttackSkillActor : AttackSkillActor
	{
		public ArcherAttackSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{ }

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			if (!archerPassive.IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return;
			}
			base.Perform();
			archerPassive.DecreaseArrow();
		}
	}

	public sealed class ArcherArrowRainSkillActor : SingleDelayedPerformSkillActor
	{
		private ArcherPassive OwnerPassive { get { return (ArcherPassive) Owner.Passive; } }

		public readonly ArcherArrowRainArguments Arguments;

		public ArcherArrowRainSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner, (Tick) 3, (Tick) 7)
		{
			Arguments = new ArcherArrowRainArguments(data.Arguments);
		}

		protected override void Perform()
		{
			if (!OwnerPassive.IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return;
			}
			var dmgValue = ((Hp) ((int) Arguments.DamagePerArrow*OwnerPassive.Arrows));
			var dmg = new Damage(dmgValue);
			Owner.Attack(Context.Boss, dmg);
			OwnerPassive.RemoveAllArrows();
		}
	}
}
