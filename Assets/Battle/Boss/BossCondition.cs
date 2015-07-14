using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum BossConditionType
	{
		False, 
		True, 
	}

	public abstract class BossCondition
	{
		public BossConditionType Type;

		protected BossCondition(BossConditionType type)
		{
			Type = type;
		}

		public abstract bool Test(Battle context, Boss boss);
	}

	public sealed class BossFalseCondition : BossCondition
	{
		public BossFalseCondition()
			: base(BossConditionType.False)
		{}

		public override bool Test(Battle context, Boss boss)
		{
			return false;
		}
	}

	public sealed class BossTrueCondition : BossCondition
	{
		public BossTrueCondition()
			: base(BossConditionType.True)
		{}

		public override bool Test(Battle context, Boss boss)
		{
			return true;
		}
	}

	public static class BossConditionFactory
	{
		public static BossCondition Create(JsonData data)
		{
			var type = data["Type"].ParseOrDefault<BossConditionType>();

			switch (type)
			{
				case BossConditionType.False:
					return new BossFalseCondition();
				case BossConditionType.True:
					return new BossTrueCondition();
			}

			Debug.LogError(LogMessages.EnumUndefined(type));
			return new BossFalseCondition();
		}
	}
}
