using System;
using System.IO;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof (App))]
	public class AppEditor : ComponentEditor<App>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			RenderScene();
			RenderTransition();
			RenderConfig();
			RenderData();
		}

		private void AskSaveAndLoadLevel(Int32 level)
		{
			if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
				Application.LoadLevel(level);
		}

		private void RenderScene()
		{
			GUILayout.Label("scene");

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("reload"))
				AskSaveAndLoadLevel(Application.loadedLevel);

			GUILayout.EndHorizontal();
		}

		private void RenderTransition()
		{
			if (!Application.isPlaying)
				return;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("camp"))
				Transition.TransferToCamp();

			if (GUILayout.Button("stage"))
				Transition.TransferToStage();

			if (GUILayout.Button("world"))
				Transition.TransferToWorld();

			GUILayout.EndHorizontal();
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

		private void RenderData()
		{
			GUILayout.Label("data");
			GUILayout.BeginHorizontal();

			GUI.enabled = Application.isPlaying;
#if BALANCE 
			if (GUILayout.Button("reload"))
				DataManager.Reload();
#endif
			GUI.enabled = true;

			GUILayout.EndHorizontal();
		}
	}
}