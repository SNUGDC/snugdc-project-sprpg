using System.Collections.Generic;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof(CharacterViewDebugger))]
	public class CharacterViewDebuggerEditor : ComponentEditor<CharacterViewDebugger>
	{
		private List<SkillBalanceData> _skillSet;

		protected override void OnEnable()
		{
			base.OnEnable();
			_skillSet = SkillBalance._.SelectCharacterSpecifics(Target.View.Id);
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			foreach (var skill in _skillSet)
				RenderPlayButton(skill);
		}

		private void RenderPlayButton(SkillBalanceData data)
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(data.Key.ToString()))
				Target.View.PlaySkillStart(data, null);
			GUILayout.EndHorizontal();
		}
	}
}