using System;
using UnityEngine;

namespace SPRPG.Battle
{
	[Serializable]
	public struct TestPartyDef
	{
		public CharacterId _1;
		public CharacterId _2;
		public CharacterId _3;

		private static CharacterDef MakeCharacterDef(CharacterBalanceData data)
		{
			return new CharacterDef(data, data.SkillSetDefault);
		}

		public PartyDef Build()
		{
			var balance = CharacterBalance._;
			return new PartyDef(
				MakeCharacterDef(balance.Find(_1)), 
				MakeCharacterDef(balance.Find(_2)), 
				MakeCharacterDef(balance.Find(_3)));
		}
	}

	public class BattleTest : MonoBehaviour
	{
		public StageId Stage;
		public bool PlayImmediate;
		public TestPartyDef Party;

		void Start()
		{
			if (BattleWrapper.Def != null)
				return;

			var def = new BattleDef(Stage, Party.Build())
			{
#if UNITY_EDITOR
				UseDataInput = false,
#endif
			};

			def.PlayImmediate = PlayImmediate;
			BattleWrapper.Def = def;
		}
	}
}