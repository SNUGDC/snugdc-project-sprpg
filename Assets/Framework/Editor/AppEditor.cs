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

			if (GUILayout.Button("load"))
				Config.LoadForced();

			if (GUILayout.Button("save"))
				Config.Save();

			GUI.enabled = File.Exists(Config.GetSavePath());
			if (GUILayout.Button("open"))
				System.Diagnostics.Process.Start(Config.GetSavePath());

			GUILayout.EndHorizontal();
			GUI.enabled = true;
		}
	}
}