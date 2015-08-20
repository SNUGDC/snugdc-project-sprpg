﻿using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudController : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;
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

		void Start()
		{
			_pauseButton.ForceSetToggle(!Context.RealtimeEnabled);
			_pauseButton.OnToggle += TogglePause;

			_characterHpBarControllers = new HudHpBarController<Character>[SPRPG.Party.Size];
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var member = Context.Party[idx];
				var hpBar = _characterHpBars[idx.ToArrayIndex()];
				hpBar.SetIcon(R.Character.LoadIcon(member.Id));
				_characterHpBarControllers[idx.ToArrayIndex()] = new HudHpBarController<Character>(hpBar, member);
			}

			var bossIcon = R.Boss.GetIcon(Context.Boss.Id);
			if (bossIcon) _bossHpBar.SetIcon(bossIcon);
			_bossHpBarController = new HudHpBarController<Boss>(_bossHpBar, Context.Boss);
		}

		void OnDestroy()
		{
			_pauseButton.OnToggle -= TogglePause;
		}

		void Update()
		{
			if (!Context.Fsm.IsResult)
			{
				var termAndDistance = Context.PlayerClock.GetCurrentTermAndDistance();
				var skillSlot = termAndDistance.Term.ToSkillSlot();
				var skillActor = Context.Party.Leader.SkillManager[skillSlot];
				var progress = (int)termAndDistance.Distance / (float)(int)Const.Term;
				_clock.Refresh(skillActor.Key, skillSlot, progress);
			}
		}

		private void TogglePause(bool shouldPause)
		{
			Context.RealtimeEnabled = !shouldPause;
		}
	}
}