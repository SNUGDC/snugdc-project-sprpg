using UnityEngine;

namespace SPRPG
{
	public class BgmPlayer : Singleton<BgmPlayer>
	{
		[SerializeField]
		private AudioSource _audioSource;

		public static void Init()
		{
			var awake = _;
		}

		private void Awake()
		{
			_audioSource = gameObject.AddComponent<AudioSource>();
		}

		public void Play(AudioClip clip, bool replayEvenIfSame = false)
		{
			if (_audioSource.clip == clip && !replayEvenIfSame)
				return;
			_audioSource.clip = clip;
			_audioSource.Play();
		}

		public void Stop()
		{
			_audioSource.Stop();
		}
	}

	public static class BgmPlayerHelper
	{
		public static void PlayUiBgm(this BgmPlayer thiz)
		{
			thiz.Play(Assets._.BgmUi);
		}
	}
}