using UnityEngine;

namespace SPRPG
{
	public class StartControl : MonoBehaviour 
	{
		private void Start()
		{
			BgmPlayer._.PlayUiBgm();
		}

		public void StartToCamp()
		{
			Transition.TransferToCamp();
		}
	}
}
