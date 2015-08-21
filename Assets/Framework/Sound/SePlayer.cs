using System.Collections.Generic;
using UnityEngine;

namespace SPRPG
{
	public class SePlayer : Singleton<SePlayer>
	{
		private readonly List<AudioSource> _playings = new List<AudioSource>();

		private void Update()
		{
			_playings.RemoveAll(playing =>
			{
				if (playing.isPlaying) return false;
				Destroy(playing);
				return true;
			});
		}

		public void Play(AudioClip clip)
		{
			var audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.Play();
			_playings.Add(audioSource);
		}
	}
}