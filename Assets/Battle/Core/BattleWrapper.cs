using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;

		public bool RealtimeEnabled = true;

		public Battle Battle { get; private set; }
		private BattleRealtime _realtime;

		void Start()
		{
			Debug.Assert(Def != null);
			if (Def == null)
				return;

			Battle = new Battle(Def);
			_realtime = new BattleRealtime(Battle);
			Def = null;

#if UNITY_EDITOR
			var partyDebugView = gameObject.AddComponent<PartyDebugView>();
			partyDebugView.BattleWrapper = this;
#endif
		}

		void Update()
		{
			Battle.Update(Time.deltaTime);

			if (RealtimeEnabled)
				_realtime.Update(Time.deltaTime);
		}
	}
}
