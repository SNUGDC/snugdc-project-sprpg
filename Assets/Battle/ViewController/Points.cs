using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class Points : MonoBehaviour
	{
		public Transform LeaderFront;
		public Transform LeaderBack;
		public Transform BossFront;
		public Transform BossBack;

		public GameObject InstantiateOnLeaderFront(GameObject prefab)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(LeaderFront, false);
			return go;
		}

		public GameObject InstantiateOnLeaderBack(GameObject prefab)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(LeaderBack, false);
			return go;
		}

		public GameObject InstantiateOnBossFront(GameObject prefab)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(BossFront, false);
			return go;
		}
	
		public GameObject InstantiateOnBossBack(GameObject prefab)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(BossBack, false);
			return go;
		}
	}
}