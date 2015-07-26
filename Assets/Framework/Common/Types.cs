using System.Collections.Generic;

namespace SPRPG
{
	public enum Weight { }

	public enum Tick { }
	public enum Term { _1, _2, _3, _4 }
	public enum Period { }

	public enum Element
	{
		Normal = 0,
		Poison = 1,
	}

	public enum PassiveKey
	{
		None = 0,
		Warrior = 1,
		Wizard = 2,
		Archer = 3,
	}

	public enum SkillKey
	{
		None1 = 1,
		None2 = 2,
		None3 = 3,
		None4 = 4,
		WarriorAttack = 5,
		WarriorEvasion = 7,
		WarriorHeal = 6,
		WarriorStrongAttack = 8,
		ArcherAttack = 9,
		ArcherEvasion = 10,
		ArcherStrongShot = 11,
		ArcherArrowRain = 12,
	}

	public enum SkillSlot { _1 = 0, _2 = 1, _3 = 2, _4 = 3 }
	public enum SkillTier { _1 = 1, _2 = 2, _3 = 3, _4 = 4 }

	public enum StageId
	{
		Radiation = 1,
		Noise, 
		Water,
		Atmosphere,
		Dessert, 
		PoisonForest,
	}

	public enum BossId
	{
		Radiation = 1,
		Noise,
		Water,
		Atmosphere,
		Dessert,
		PoisonForest,
	}

	public enum Hp { }

	public struct Damage
	{
		public Hp Value;
		public Element Element;

		public Damage(Hp value)
		{
			Value = value;
			Element = Element.Normal;
		}

		public Damage(Hp value, Element element)
		{
			Value = value;
			Element = element;
		}
	}

	public static partial class ExtensionMethods
	{
		public static int ToIndex(this SkillSlot thiz)
		{
			return (int) thiz;
		}

		public static SkillTier ToTier(this SkillSlot thiz)
		{
			return (SkillTier)((int)thiz + 1);
		}

		public static SkillSlot ToSlot(this SkillTier thiz)
		{
			return (SkillSlot) ((int) thiz - 1);
		}

		public static BossId ToBossId(this StageId thiz)
		{
			return (BossId) (int) thiz;
		}

		public static Hp ToValue(this StatHp thiz)
		{
			return (Hp)(int)thiz;
		}
	}

	public static class SkillHelper
	{
		public static IEnumerator<SkillSlot> GetSlotEnumerator()
		{
			yield return SkillSlot._1;
			yield return SkillSlot._2;
			yield return SkillSlot._3;
			yield return SkillSlot._4;
		}
	}
}
