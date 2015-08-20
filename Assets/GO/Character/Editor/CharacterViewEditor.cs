using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof(CharacterViewDebugger))]
	public class CharacterViewDebuggerEditor : ComponentEditor<CharacterViewDebugger>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			GUILayout.BeginHorizontal();
			Target.Skill = (SkillKey)EditorGUILayout.EnumPopup(Target.Skill);
			if (GUILayout.Button("play"))
			{
				var data = SkillBalance._.Find(Target.Skill);
				if (data != null) Target.View.PlaySkillStart(data);
			}
			GUILayout.EndHorizontal();
		}
	}
}