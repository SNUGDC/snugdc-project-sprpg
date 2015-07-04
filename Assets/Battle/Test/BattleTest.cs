using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleTest : MonoBehaviour
	{
		void Awake()
		{
			Battle.DefToInit = new BattleDef()
			{
#if BALANCE
				UseDataInput = false,
#endif
			};
		}
	}
}