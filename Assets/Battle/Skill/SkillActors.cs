using UnityEngine;

namespace SPRPG.Battle
{
	public sealed class NullSkillActor : SkillActor
	{
		public NullSkillActor(Character owner)
			: base(new SkillBalanceData(), owner)
		{ }

		public NullSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner)
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
		protected Battle Battle { get { return Battle._; } }
		private readonly Tick _duration;
		private readonly Tick _performTick;
		private Job _performJob;
		private Job _stopJob;

		public SingleDelayedPerformSkillActor(SkillBalanceData data, Character owner, Tick duration, Tick performTick) : base(data, owner)
		{
			_duration = duration;
			_performTick = performTick;
		}

		protected override void DoStart()
		{
			_performJob = Battle.AddPlayerPerform(_performTick, Perform);
			_stopJob = Battle.AddPlayerPerform(_duration, Stop);
		}

		protected abstract void Perform();
	}

	public class AttackSkillActor : SingleDelayedPerformSkillActor 
	{
		public readonly AttackSkillArguments Arguments;

		private Battle _battle { get { return Battle._; } }
		private Job _performJob;
		private Job _stopJob;

		public AttackSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner, (Tick)3, (Tick)5)
		{
			Arguments = new AttackSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			_battle.Boss.Hit(Arguments.Damage);
		}
	}

	public sealed class HealSkillActor : SingleDelayedPerformSkillActor 
	{
		public readonly HealSkillArguments Arguments;

		public HealSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner, (Tick)3, (Tick)7)
		{
			Arguments = new HealSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			Owner.Heal(Arguments.Amount);
		}
	}

	public sealed class EvasionSkillActor : SkillActor
	{
		public readonly FiniteSkillArguments Arguments;

		private Battle _battle { get { return Battle._; } }
		private Job _performJob;
		private Job _stopJob;

		public EvasionSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner)
		{
			Arguments = new FiniteSkillArguments(data.Arguments);
		}

		protected override void DoStart()
		{
			_performJob = _battle.AddPlayerPerform((Tick)3, Perform);
			_stopJob = _battle.AddPlayerPerform((Tick)7, Stop);
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

	public sealed class ArcherAttackSkillActor : AttackSkillActor
	{
		public ArcherAttackSkillActor(SkillBalanceData data, Character owner) : base(data, owner)
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

		public ArcherArrowRainSkillActor(SkillBalanceData data, Character owner) : base(data, owner, (Tick) 3, (Tick) 7)
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
			Battle.Boss.Hit(dmg);
			OwnerPassive.RemoveAllArrows();
		}
	}
}
