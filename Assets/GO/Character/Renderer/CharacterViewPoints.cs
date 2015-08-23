using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterViewPoints : MonoBehaviour
	{
		[SerializeField]
		private Transform _center;
		public Transform Center { get { return _center; } }
		[SerializeField]
		private Transform _bottom;
		[SerializeField]
		private Transform _aim;
		[SerializeField]
		private Transform _buffTop;

		public GameObject InstantiateOnCenter(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(_center);
		}

		public GameObject InstantiateOnBottom(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(_bottom);
		}

		public GameObject InstantiateOnAim(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(_aim);
		}

		public GameObject InstantiateOnBuffTop(GameObject prefab)
		{
			return prefab.InstantiateToParentAndToggleActive(_buffTop);
		}
	}
}
