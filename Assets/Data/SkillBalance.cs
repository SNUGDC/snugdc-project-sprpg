using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;

namespace SPRPG
{
	public class SkillBalanceData
	{
		public struct ViewActsDef
		{
			public Battle.ActData OnStart;
		}

		public struct ViewActs_
		{
			public Battle.Act OnStart;

			public ViewActs_(ViewActsDef def) 
			{
				OnStart = Battle.ActFactory.Create(def.OnStart);
			}
		}

		public SkillKey Key;
		public string Name;
		public string Icon;
		public SkillTier Tier;
		public CharacterId MajorUser;
		public StringOrDictionaryText DescriptionFormat;
		public SkillDescriptor Descriptor;
		[JsonInclude]
		private ViewActsDef? _viewActs;
		public ViewActs_ ViewActs;
		public JsonData Arguments;

		public string Describe()
		{
			if (Descriptor == null)
				return "undefined descriptor";
			return Descriptor(this);
		}

		public void Build()
		{
			if (_viewActs.HasValue) ViewActs = new ViewActs_(_viewActs.Value);
			Descriptor = SkillDescriptorFactory.Create(Key);
		}
	}

	public sealed class SkillBalance : Balance<List<SkillBalanceData>>
	{
		public static readonly SkillBalance _ = new SkillBalance();
		private Dictionary<SkillKey, SkillBalanceData> _dic;

		private SkillBalance() : base("skill")
		{}

		protected override void AfterLoad(bool success)
		{
			if (!success)
			{
				_dic = new Dictionary<SkillKey, SkillBalanceData>();
				return;
			}

			_dic = Data.ToDictionary(element => element.Key);
			Data.Clear();

			foreach (var kv in _dic)
				kv.Value.Build();
		}

		public SkillBalanceData Find(SkillKey key)
		{
			SkillBalanceData ret;
			_dic.TryGet(key, out ret);
			return ret;
		}
	}
}