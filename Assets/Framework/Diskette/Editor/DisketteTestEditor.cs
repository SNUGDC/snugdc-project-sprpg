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

			if (GUILayout.Button("open default"))
			{
				System.Diagnostics.Process.Start(Diskette.GetDefaultPath());
			}

			var path = Diskette.GetSavePath(Target.SaveName);
			var exists = File.Exists(path);

			GUI.enabled = exists;
			if (GUILayout.Button("clear"))
				File.Delete(path);
			GUI.enabled = true;
				

			if (!Application.isPlaying)
				return;

			{
				GUILayout.BeginHorizontal();

				if (GUILayout.Button(!Diskette.IsLoaded ? "load" : "load forced"))
					Diskette.LoadForced(Target.SaveName);

				GUI.enabled = Diskette.IsLoaded;
				if (GUILayout.Button("save"))
					Diskette.Save();

				GUI.enabled = exists;
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