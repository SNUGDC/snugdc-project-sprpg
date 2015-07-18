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

	public sealed class AttackSkillActor : SkillActor
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

		private void Perform()
		{
			_battle.Boss.Hit(Arguments.Damage);
		}

		protected override void DoCancel()
		{
			_performJob.Cancel();
			_stopJob.Cancel();
		}
	}
}
