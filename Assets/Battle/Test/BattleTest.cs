using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		void Awake()
		{
			Battle.DefToInit = new BattleDef(new PartyDef
			{
				_1 = (CharacterID)0,
				_2 = (CharacterID)1,
				_3 = (CharacterID)2,
			})
			{
#if BALANCE
				UseDataInput = false,
#endif
			};
		}
	}
}