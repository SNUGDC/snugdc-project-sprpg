using System.Collections.Generic;
using Gem;
using LitJson;

namespace SPRPG.Battle
{
	public class ConditionDictionary : Balance<Dictionary<string, JsonData>>
	{
		public static readonly ConditionDictionary _ = new ConditionDictionary();

		private Dictionary<string, Condition> _dic;

		protected ConditionDictionary() : base("condition") { }

		public Condition Find(string key)
		{
			Condition ret;
			_dic.TryGet(key, out ret);
			return ret;
		}

		protected override void AfterLoad(bool success)
		{
			if (!success) return;
			_dic = Data.Map((key, value) => new KeyValuePair<string, Condition>(key, ConditionFactory.Create(value)));
			Data.Clear();
		}
	}
}
