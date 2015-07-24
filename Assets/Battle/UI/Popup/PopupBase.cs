using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PopupBase : MonoBehaviour
	{
		private readonly PopupFsm _fsm = new PopupFsm();

		public bool TryOpen(RectTransform parent)
		{
			var ret = _fsm.TryOpen();
			if (ret) transform.SetParent(parent, false);
			return ret;
		}

		public bool TryClose(bool destroyGameObject)
		{
			var ret = _fsm.TryClose();
			if (destroyGameObject) 
				Destroy(gameObject);
			return ret;
		}
	}
}
