using System.Collections.Generic;
using Gem;
using UnityEngine;
using Random = System.Random;

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
		None,
		Archer,
		Thief,
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
		ArcherReload,
		ArcherEvadeShot,
		ArcherSnipe,
		ArcherWeakPointAttack,
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

		public static Color ToColor(this Element thiz)
		{
			switch (thiz)
			{
				case Element.Normal:
					return Color.black;
				case Element.Fire:
					return Color.red;
				case Element.Ice:
					return Color.blue;
				case Element.Electric:
					return Color.yellow;
				case Element.Poison:
					return Color.magenta;
				default:
					Debug.LogError(LogMessages.EnumNotHandled(thiz));
					return Color.black;
			}
		}

		public static int ToIndex(this SkillSlot thiz)
		{
			return (int) thiz;
		}

		public static SkillTier ToTier(this SkillSlot thiz)
		{
			return (SkillTier)((int)thiz + 1);
		}

		public static int ToIndex(this SkillTier thiz)
		{
			return ((int) thiz) - 1;
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
