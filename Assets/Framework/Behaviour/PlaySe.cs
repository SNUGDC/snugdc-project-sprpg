using UnityEngine;

namespace SPRPG
{
	public class PlaySe : MonoBehaviour
	{
		public bool ExecuteOnStart = true;
		public bool DestroyComponentAfterExecute = true;
		public AudioClip AudioClip;

		void Start()
		{
			if (ExecuteOnStart)
				Execute();
		}

		public void Execute()
		{
			SePlayer._.Play(AudioClip);
			if (DestroyComponentAfterExecute)
				Destroy(this);
		}
	}
}