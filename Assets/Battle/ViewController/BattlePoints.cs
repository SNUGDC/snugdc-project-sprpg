using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BattlePoints : MonoBehaviour
	{
		public static BattlePoints _ { get; private set; }

		public Transform LeaderFront;
		public Transform LeaderBack;
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

		public GameObject InstantiateOnLeaderFront(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(LeaderFront);
		}

		public GameObject InstantiateOnLeaderBack(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(LeaderBack);
		}

		public GameObject InstantiateOnBossFront(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(BossFront);
		}
	
		public GameObject InstantiateOnBossBack(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(BossBack);
		}
	}
}