using System.Linq;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudController : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;
		[SerializeField]
		private Canvas _overlay;
		[SerializeField]
		private BattleController _battleController;
		private Battle Context { get { return _battleWrapper.Battle; } }
		
		[SerializeField]
		private HudClock _clock;
		[SerializeField]
		private HudPauseButton _pauseButton;

		[SerializeField]
		private HudHpBar[] _characterHpBars;
		[SerializeField]
		private HudHpBar _bossHpBar;

		private HudHpBarController<Character>[] _characterHpBarControllers;
		private HudHpBarController<Boss> _bossHpBarController;

		[SerializeField]
		private HudNumberPoper _numberPoperPrefab;
		private HudNumberPoper[] _characterNumberPopers;
		[SerializeField]
		private HudNumberPoper _bossNumberPoper;

		void Start()
		{
			_pauseButton.ForceSetToggle(!Context.RealtimeEnabled);
			_pauseButton.OnToggle += TogglePause;

			_characterHpBarControllers = new HudHpBarController<Character>[SPRPG.Party.Size];
			_characterNumberPopers = new HudNumberPoper[SPRPG.Party.Size];
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var member = Context.Party[idx];

				var hpBar = _characterHpBars[idx.ToArrayIndex()];
				hpBar.SetIcon(R.Character.LoadIcon(member.Id));
				_characterHpBarControllers[idx.ToArrayIndex()] = new HudHpBarController<Character>(hpBar, member);

				var numberPoper = _numberPoperPrefab.Instantiate();
				numberPoper.transform.SetParent(_overlay.transform, false);
				_characterNumberPopers[idx.ToArrayIndex()] = numberPoper;

				var idxCpy = idx;
				Context.Party[idx].OnAfterHeal += (character, hp) => OnCharacterHeal(idxCpy, character, hp);
				Context.Party[idx].OnAfterHit += (character, damage) => OnCharacterHit(idxCpy, character, damage);
			}

			var bossIcon = R.Boss.GetIcon(Context.Boss.Id);
			if (bossIcon) _bossHpBar.SetIcon(bossIcon);
			_bossHpBarController = new HudHpBarController<Boss>(_bossHpBar, Context.Boss);

			Context.Boss.OnAfterHeal += OnBossHeal;
			Context.Boss.OnAfterHit += OnBossHit;
		}

		void OnDestroy()
		{
			_pauseButton.OnToggle -= TogglePause;
			Context.Boss.OnAfterHit -= OnBossHit;
		}

		void Update()
		{
			if (!Context.Fsm.IsResult)
				UpdateClock();
			UpdatePositions();
		}

		void UpdateClock()
		{
			var termAndDistance = Context.PlayerClock.GetCurrentTermAndDistance();
			var skillSlot = termAndDistance.Term.ToSkillSlot();
			var skillActor = Context.Party.Leader.SkillManager[skillSlot];
			var progress = (int)termAndDistance.Distance / (float)(int)Const.Term;
			_clock.Refresh(skillActor.Key, skillSlot, progress);
		}

		void UpdatePositions()
		{
			for (var i = 0; i != _characterHpBars.Count(); ++i)
			{
				var idx = BattleHelper.MakeOriginalPartyIdxFromIndex(i);
				var characterView = _battleController.PartyView[idx];

				var hpBar = _characterHpBars[i];
				hpBar.transform.position = characterView.transform.position;
				hpBar.transform.Translate(new Vector3(-0.4f, 1.2f));

				var poper = _characterNumberPopers[i];
				poper.transform.position = characterView.transform.position;
				poper.transform.Translate(new Vector3(0, 0.6f));
			}
		}

		private void TogglePause(bool shouldPause)
		{
			Context.RealtimeEnabled = !shouldPause;
			_battleController.PartyView.SetAnimatorEnabled(!shouldPause);
			_battleController.BossView.Animator.enabled = !shouldPause;
		}

		private void OnCharacterHeal(OriginalPartyIdx idx, Character character, Hp hp)
		{
			_characterNumberPopers[idx.ToArrayIndex()].PopHeal(hp);
		}

		private void OnCharacterHit(OriginalPartyIdx idx, Character character, Damage damage)
		{
			_characterNumberPopers[idx.ToArrayIndex()].PopDamage(damage);
		}

		private void OnBossHeal(Boss boss, Hp hp)
		{
			_bossNumberPoper.PopHeal(hp);
		}

		private void OnBossHit(Boss boss, Damage damage)
		{
			_bossNumberPoper.PopDamage(damage);
		}
	}
}