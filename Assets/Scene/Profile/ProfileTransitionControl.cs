using UnityEngine;

namespace SPRPG.Profile
{
	public class ProfileTransitionControl : MonoBehaviour 
	{
		public void Back()
		{
			Transition.TransferToCamp();
		}

		public void TransferToSetting()
		{
			Transition.TransferToSetting();
		}
	}
}
