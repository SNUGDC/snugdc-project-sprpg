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

	public class AttackSkillActor : SkillActor
	{
		public readonly AttackSkillArguments Arguments;

		private Battle _battle { get { return Battle._; } }
		private Job _performJob;
		private Job _stopJob;

		public AttackSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner)
		{
			Arguments = new AttackSkillArguments(data.Arguments);
		}

		protected override void DoStart()
		{
			_performJob = _battle.AddPlayerPerform((Tick)3, Perform);
			_stopJob = _battle.AddPlayerPerform((Tick)7, Stop);
		}

		protected virtual void Perform()
		{
			_battle.Boss.Hit(Arguments.Damage);
		}

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
		}
	}

	public sealed class HealSkillActor : SkillActor
	{
		public readonly HealSkillArguments Arguments;

		private Battle _battle { get { return Battle._; } }
		private Job _performJob;
		private Job _stopJob;

		public HealSkillActor(SkillBalanceData data, Character owner)
			: base(data, owner)
		{
			Arguments = new HealSkillArguments(data.Arguments);
		}

		protected override void DoStart()
		{
			_performJob = _battle.AddPlayerPerform((Tick)3, Perform);
			_stopJob = _battle.AddPlayerPerform((Tick)7, Stop);
		}

		private void Perform()
		{
			Owner.Heal(Arguments.Amount);
		}

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
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
		{}

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive) Owner.Passive);
			if (!archerPassive.IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return;
			}
			base.Perform();
			archerPassive.DecreaseArrow();
		}
	}
}
