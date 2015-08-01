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

			Config.TryLoad();
			CampBalance._.Load();
			SkillBalance._.Load();
			Battle.BossBalance._.Load();

			CharacterDb.Init(CharacterDb);

			if (Application.isPlaying)
				App.Init();

			_isLoaded = true;
		}
	}
}