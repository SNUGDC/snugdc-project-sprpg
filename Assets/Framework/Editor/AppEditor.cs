using System.IO;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof (App))]
	public class AppEditor : Editor<App>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			RenderConfig();
		}

		private void RenderConfig()
		{
			GUILayout.BeginHorizontal();

			GUILayout.Label("config");

			if (Application.isPlaying)
			{
				if (GUILayout.Button("load"))
					Config.LoadForced();

				if (GUILayout.Button("save"))
					Config.Save();
			}

			var savePath = Config.GetSavePath();
			var isExists = File.Exists(savePath);

			GUI.enabled = isExists;

			if (GUILayout.Button("open"))
				System.Diagnostics.Process.Start(savePath);

			GUI.enabled = true;
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("open default"))
				System.Diagnostics.Process.Start(Config.GetDefaultPath());

			GUI.enabled = isExists;

			if (GUILayout.Button("clear"))
			{
				File.Delete(savePath);
				Config.LoadForced();
			}

			GUI.enabled = true;
			GUILayout.EndHorizontal();
		}
	}
}