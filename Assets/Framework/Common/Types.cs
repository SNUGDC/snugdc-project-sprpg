namespace SPRPG
{
	public enum Proportion { }

	public enum Element
	{
		Normal = 0,
	}

	public enum PassiveKey
	{
		None = 0,
	}

	public enum SkillKey
	{
		None1 = 1,
		None2 = 2,
		None3 = 3,
		None4 = 4,
		WarriorAttack,
	}

	public enum SkillSlot { _1 = 0, _2 = 1, _3 = 2, _4 = 3 }
	public enum SkillTear { _1 = 1, _2 = 2, _3 = 3, _4 = 4 }

	public enum StageId
	{
		Radiation = 1,
	}

	public enum BossId
	{
		Radiation = 1,
	}

	public static partial class ExternsionMethods
	{
		public static int ToIndex(this SkillSlot thiz)
		{
			return (int) thiz;
		}

		public static SkillTear ToTear(this SkillSlot thiz)
		{
			return (SkillTear)((int)thiz + 1);
		}

		public static SkillSlot ToSlot(this SkillTear thiz)
		{
			return (SkillSlot) ((int) thiz - 1);
		}

		public static BossId ToBossId(this StageId thiz)
		{
			return (BossId) (int) thiz;
		}
	}
}
