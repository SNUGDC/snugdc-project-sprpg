using UnityEngine;

namespace SPRPG.Profile
{
	public class ProfileControl : MonoBehaviour 
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
