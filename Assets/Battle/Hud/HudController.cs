using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudController : MonoBehaviour
	{
		public static HudController _ { get; private set; }
		public static Battle Context { get { return _._battleWrapper.Battle; } }
		
		[SerializeField]
		private BattleWrapper _battleWrapper;
		private Battle _battle { get { return _battleWrapper.Battle; } }
		
		[SerializeField]
		private RectTransform _overlay;

		[SerializeField]
		private HudClock _clock;
		[SerializeField]
		private HudHpBar _bossHpBar;

		[SerializeField]
		private PartyView _party;
		private PartyPlacer _partyPlacer;

		[SerializeField]
		private HudHpBar[] _hpBars;

		
		void Start()
		{
			Debug.Assert(_ == null);
			_ = this;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				_party[idx].SetInitData(Context.Party[idx].Data);
			_party.gameObject.SetActive(true);
			
			_partyPlacer = new PartyPlacer(Context.Party, _party);
			_partyPlacer.ResetPosition();

			Events.AfterTurn += AfterTurn;
			Events.OnBossHpChanged += OnBossHpChanged;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				HudEvents.GetOnCharacterHpChanged(idx).Value.Action += OnSomeCharacterHpChanged;

			Events.OnWin += OnWin;
			Events.OnLose += OnLose;

#if UNITY_EDITOR
			DebugLogger.Init();
#endif
		}

		private void OnSomeCharacterHpChanged(OriginalPartyIdx idx, Character character, Hp oldHp)
		{
			_hpBars[idx.ToArrayIndex()].SetHp(character.Hp);
		}

		void OnDestroy()
		{
			Events.AfterTurn -= AfterTurn;
			Events.OnBossHpChanged -= OnBossHpChanged;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				HudEvents.GetOnCharacterHpChanged(idx).Value.Action -= OnSomeCharacterHpChanged;

			Events.OnWin -= OnWin;
			Events.OnLose -= OnLose;

			Debug.Assert(_ == this);
			_ = null;
		}

		void Update()
		{
			if (!_battle.Fsm.IsResult)
				_clock.RefreshTime(_battle.PlayerClock.Relative);
		}

		public void AfterTurn()
		{
			_partyPlacer.AfterTurn();
		}

		private void OnBossHpChanged(Boss boss, Hp oldHp)
		{
			_bossHpBar.SetHp(boss.Hp);
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
			Transition.TransferToWorld();
		}
	}
}