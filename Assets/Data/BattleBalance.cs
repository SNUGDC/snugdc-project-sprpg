using System.Collections.Generic;
using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public interface IStatusConditionBalanceData
	{
		Tick DefaultDuration { get; set; }
	}

	public class StatusConditionBalanceData : IStatusConditionBalanceData
	{
		[JsonInclude]
		public Tick DefaultDuration { get; set; }
	}

	public class StatusConditionPoisonBalanceData : IStatusConditionBalanceData
	{
		[JsonInclude]
		public Tick DefaultDuration { get; set; }
		public Tick Cooltime;
		public Hp Damage;
	}

	public class StatusConditionBlindBalanceData : IStatusConditionBalanceData
	{
		[JsonInclude]
		public Tick DefaultDuration { get; set; }
		public Percentage Accuracy;
	}

	public struct BattleCharacterBalanceData
	{
		public Tick DefaultSkillDuration;
		public Tick DefaultSkillDelay;
	}

	public struct BattleBossBalanceData
	{
		public Tick DelayBeforeSkill;
	}

	public class BattleBalanceData
	{
		public Tick TickPerSecond;
		public Tick InputValidBefore;
		public Tick InputValidAfter;

		[JsonInclude]
		private Dictionary<string, JsonData> _statusConditions;
		[JsonIgnore]
		public readonly Dictionary<StatusConditionType, IStatusConditionBalanceData> StatusConditions = new Dictionary<StatusConditionType, IStatusConditionBalanceData>();

		public BattleCharacterBalanceData Character;
		public BattleBossBalanceData Boss;

		public float PartyPlaceLerp;
		public List<Vector3> PartyPositions;

		public void Build()
		{
			BuildStatusConditions();
		}

		private void BuildStatusConditions()
		{
			foreach (var kv in _statusConditions)
			{
				StatusConditionType type;
				if (EnumHelper.TryParse(kv.Key, out type))
					StatusConditions[type] = BattleBalanceHelper.CreateStatusCondition(type, kv.Value);
			}
			_statusConditions = null;
		}
	}

	public sealed class BattleBalance : Balance<BattleBalanceData>
	{
		public static readonly BattleBalance _ = new BattleBalance();

		public BattleBalance() : base("battle")
		{}

		protected override void AfterLoad(bool success)
		{
			Data.Build();
		}
	}

	public static class BattleBalanceHelper
	{
		public static IStatusConditionBalanceData CreateStatusCondition(StatusConditionType type, JsonData value)
		{
			switch (type)
			{
				case StatusConditionType.Freeze: return value.ToObject<StatusConditionBalanceData>();
				case StatusConditionType.Poison: return value.ToObject<StatusConditionPoisonBalanceData>();
				case StatusConditionType.Blind: return value.ToObject<StatusConditionBlindBalanceData>();
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); return null;
			}
		}

		public static StatusConditionBlindBalanceData GetStatusConditionBlind(this BattleBalanceData thiz)
		{
			return (StatusConditionBlindBalanceData)thiz.StatusConditions[StatusConditionType.Blind];
		}

		public static StatusConditionPoisonBalanceData GetStatusConditionPoison(this BattleBalanceData thiz)
		{
			return (StatusConditionPoisonBalanceData)thiz.StatusConditions[StatusConditionType.Poison];
		}
	}
}
