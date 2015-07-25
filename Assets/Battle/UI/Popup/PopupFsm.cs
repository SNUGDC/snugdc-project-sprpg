using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PopupFsm
	{
		public bool IsOpened { get; private set; }
		public bool IsClosed { get { return !IsOpened; } }

		public PopupFsm(bool isOpened = false)
		{
			IsOpened = isOpened;
		}

		public void JustSetOpened()
		{
			IsOpened = true;
		}

		public bool TryOpen()
		{
			if (!IsClosed)
			{
				Debug.LogError("not closed.");
				return false;
			}

			JustSetOpened();
			return true;
		}

		public bool TryClose()
		{
			if (!IsOpened)
			{
				Debug.LogError("not opened.");
				return false;
			}

			IsOpened = false;
			return true;
		}
	}
}