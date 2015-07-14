using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;

namespace SPRPG
{
	public class SkillBalanceData
	{
		public SkillKey Key;
		public string Name;
		public SkillTear Tear;
		public CharacterId MajorUser;
		public string DescriptionFormat;
		public SkillDescriptor Descriptor;
		public JsonData Arguments;

		public string Describe()
		{
			if (Descriptor == null)
				return "undefined descriptor";
			return Descriptor(this);
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
				kv.Value.Descriptor = SkillDescriptorFactory.Create(kv.Key);
		}

		public SkillBalanceData Find(SkillKey key)
		{
			SkillBalanceData ret;
			_dic.TryGet(key, out ret);
			return ret;
		}
	}
}