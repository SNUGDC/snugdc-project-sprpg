using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		public StageId Stage;

		void Awake()
		{
			var party = new PartyDef
			{
				_1 = (CharacterID) 0,
				_2 = (CharacterID) 1,
				_3 = (CharacterID) 2,
			};

			BattleWrapper.Def = new BattleDef(Stage, party)
			{
#if BALANCE
				UseDataInput = false,
#endif
			};
		}
	}
}