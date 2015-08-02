using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;

		public bool RealtimeEnabled = true;

		public Battle Battle { get; private set; }

		void Start()
		{
			Debug.Assert(Def != null);
			if (Def == null) return;
			Battle = new Battle(Def);
			Def = null;

#if UNITY_EDITOR
			var partyDebugView = gameObject.AddComponent<PartyDebugView>();
			partyDebugView.BattleWrapper = this;

			var bossDebugView = gameObject.AddComponent<BossDebugView>();
			bossDebugView.BattleWrapper = this;
#endif
		}

		void Update()
		{
			Battle.RealtimeEnabled = RealtimeEnabled;
			Battle.Update(Time.deltaTime);
		}
	}
}
