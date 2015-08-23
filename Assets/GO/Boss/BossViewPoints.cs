using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossViewPoints : MonoBehaviour
	{
		public Transform Center;
		public Transform FrontGround;
		public Transform AboveHead;

		public GameObject InstantiateOnCenter(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(Center); }
		public GameObject InstantiateOnFrontGround(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(FrontGround); }
		public GameObject InstantiateOnAboveHead(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(AboveHead); }
	}
}