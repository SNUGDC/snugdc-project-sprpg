﻿using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Camp
{
	[CustomEditor(typeof(EntryIcon))]
	public class EntryIconEditor : ComponentEditor<EntryIcon>
	{
		private CharacterId _character;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			GUILayout.BeginHorizontal();
			_character = (CharacterId)EditorGUILayout.IntField("character", (int)_character);
			if (GUILayout.Button("set"))
				Target.SetCharacter(_character);
			if (GUILayout.Button("remove"))
				Target.RemoveCharacter();
			GUILayout.EndHorizontal();
		}
	}
}