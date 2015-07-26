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
		Step1MeltDown,
	}
	
	public enum BossSkillLocalKey
	{
		Attack,
		MassAttack,
		Radioactivity,
	}

	public class BossPassiveBalanceData
	{
		public BossPassiveLocalKey Key;
		public JsonData Arguments;
	}

	public class BossSkillBalanceData
	{
		public BossSkillLocalKey Key;
		public Weight Weight;
		public Tick Duration;
		[JsonInclude] private JsonData _sampleCondition;
		[JsonIgnore] public Condition SampleCondition;
		public JsonData Arguments;
		
		public void Build(BossId id)
		{
			SampleCondition = ConditionFactory.Create(_sampleCondition);
			_sampleCondition = null;
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
			Passives = _passives.ToDictionary(data => data.Key);
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