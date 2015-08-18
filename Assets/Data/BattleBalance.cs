using System;
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

	public class StatusConditionBossBlindBalanceData : IStatusConditionBalanceData
	{
		[JsonInclude]
		public Tick DefaultDuration { get; set; }
		public Percentage Accuracy;
	}

	public class StatusConditionBalanceDatas
	{
		private readonly Dictionary<StatusConditionType, IStatusConditionBalanceData> _dict =
			new Dictionary<StatusConditionType, IStatusConditionBalanceData>();

		public IStatusConditionBalanceData this[StatusConditionType type] { get { return _dict[type]; } }

		public StatusConditionBalanceDatas(Dictionary<string, JsonData> dict, StatusConditionGroup group)
		{
			foreach (var kv in dict)
			{
				StatusConditionType type;
				if (EnumHelper.TryParse(kv.Key, out type))
					_dict[type] = BattleBalanceHelper.CreateStatusCondition(group, type, kv.Value);
			}
		}
	}

	public class CharacterStatusConditionBalanceDatas : StatusConditionBalanceDatas
	{
		public CharacterStatusConditionBalanceDatas(Dictionary<string, JsonData> dict) : base(dict, StatusConditionGroup.Character) { }
		public StatusConditionPoisonBalanceData GetPoison() { return (StatusConditionPoisonBalanceData)this[StatusConditionType.Poison]; }
	}

	public class BossStatusConditionBalanceDatas : StatusConditionBalanceDatas
	{
		public BossStatusConditionBalanceDatas(Dictionary<string, JsonData> dict) : base(dict, StatusConditionGroup.Boss) { }
		public StatusConditionBalanceData GetFreeze() { return (StatusConditionBalanceData)this[StatusConditionType.Freeze]; }
		public StatusConditionPoisonBalanceData GetPoison() { return (StatusConditionPoisonBalanceData)this[StatusConditionType.Poison]; }
		public StatusConditionBossBlindBalanceData GetBlind() { return (StatusConditionBossBlindBalanceData)this[StatusConditionType.Blind]; }
	}

	public class BattleCharacterBalanceData
	{
		public Tick DefaultSkillDuration;
		public Tick DefaultSkillDelay;

		[JsonInclude]
		private Dictionary<string, JsonData> _statusConditions;
		[JsonIgnore]
		public CharacterStatusConditionBalanceDatas StatusConditions;

		public void Build()
		{
			StatusConditions = new CharacterStatusConditionBalanceDatas(_statusConditions);
			_statusConditions = null;
		}
	}

	public class BattleBossBalanceData
	{
		public Tick DelayBeforeSkill;

		[JsonInclude]
		private Dictionary<string, JsonData> _statusConditions;
		[JsonIgnore]
		public BossStatusConditionBalanceDatas StatusConditions;

		public void Build()
		{
			StatusConditions = new BossStatusConditionBalanceDatas(_statusConditions);
			_statusConditions = null;
		}
	}

	public class BattleBalanceData
	{
		public Tick TickPerSecond;
		public Tick InputValidBefore;
		public Tick InputValidAfter;

		public BattleCharacterBalanceData Character;
		public BattleBossBalanceData Boss;

		public float PartyPlaceLerp;
		public List<Vector3> PartyPositions;

		public void Build()
		{
			Character.Build();
			Boss.Build();
		}

		public IStatusConditionBalanceData GetStatusCondition(StatusConditionGroup group, StatusConditionType type)
		{
			switch (group)
			{
				case StatusConditionGroup.Character: return Character.StatusConditions[type];
				case StatusConditionGroup.Boss: return Boss.StatusConditions[type];
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); return null;
			}
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
		public static IStatusConditionBalanceData CreateStatusCondition(StatusConditionGroup group, StatusConditionType type, JsonData value)
		{
			switch (type)
			{
				case StatusConditionType.Freeze: return value.ToObject<StatusConditionBalanceData>();
				case StatusConditionType.Poison: return value.ToObject<StatusConditionPoisonBalanceData>();
				case StatusConditionType.Blind: return CreateBossStatusCondition(type, value);
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); return null;
			}
		}

		public static IStatusConditionBalanceData CreateBossStatusCondition(StatusConditionType type, JsonData value)
		{
			switch (type)
			{
				case StatusConditionType.Blind: return value.ToObject<StatusConditionBossBlindBalanceData>();
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); return null;
			}
		}
	}
}
