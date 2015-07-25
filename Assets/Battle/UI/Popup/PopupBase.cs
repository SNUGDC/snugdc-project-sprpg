using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PopupBase : MonoBehaviour
	{
		private readonly PopupFsm _fsm = new PopupFsm();

		// will be cleared after close.
		public Action CloseCallback;

		public bool TryOpen(RectTransform parent)
		{
			var ret = _fsm.TryOpen();
			if (ret) transform.SetParent(parent, false);
			return ret;
		}

		// for inspector
		public void TryCloseNoReturn(bool destroyGameObject)
		{
			TryClose(destroyGameObject);
		}

		public bool TryClose(bool destroyGameObject)
		{
			var ret = _fsm.TryClose();
			if (ret)
			{
				if (destroyGameObject) 
					Destroy(gameObject);
				CloseCallback.CheckAndCall();
				CloseCallback = null;
			}
			return ret;
		}
	}
}
