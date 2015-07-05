using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;

namespace SPRPG.Battle
{
	public enum BossSkillLocalKey
	{
		Attack,
	}

	public class BossSkillBalanceData
	{
		public BossSkillLocalKey Key;
		public Proportion Proportion;
		public Tick Duration;

		[JsonInclude]
		private JsonData _condition;
		[JsonIgnore]
		public BossCondition Condition;

		public JsonData Arguments;

		public void Build(BossId id)
		{
			Condition = BossConditionFactory.Create(_condition);
			_condition = null;
		}
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
		private List<BossSkillBalanceData> _skills;
		[JsonIgnore]
		public Dictionary<BossSkillLocalKey, BossSkillBalanceData> Skills;

		public void Build(BossId id)
		{
			Id = id;
			Stats = _stats;
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