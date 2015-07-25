using UnityEngine;

namespace SPRPG.UI
{
	public class BackButton : MonoBehaviour
	{
		public void Back()
		{
			Transition.TransferToPreviousScene(SceneType.Camp);
		}
	}
}