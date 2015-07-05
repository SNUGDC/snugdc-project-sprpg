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
		private JobId _performJob;

		public AttackSkillActor(SkillBalanceData data, Damage damage)
			: base(data)
		{
			Damage = damage;
		}

		public override void Perform()
		{
			Context.AddPlayerPerform((Tick) 3, DoPerform);
		}

		private void DoPerform()
		{
			Context.Boss.Hit(Damage);
			_performJob = default(JobId);
		}

		public override void Cancel()
		{
			if (_performJob == default(JobId)) return;
			Context.RemovePlayerPerform(_performJob);
			_performJob = default(JobId);
		}
	}
}
