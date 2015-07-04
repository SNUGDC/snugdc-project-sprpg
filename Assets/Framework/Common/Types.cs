namespace SPRPG
{
	public enum Element
	{
		Normal = 0,
	}

	public enum PassiveKey { }

	public enum SkillKey
	{
		WarriorAttack,
	}

	public enum SkillSlot { _1 = 0, _2 = 1, _3 = 2, _4 = 3 }
	public enum SkillTear { _1 = 1, _2 = 2, _3 = 3, _4 = 4 }

	public enum StageId
	{
		Radiation,
	}

	public enum BossId
	{
		Radiation,
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
