using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		public StageId Stage;
		public bool RealtimeEnabled;
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
			};

			def.RealtimeEnabled = RealtimeEnabled;
			BattleWrapper.Def = def;
		}
	}
}