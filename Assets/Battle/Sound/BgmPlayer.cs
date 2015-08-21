using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BgmPlayer : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;
		private Battle _context { get { return _battleWrapper.Battle; } }
		[SerializeField]
		private AudioSource _player;
		[SerializeField]
		private List<AudioClip> _bgms;

		void Start()
		{
			Play(BossHelper.MakeBossPhaseFromIndex(0));
			_context.Boss.Phase.OnChanged += Play;
		}

		void OnDestroy()
		{
			_context.Boss.Phase.OnChanged -= Play;
		}

		private void Play(BossPhaseState state)
		{
			if (state.ToIndex() >= _bgms.Count)
			{
				Debug.LogError("phase exceed bgm exceed.");
				state = BossHelper.MakeBossPhaseFromIndex(_bgms.Count - 1);
			}

			_player.clip = _bgms[state.ToIndex()];
			_player.Play();
		}
	}
}