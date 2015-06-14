using System.IO;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof(DisketteTest))]
	public class DisketteTestEditor : ComponentEditor<DisketteTest>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var path = Diskette.GetSavePath(Target.SaveName);
			var exists = File.Exists(path);

			{
				GUILayout.BeginHorizontal();

				GUI.enabled = exists;
				if (GUILayout.Button("open"))
					System.Diagnostics.Process.Start(path);
				GUI.enabled = true;

				if (GUILayout.Button("default"))
					System.Diagnostics.Process.Start(Diskette.GetDefaultPath());

				if (GUILayout.Button("replace"))
				{
					File.Delete(path);
					File.Copy(Diskette.GetDefaultPath(), path);
				}

				GUILayout.EndHorizontal();
			}

			if (!Application.isPlaying)
				return;

			{
				GUILayout.BeginHorizontal();

				if (GUILayout.Button(!Diskette.IsLoaded ? "load" : "load forced"))
					Diskette.LoadForced(Target.SaveName);

				GUI.enabled = Diskette.IsLoaded;
				if (GUILayout.Button("save"))
					Diskette.Save();

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