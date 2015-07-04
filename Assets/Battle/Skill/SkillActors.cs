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
	}

	public class AttackSkillActor : SkillActor
	{
		public readonly Damage Damage;

		public AttackSkillActor(SkillBalanceData data, Damage damage)
			: base(data)
		{
			Damage = damage;
		}

		public override void Perform()
		{
			Context.Boss.Hit(Damage);
		}
	}
}
