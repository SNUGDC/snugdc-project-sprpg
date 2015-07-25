using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public static class PopupOpener
	{
		public static ResultWinPopup OpenResultWin(RectTransform parent)
		{
			var ret = Assets._.ResultWinPopup.Instantiate();
			ret.TryOpen(parent);
			return ret;
		}

		public static ResultLosePopup OpenResultLose(RectTransform parent)
		{
			var ret = Assets._.ResultLosePopup.Instantiate();
			ret.TryOpen(parent);
			return ret;
		}
	}
}
