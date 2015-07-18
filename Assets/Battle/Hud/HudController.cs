using UnityEngine;

namespace SPRPG.Battle
{
	public class HudController : MonoBehaviour
	{
		public static HudController _ { get; private set; }
		public static Battle Context { get { return _._battleWrapper.Battle; } }
		
		private BattleWrapper _battleWrapper;

		[SerializeField]
		private HudClock _clock;
		
		void Start()
		{
			Debug.Assert(_ == null);
			_ = this;

			_battleWrapper = FindObjectOfType<BattleWrapper>();
		}

		void OnDestroy()
		{
			Debug.Assert(_ == this);
			_ = null;
		}

		void Update()
		{
			_clock.RefreshTime(_battleWrapper.Battle.PlayerClock.Relative);
		}
	}
}