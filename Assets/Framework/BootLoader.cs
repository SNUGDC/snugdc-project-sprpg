#pragma warning disable 0168

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

			Config.TryLoad();

			App._.transform.SetParent(transform.parent, false);

			Destroy(gameObject);

			_isLoaded = true;
		}
	}
}