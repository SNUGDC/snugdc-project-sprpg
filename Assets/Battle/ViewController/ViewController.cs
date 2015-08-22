using UnityEngine;

namespace SPRPG.Battle.View
{
	public class ViewController : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;

		[SerializeField]
		private RectTransform _overlay;
		[SerializeField]
		private HudController _hud;
		[SerializeField]
		private BattleController _battle;

		void Start()
		{
			Events.AfterTurn += AfterTurn;
			Events.OnWin += OnWin;
			Events.OnLose += OnLose;

#if UNITY_EDITOR
			DebugLogger.Init();
#endif
		}

		void OnDestroy()
		{
			Events.AfterTurn -= AfterTurn;
			Events.OnWin -= OnWin;
			Events.OnLose -= OnLose;
		}

		private void AfterTurn()
		{
			_battle.AfterTurn();
		}

		private void OnWin()
		{
			var popup = PopupOpener.OpenResultWin(_overlay);
			popup.CloseCallback += TransferAfterResult;
		}

		private void OnLose()
		{
			var popup = PopupOpener.OpenResultLose(_overlay);
			popup.CloseCallback += TransferAfterResult;
		}

		private void TransferAfterResult()
		{
			Transition.TransferToPreviousScene(SceneType.World);
			Transition.ClearLog();
		}
	}
}
