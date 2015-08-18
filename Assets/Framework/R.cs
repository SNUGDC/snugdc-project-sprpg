using Gem;
using UnityEngine;

namespace SPRPG
{
	public static class R
	{
		private static T Load<T>(string path) where T : Object
		{
			var ret = Resources.Load<T>(path);
			if (!ret) Debug.LogError(LogMessages.FileNotFound(path));
			return ret;
		}

		public static class Boss
		{
			private static string PrependRoot(string path) { return "Boss/" + path; }
			private static string PrependRoot(BossId id, string path) { return PrependRoot(id + "/" + path); }

			public static Sprite GetIcon(BossId id) { return Load<Sprite>(PrependRoot(id, "boss_" + id + "_icon")); }
		}
	}
}