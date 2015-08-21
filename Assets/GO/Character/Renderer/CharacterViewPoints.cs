using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterViewPoints : MonoBehaviour
	{
		[SerializeField]
		private Transform Aim;
		[SerializeField]
		private Transform BuffTop;

		private static GameObject Instantiate(GameObject prefab, Transform parent)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(parent, false);
			
			// prevent mysterious initial particle curve.
			var activeOrg = go.activeSelf;
			if (activeOrg)
			{
				go.SetActive(false);
				go.SetActive(true);
			}

			return go;
		}

		public GameObject InstantiateOnAim(GameObject prefab)
		{
			return Instantiate(prefab, Aim);
		}

		public GameObject InstantiateOnBuffTop(GameObject prefab)
		{
			return Instantiate(prefab, BuffTop);
		}
	}
}
