using SPRPG.Battle.View;
using UnityEngine;

namespace SPRPG
{
	public class BossViewDebugger : MonoBehaviour
	{
		[HideInInspector]
		public BossView View;

		void Start()
		{
			View = GetComponent<BossView>();
		}
	}
}