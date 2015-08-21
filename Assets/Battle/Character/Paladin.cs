namespace SPRPG.Battle
{
	public class PaladinDefenseSkillActor : SkillActor
	{
		public PaladinDefenseSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void DoStart()
		{
			Owner.Guard.Enable();
			Stop();
		}
	}

	public class PaladinMassDefenceSkillActor : SkillActor
	{
		public PaladinMassDefenceSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void DoStart()
		{
			foreach (var member in Context.Party)
				if (member.IsAlive) member.Guard.Enable();
			Stop();
		}
	}
}
