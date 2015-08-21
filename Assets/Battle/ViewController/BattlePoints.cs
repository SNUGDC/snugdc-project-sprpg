using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BattlePoints : MonoBehaviour
	{
		public static BattlePoints _ { get; private set; }

		public Transform LeaderCenter;
		public Transform LeaderBottom;
		public Transform LeaderFront;
		public Transform LeaderBack;

		public Transform BossCenter;
		public Transform BossBottom;
		public Transform BossFront;
		public Transform BossBack;

		void Start()
		{
			_ = this;
		}

		void OnDestroy()
		{
			if (_ == this)
				_ = null;
		}

		public GameObject InstantiateOnLeaderCenter(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(LeaderCenter); }
		public GameObject InstantiateOnLeaderBottom(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(LeaderBottom); }
		public GameObject InstantiateOnLeaderFront(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(LeaderFront); }
		public GameObject InstantiateOnLeaderBack(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(LeaderBack); }

		public GameObject InstantiateOnBossCenter(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(BossCenter); }
		public GameObject InstantiateOnBossBottom(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(BossBottom); }
		public GameObject InstantiateOnBossFront(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(BossFront); }
		public GameObject InstantiateOnBossBack(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(BossBack); }
	}
}