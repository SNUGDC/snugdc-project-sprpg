using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleWrapper : MonoBehaviour
	{
		public static BattleDef Def;
		public Battle Battle { get; private set; }

		[SerializeField]
		private View.ViewController _viewController;
		[SerializeField]
		private View.BattleController _battleController;
		[SerializeField]
		private View.HudController _hudController;

		void Start()
		{
			Debug.Assert(Def != null);
			if (Def == null) return;
			Battle = new Battle(Def);
			Def = null;

			_viewController.enabled = true;
			_battleController.enabled = true;
			_hudController.enabled = true;

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
