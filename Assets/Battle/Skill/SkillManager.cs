namespace SPRPG.Battle
{
	public class SkillManager
	{
		private readonly SkillActor[] _actors = new SkillActor[SPRPG.Const.SkillSlotSize];

		public SkillActor this[SkillSlot slot]
		{
			get { return _actors[slot.ToIndex()]; }
			private set { _actors[slot.ToIndex()] = value; }
		}

		public SkillManager(SkillSetDef def)
		{
			this[SkillSlot._1] = SkillFactory.Create(def._1);
			this[SkillSlot._2] = SkillFactory.Create(def._2);
			this[SkillSlot._3] = SkillFactory.Create(def._3);
			this[SkillSlot._4] = SkillFactory.Create(def._4);
		}
	}
}
