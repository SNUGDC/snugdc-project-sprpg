using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		public StageId Stage;
		public PartyDef Party;

		void Awake()
		{
			BattleWrapper.Def = new BattleDef(Stage, Party)
			{
#if BALANCE
				UseDataInput = false,
#endif
			};
		}
	}
}