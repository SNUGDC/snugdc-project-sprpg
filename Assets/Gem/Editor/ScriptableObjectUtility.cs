using UnityEditor;
using UnityEngine;

namespace Gem
{
	public static class ScriptableObjectUtility
	{
		public static T CreateAsset<T>(string name = null) where T : ScriptableObject
		{
			if (string.IsNullOrEmpty(name))
				name = "New " + typeof (T).Name;
			name += ".asset";

			var asset = ScriptableObject.CreateInstance<T>();
			ProjectWindowUtil.CreateAsset(asset, name);
			return asset;
		}
	}
}