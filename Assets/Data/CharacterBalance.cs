using System.Collections.Generic;
using Gem;
using LitJson;

namespace SPRPG
{
	public class CharacterBalanceData
	{
		public JsonData Special;
	}

	public class ArcherSpecialBalanceData
	{
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
	}
}