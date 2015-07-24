using Gem;

namespace SPRPG.Battle.View
{
	public static class PopupFactory
	{
		public static ResultWinPopup OpenResultWin()
		{
			return Assets._.ResultWinPopup.Instantiate();
		}

		public static ResultLosePopup OpenResultLose()
		{
			return Assets._.ResultLosePopup.Instantiate();
		}
	}
}
