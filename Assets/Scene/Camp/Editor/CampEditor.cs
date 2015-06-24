﻿using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Camp
{
	[CustomEditor(typeof(CargoEntry))]
	public class CargoEntryEditor : ComponentEditor<CargoEntry>
	{
		private CharacterID _character;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			GUILayout.BeginHorizontal();

			_character = (CharacterID)EditorGUILayout.IntField("character", (int)_character);

			if (GUILayout.Button("apply"))
				Target.SetCharacter(_character);

			GUILayout.EndHorizontal();
		}
	}
}