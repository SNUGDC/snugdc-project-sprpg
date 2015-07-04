using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;
		private Battle _battle;

		void Start()
		{
			Debug.Assert(Def != null);

			if (Def != null)
				_battle = new Battle(Def);
		}

#if UNITY_EDITOR
		public Battle EditBattle()
		{
			return _battle;
		}
#endif
	}
}
