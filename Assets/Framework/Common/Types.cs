using System;
using System.Collections.Generic;

namespace SPRPG
{
	public enum Weight { }
	public enum Percentage { _0 = 0, _50 = 50, _100 = 100 }

	public enum Tick { }
	public enum Term { _1, _2, _3, _4 }
	public enum Period { }

	public enum Element
	{
		Normal = 0,
		Fire = 1,
		Ice = 2,
		Electric = 3,
		Poison = 4,
	}

	public enum StatusConditionType
	{
		Freeze = 1,
		Poison = 2,
		Blind = 3,
	}

	public enum StatusConditionGroup
	{
		Character = 1,
		Boss = 2,
	}

	public enum PassiveKey
	{
		None = 0,
		Archer = 1,
	}

	public enum SkillKey
	{
		None1,
		None2,
		None3,
		None4,
		WarriorAttack,
		WarriorEvasion,
		WarriorHeal,
		WarriorStrongAttack,
		WizardFireBolt,
		WizardIceBolt,
		WizardLighteningBolt,
		WizardFireBall,
		WizardIceSpear,
		WizardLightening,
		ArcherAttack,
		ArcherEvasion,
		ArcherStrongShot,
		ArcherArrowRain,
		PaladinMassHeal,
		PaladinBash,
		PaladinDefense,
		PaladinPurification,
		PaladinBigMassHeal,
		PaladinMassDefense,
		MonkHeal,
		MonkSacrifice,
		MonkEquility,
		MonkRevenge,
		MonkRecovery,
		ThiefStab,
		ThiefEvade,
		ThiefSandAttack,
		ThiefPoisonAttack,
		ThiefAssassination,
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
		public static Random Random = new Random(0);

		public static bool Test(this Percentage thiz)
		{
			return Random.Next()%100 < (int)thiz;
		}
		
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
		public static SkillSlot MakeSlotWithIndex(int i)
		{
			return (SkillSlot) i;
		}

		public static IEnumerable<SkillSlot> GetSlotEnumerable()
		{
			yield return SkillSlot._1;
			yield return SkillSlot._2;
			yield return SkillSlot._3;
			yield return SkillSlot._4;
		}
	}
}
