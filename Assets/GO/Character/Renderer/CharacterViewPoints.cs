using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterViewPoints : MonoBehaviour
	{
		[SerializeField]
		private Transform Aim;

		public GameObject InstantiateOnAim(GameObject prefab)
		{
			var go = prefab.Instantiate();
			go.transform.SetParent(Aim, false);
			
			// prevent mysterious initial particle curve.
			var activeOrg = go.activeSelf;
			if (activeOrg)
			{
				go.SetActive(false);
				go.SetActive(true);
			}

			return go;
		}
	}
}
