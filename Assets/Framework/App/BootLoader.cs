#pragma warning disable 0168
using UnityEngine;

namespace SPRPG
{
	public class BootLoader : MonoBehaviour
	{
		private static bool _isLoaded;

		public Assets Assets;

		public CharacterDB CharacterDB;

		private void Start()
		{
			Load();
			Destroy(gameObject);
		}

		private void Load()
		{
			if (_isLoaded)
				return;

			JsonBindings.Setup();

			Assets.Init(Assets);

			Config.TryLoad();
			CampBalance._.Load();
			SkillBalance._.Load();
			Battle.BossBalance._.Load();

			CharacterDB.Init(CharacterDB);

			App.Init();

			_isLoaded = true;
		}
	}
}