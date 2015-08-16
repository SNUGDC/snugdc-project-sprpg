using System.Collections.Generic;
using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public interface StatusConditionBalanceData
	{
		Tick DefaultDuration { get; set; }
	}

	public class StatusConditionPoisonBalanceData : StatusConditionBalanceData
	{
		[JsonInclude]
		public Tick DefaultDuration { get; set; }
		public Tick Cooltime;
		public Hp Damage;
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
		public readonly Dictionary<StatusConditionType, StatusConditionBalanceData> StatusConditions = new Dictionary<StatusConditionType, StatusConditionBalanceData>();

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
		public static StatusConditionBalanceData CreateStatusCondition(StatusConditionType type, JsonData value)
		{
			switch (type)
			{
				case StatusConditionType.Poison: return value.ToObject<StatusConditionPoisonBalanceData>();
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); return null;
			}
		}

		public static StatusConditionPoisonBalanceData GetStatusConditionPoison(this BattleBalanceData thiz)
		{
			return (StatusConditionPoisonBalanceData)thiz.StatusConditions[StatusConditionType.Poison];
		}
	}
}
