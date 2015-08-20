using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum ConditionType
	{
		False, 
		True, 
		And,
		Or,
		CompareInt,
		Dictionary,
		SomeCharacterHasStatusCondition,
		BossPhase,
	}

	public enum ConditionCompareOperator
	{
		Equal,
		Less,
		LessEqual,
		Greater,
		GreaterEqual,
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

	public sealed class AndCondition : Condition
	{
		private readonly List<Condition> _conditions;

		public AndCondition(List<Condition> conditions)
			: base(ConditionType.And)
		{
			_conditions = conditions;
		}

		public override bool Test(Battle context)
		{
			return _conditions.All(condition => condition.Test(context));
		}
	}

	public sealed class OrCondition : Condition
	{
		private readonly List<Condition> _conditions;

		public OrCondition(List<Condition> conditions)
			: base(ConditionType.And)
		{
			_conditions = conditions;
		}

		public override bool Test(Battle context)
		{
			return _conditions.Any(condition => condition.Test(context));
		}
	}

	public sealed class CompareIntCondition : Condition
	{
		private readonly ConditionCompareOperator _operator;
		private readonly JsonData _value1;
		private readonly JsonData _value2;

		public CompareIntCondition(JsonData data) : base(ConditionType.False)
		{
			ConditionHelper.TryParse((string) data["Operator"], out _operator);
			_value1 = data["Value1"];
			_value2 = data["Value2"];
		}

		public override bool Test(Battle context)
		{
			var binding = context.Binding;
			var value1 = binding.EvalOrDefault(_value1);
			var value2 = binding.EvalOrDefault(_value2);
			return _operator.Do(value1, value2);
		}
	}

	public sealed class DictionaryCondition : Condition
	{
		private readonly Condition _condition;

		public DictionaryCondition(JsonData data) : base(ConditionType.Dictionary)
		{
			var key = (string)data["Key"];
			_condition = ConditionDictionary._.Find(key);
		}

		public override bool Test(Battle context)
		{
			if (_condition == null)
			{
				Debug.LogError("condition is null.");
				return false;
			}

			return _condition.Test(context);
		}
	}

	public sealed class SomeCharacterHasStatusConditionCondition : Condition
	{
		private readonly StatusConditionType _type;

		public SomeCharacterHasStatusConditionCondition(JsonData data) : base(ConditionType.SomeCharacterHasStatusCondition)
		{
			_type = data["StatusCondition"].ToEnum<StatusConditionType>();
		}

		public override bool Test(Battle context)
		{
			return context.Party.Any(member => member.HasStatusCondition(_type));
		}
	}

	public sealed class BossPhaseCondition : Condition
	{
		private readonly BossPhaseState _phase;

		public BossPhaseCondition(JsonData data) : base(ConditionType.BossPhase)
		{
			_phase = (BossPhaseState) (int) data["Phase"];
		}

		public override bool Test(Battle context)
		{
			return context.Boss.Phase == _phase;
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
				case ConditionType.And:
					return new AndCondition(CreateArray(data["Array"]));
				case ConditionType.Or:
					return new OrCondition(CreateArray(data["Array"]));
				case ConditionType.CompareInt:
					return new CompareIntCondition(data);
				case ConditionType.Dictionary:
					return new DictionaryCondition(data);
				case ConditionType.SomeCharacterHasStatusCondition:
					return new SomeCharacterHasStatusConditionCondition(data);
				case ConditionType.BossPhase:
					return new BossPhaseCondition(data);
			}

			Debug.LogError(LogMessages.EnumUndefined(type));
			return new FalseCondition();
		}

		public static List<Condition> CreateArray(JsonData data)
		{
			var ret = new List<Condition>(data.Count);
			foreach (var conditionData in data.GetListEnum())
				ret.Add(Create(conditionData));
			return ret;
		}
	}

	public static class ConditionHelper
	{
		public static bool TryParse(string str, out ConditionCompareOperator op)
		{
			switch (str)
			{
				case "==": op = ConditionCompareOperator.Equal; return true;
				case "<": op = ConditionCompareOperator.Less; return true;
				case "<=": op = ConditionCompareOperator.LessEqual; return true;
				case ">": op = ConditionCompareOperator.Greater; return true;
				case ">=": op = ConditionCompareOperator.GreaterEqual; return true;
			}

			Debug.LogError(LogMessages.ParseFailed<ConditionCompareOperator>(str));
			op = default(ConditionCompareOperator);
			return false;
		}

		public static bool Do(this ConditionCompareOperator thiz, int value1, int value2)
		{
			switch (thiz)
			{
				case ConditionCompareOperator.Equal: return value1 == value2;
				case ConditionCompareOperator.Less: return value1 < value2;
				case ConditionCompareOperator.LessEqual: return value1 <= value2;
				case ConditionCompareOperator.Greater: return value1 > value2;
				case ConditionCompareOperator.GreaterEqual: return value1 >= value2;
				default:
					Debug.LogError(LogMessages.EnumNotHandled(thiz));
					return false;
			}
		}
	}
}
