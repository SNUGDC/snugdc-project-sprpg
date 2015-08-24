#pragma warning disable 0168
using UnityEngine;

namespace SPRPG
{
	public class BootLoader : MonoBehaviour
	{
		private static bool _isLoaded;

		public Assets Assets;

		public CharacterDb CharacterDb;

		private void Start()
		{
			Load();
			Destroy(gameObject);
		}

		public void Load()
		{
			if (_isLoaded)
				return;

			JsonBindings.Setup();
			Assets.Init(Assets);
			DataManager.Load();
			CharacterDb.Load(CharacterDb);

			if (Application.isPlaying)
			{
				App.Init();
				BgmPlayer.Init();
			}

			_isLoaded = true;
		}
	}
}