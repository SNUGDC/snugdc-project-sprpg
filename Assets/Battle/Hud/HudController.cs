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
		private HudHpBar[] _hpBars;
		[SerializeField]
		private HudHpBar _bossHpBar;

		[SerializeField]
		private PartyView _party;
		private PartyPlacer _partyPlacer;
		
		void Start()
		{
			Debug.Assert(_ == null);
			_ = this;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var member = Context.Party[idx];
				_party.InstantiateAndAssign(idx, member.Data);
				_hpBars[idx.ToArrayIndex()].MaxHp = member.HpMax;
			}
			_party.gameObject.SetActive(true);
			
			_partyPlacer = new PartyPlacer(Context.Party, _party);
			_partyPlacer.ResetPosition();

			_bossHpBar.MaxHp = Context.Boss.HpMax;

			Events.AfterTurn += AfterTurn;
			Events.Boss.OnHpChanged += OnBossHpChanged;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var characterEvents = Events.GetCharacter(idx);
				characterEvents.OnHpChanged += OnCharacterHpChanged;
				characterEvents.OnSkillStart += OnSomeCharacterSkillStart;
			}

			Events.OnWin += OnWin;
			Events.OnLose += OnLose;

#if UNITY_EDITOR
			DebugLogger.Init();
#endif
		}

		void OnDestroy()
		{
			Events.AfterTurn -= AfterTurn;
			Events.Boss.OnHpChanged -= OnBossHpChanged;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var characterEvents = Events.GetCharacter(idx);
				characterEvents.OnHpChanged -= OnCharacterHpChanged;
				characterEvents.OnSkillStart -= OnSomeCharacterSkillStart;
			}

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

		private void OnCharacterHpChanged(OriginalPartyIdx idx, Character character, Hp oldHp)
		{
			_hpBars[idx.ToArrayIndex()].SetHp(character.Hp);
		}

		private void OnSomeCharacterSkillStart(OriginalPartyIdx idx, Character character, SkillActor skill)
		{
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
			Transition.TransferToPreviousScene(SceneType.Battle);
		}
	}
}