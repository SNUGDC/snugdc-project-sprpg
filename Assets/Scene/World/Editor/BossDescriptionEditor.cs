using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.World
{
	[CustomEditor(typeof(BossDescription))]
	public class BossDescriptionEditor : ComponentEditor<BossDescription>
	{
		private StageId _stageId = StageId.Radiation;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			EditorGUILayout.BeginHorizontal();

			_stageId = (StageId)EditorGUILayout.EnumPopup(_stageId);

			if (GUILayout.Button("apply"))
				Target.SetDescription(_stageId);

			EditorGUILayout.EndHorizontal();
		}
	}
}
