using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;
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
			Battle.Update(Time.deltaTime);
		}
	}
}
