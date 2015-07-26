using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;

namespace SPRPG.Battle
{
	public enum BossPassiveLocalKey
	{
		None,
		RadioactiveArea,
	}
	
	public enum BossSkillLocalKey
	{
		Attack,
	}

	public class BossSkillCommonBalanceData
	{
		public Proportion Proportion;
		public Tick Duration;

		public JsonData _condition;
		public BossCondition Condition;

		public JsonData Arguments;

		public void Build(BossId id)
		{
			Condition = BossConditionFactory.Create(_condition);
			_condition = null;
		}
	}

	public class BossPassiveBalanceData : BossSkillCommonBalanceData
	{
		public BossPassiveLocalKey Key;
	}

	public class BossSkillBalanceData : BossSkillCommonBalanceData
	{
		public BossSkillLocalKey Key;
	}
	
	public class BossBalanceData
	{
		[JsonIgnore]
		public BossId Id;
		public string Name;

		[JsonInclude]
		private JsonStats _stats;
		[JsonIgnore]
		public Stats Stats;

		[JsonInclude]
		private List<BossPassiveBalanceData> _passives;
		[JsonIgnore]
		public Dictionary<BossPassiveLocalKey, BossPassiveBalanceData> Passives;

		[JsonInclude]
		private List<BossSkillBalanceData> _skills;
		[JsonIgnore]
		public Dictionary<BossSkillLocalKey, BossSkillBalanceData> Skills;

		public void Build(BossId id)
		{
			Id = id;
			Stats = _stats;
			Passives = _passives.ToDictionary(data =>
			{
				data.Build(id);
				return data.Key;
			});
			Skills = _skills.ToDictionary(data =>
			{
				data.Build(id);
				return data.Key;
			});
			_skills = null;
		}
	}

	public class BossBalance : Balance<Dictionary<string, BossBalanceData>>
	{
		public static readonly BossBalance _ = new BossBalance();

		protected BossBalance() : base("boss")
		{}

		public BossBalanceData Find(BossId id)
		{
			BossBalanceData ret;
			Data.TryGet(id.ToString(), out ret);
			return ret;
		}

		protected override void AfterLoad(bool success)
		{
			if (!success) return;
			foreach (var kv in Data)
			{
				var id = EnumHelper.ParseOrDefault<BossId>(kv.Key);
				kv.Value.Build(id);
			}
		}
	}
}