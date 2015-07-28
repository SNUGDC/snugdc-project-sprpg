namespace SPRPG.Battle
{
	public abstract class SkillActor : SkillActorBase<SkillActor>
	{
		public SkillKey Key { get { return Data.Key; } }
		public readonly SkillBalanceData Data;
		public readonly Character Owner;

		protected SkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(context)
		{
			Data = data;
			Owner = owner;
		}
	}
}