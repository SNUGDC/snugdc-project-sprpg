using UnityEngine;

namespace SPRPG
{
	public class BootLoader : MonoBehaviour
	{
		private static bool _isLoaded;

		void Start()
		{
			if (_isLoaded)
				return;

			DontDestroyOnLoad(transform.parent.gameObject);

			var loadConfig = Config.Data;

			App._.transform.SetParent(transform.parent, false);

			Destroy(gameObject);

			_isLoaded = true;
		}
	}
}