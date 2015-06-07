using System.IO;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof(DisketteTest))]
	public class DisketteTestEditor : Editor<DisketteTest>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			var path = Diskette.GetSavePath(Target.SaveName);

			{
				GUILayout.BeginHorizontal();

				if (GUILayout.Button(!Diskette.IsLoaded ? "load" : "load forced"))
					Diskette.LoadForced(Target.SaveName);

				GUI.enabled = Diskette.IsLoaded;
				if (GUILayout.Button("save"))
					Diskette.Save();

				GUI.enabled = File.Exists(path);
				if (GUILayout.Button("open"))
					System.Diagnostics.Process.Start(path);

				GUILayout.EndHorizontal();
				GUI.enabled = true;
			}

			{
				GUI.enabled = Directory.Exists(Diskette.GetSaveDirectory());
				if (GUILayout.Button("open containing folder"))
				{
					var directory = Path.GetDirectoryName(path);
					if (directory != null)
						System.Diagnostics.Process.Start(directory);
				}

				GUI.enabled = true;
			}

		}
	}
}