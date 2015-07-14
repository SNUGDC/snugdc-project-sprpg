namespace SPRPG.Battle
{
	public class NullSkillActor : SkillActor
	{
		public NullSkillActor()
			: base(new SkillBalanceData())
		{ }

		public NullSkillActor(SkillBalanceData data)
			: base(data)
		{ }

		public override void Perform()
		{ }

		public override void Cancel()
		{ }
	}

	public class AttackSkillActor : SkillActor
	{
		public readonly Damage Damage;
		private Battle _battle { get { return Battle._; } }
		private JobId _performJob;

		public AttackSkillActor(SkillBalanceData data, Damage damage)
			: base(data)
		{
			Damage = damage;
		}

		public override void Perform()
		{
			_battle.AddPlayerPerform((Tick)3, DoPerform);
		}

		private void DoPerform()
		{
			_battle.Boss.Hit(Damage);
			_performJob = default(JobId);
		}

		public override void Cancel()
		{
			if (_performJob == default(JobId)) return;
			_battle.RemovePlayerPerform(_performJob);
			_performJob = default(JobId);
		}
	}
}
