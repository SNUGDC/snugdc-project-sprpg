using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;

		public bool RealtimeEnabled = true;

		private Battle _battle;
		private BattleRealtime _realtime;

		void Start()
		{
			Debug.Assert(Def != null);
			if (Def == null)
				return;

			_battle = new Battle(Def);
			_realtime = new BattleRealtime(_battle);
			Def = null;
		}

		void Update()
		{
			_battle.Update(Time.deltaTime);

			if (RealtimeEnabled)
				_realtime.Update(Time.deltaTime);
		}

#if UNITY_EDITOR
		public Battle EditBattle()
		{
			return _battle;
		}
#endif
	}
}
