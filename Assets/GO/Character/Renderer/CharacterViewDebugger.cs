using UnityEngine;

namespace SPRPG
{
	public class CharacterViewDebugger : MonoBehaviour
	{
		[HideInInspector]
		public CharacterView View;

		void Start()
		{
			View = GetComponent<CharacterView>();
		}
	}
}
