using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BgmPlayer : MonoBehaviour
	{
		private const float Delay = 0.8f;

		[SerializeField]
		private BattleWrapper _battleWrapper;
		private Battle _context { get { return _battleWrapper.Battle; } }
		[SerializeField]
		private AudioSource _player;
		[SerializeField]
		private List<AudioClip> _bgms;

		void Start()
		{
			_player.Pause();
			Play(BossHelper.MakeBossPhaseFromIndex(0));
			OnPlayingChanged(_context.IsPlaying);

			_context.Boss.Phase.OnChanged += Play;
			Events.OnPlayingChanged += OnPlayingChanged;
		}

		void OnDestroy()
		{
			_context.Boss.Phase.OnChanged -= Play;
			Events.OnPlayingChanged -= OnPlayingChanged;
		}

		private void Play(BossPhaseState state)
		{
			if (state.ToIndex() >= _bgms.Count)
			{
				Debug.LogError("phase exceed bgm exceed.");
				state = BossHelper.MakeBossPhaseFromIndex(_bgms.Count - 1);
			}

			_player.clip = _bgms[state.ToIndex()];
			_player.Play((ulong)(_player.clip.frequency * Delay));
		}

		private void OnPlayingChanged(bool isPlaying)
		{
			if (isPlaying)
				_player.UnPause();
			else
				_player.Pause();
		}
	}
}