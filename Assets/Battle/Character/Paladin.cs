namespace SPRPG.Battle
{
	public class PaladinDefenseSkillActor : SingleDelayedPerformSkillActor
	{
		public PaladinDefenseSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void Perform()
		{
			Owner.Guard.Enable();
		}
	}

	public class PaladinMassDefenceSkillActor : SingleDelayedPerformSkillActor
	{
		public PaladinMassDefenceSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void Perform()
		{
			foreach (var member in Context.Party)
				if (member.IsAlive) member.Guard.Enable();
		}
	}
}
