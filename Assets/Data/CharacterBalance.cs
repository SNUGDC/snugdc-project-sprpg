using System.Collections.Generic;
using Gem;
using LitJson;

namespace SPRPG
{
	public class CharacterBalanceData
	{
		[JsonIgnore]
		public CharacterId Id;
		public string Detail;
		public JsonStats Stats;
		public PassiveKey Passive;
		public SkillKey[] SkillSetDefault;
		public JsonData Special;
	}

	public class ArcherSpecialBalanceData
	{
		public int ArrowInitial;
		public int ArrowMax;
		public Tick ArrowRechargeCooltime;
	}

	public class CharacterBalance : Balance<Dictionary<string, CharacterBalanceData>>
	{
		public static readonly CharacterBalance _ = new CharacterBalance();
		
		protected CharacterBalance() : base("character")
		{ }
	
		public CharacterBalanceData Find(CharacterId id)
		{
			CharacterBalanceData ret;
			Data.TryGet(id.ToString(), out ret);
			return ret;
		}

		protected override void AfterLoad(bool success)
		{
			if (!success) return;
			foreach (var kv in Data)
				EnumHelper.TryParse(kv.Key, out kv.Value.Id);
		}
	}
}