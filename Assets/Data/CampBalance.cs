using System.Collections.Generic;
using System.Linq;
using LitJson;

namespace SPRPG
{
	public class CampData
	{
		public class Character_
		{
			public Vector2 Position;
		}

		public List<Character_> Characters;
		public Dictionary<CharacterID, Character_> CharacterDic;

		public void Build()
		{
			var i = 0;
			CharacterDic = Characters.ToDictionary(character => (CharacterID)i++);
		}
	}

	public class CampBalance : Balance<CampData>
	{
		public static readonly CampBalance _ = new CampBalance();

		protected CampBalance() : base("camp")
		{}

		protected override void AfterLoad(bool success)
		{
			base.AfterLoad(success);
			Data.Build();
		}
	}
}