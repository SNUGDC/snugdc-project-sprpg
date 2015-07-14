#if UNITY_EDITOR

using UnityEngine;

namespace SPRPG.Battle
{
	public class PartyDebugView : MonoBehaviour
	{
		public BattleWrapper BattleWrapper;

		public Battle Battle { get { return BattleWrapper.Battle; } }
	}
}

#endif