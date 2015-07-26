using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum ConditionType
	{
		False, 
		True, 
	}

	public abstract class Condition
	{
		public ConditionType Type;

		protected Condition(ConditionType type)
		{
			Type = type;
		}

		public abstract bool Test(Battle context);
	}

	public sealed class FalseCondition : Condition
	{
		public FalseCondition()
			: base(ConditionType.False)
		{}

		public override bool Test(Battle context)
		{
			return false;
		}
	}

	public sealed class TrueCondition : Condition
	{
		public TrueCondition()
			: base(ConditionType.True)
		{}

		public override bool Test(Battle context)
		{
			return true;
		}
	}

	public static class ConditionFactory
	{
		public static Condition Create(JsonData data)
		{
			var type = data["Type"].ParseOrDefault<ConditionType>();

			switch (type)
			{
				case ConditionType.False:
					return new FalseCondition();
				case ConditionType.True:
					return new TrueCondition();
			}

			Debug.LogError(LogMessages.EnumUndefined(type));
			return new FalseCondition();
		}
	}
}
