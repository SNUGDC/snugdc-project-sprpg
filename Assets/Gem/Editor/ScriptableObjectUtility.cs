using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gem
{
	public static class ScriptableObjectUtility
	{
		public static T CreateAsset<T>(string name = null) where T : ScriptableObject
		{
			var asset = ScriptableObject.CreateInstance<T>();

			var dir = AssetDatabase.GetAssetPath(Selection.activeObject);
			dir = dir != "" ? Path.GetDirectoryName(dir) : "Assets";

			if (name == null)
				name = "New " + typeof (T).Name;

			var path = AssetDatabase.GenerateUniqueAssetPath(dir + "/" + name + ".asset");

			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();

			return asset;
		}
	}
}