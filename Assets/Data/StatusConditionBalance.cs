using System.Collections.Generic;
using Gem;
using LitJson;

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
}