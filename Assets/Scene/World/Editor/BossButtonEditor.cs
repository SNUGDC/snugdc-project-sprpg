using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.World
{
	[CustomEditor(typeof(BossButton))]
	public class BossButtonEditor : ComponentEditor<BossButton>
	{
		private BossId _bossId = BossId.Radiation;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			EditorGUILayout.BeginHorizontal();

			_bossId = (BossId) EditorGUILayout.EnumPopup(_bossId);

			if (GUILayout.Button("apply"))
				Target.SetIcon(_bossId);

			EditorGUILayout.EndHorizontal();
		}
	}
}
