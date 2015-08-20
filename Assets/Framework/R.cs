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

		public static class Skill
		{
			public static Sprite GetIcon(SkillKey id) { return Load<Sprite>("SkillIcon/" + id); }
		}

		public static class Character
		{
			private static string PrependRoot(string path) { return "Character/" + path; }
			private static string PrependRoot(CharacterId id, string path) { return PrependRoot(id + "/" + path); }

			public static Sprite LoadIcon(CharacterId id) { return Load<Sprite>(PrependRoot(id, id + "_icon")); }
		}

		public static class Boss
		{
			private static string PrependRoot(string path) { return "Boss/" + path; }
			private static string PrependRoot(BossId id, string path) { return PrependRoot(id + "/" + path); }

			public static Sprite GetIcon(BossId id) { return Load<Sprite>(PrependRoot(id, "boss_" + id + "_icon")); }
			public static GameObject GetSkeleton(BossId id) { return Load<GameObject>(PrependRoot(id, "Boss" + id)); }
		}
	}
}