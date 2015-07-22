using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		public StageId Stage;
		public PartyDef Party;

		void Awake()
		{
			if (BattleWrapper.Def != null)
				return;

			var def = new BattleDef(Stage, Party)
			{
#if BALANCE
				UseDataInput = false,
#endif
				RealtimeEnabled = false,
			};

			BattleWrapper.Def = def;
		}
	}
}