using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Profile
{
	[CustomEditor(typeof(ProfileController))]
	public class ProfileControllerEditor : ComponentEditor<ProfileController>
	{
		private CharacterId _characterToShow;

		protected override void OnEnable()
		{
			base.OnEnable();
			_characterToShow = Target.Character ?? CharacterId.Warrior;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.BeginHorizontal();
			_characterToShow = (CharacterId) EditorGUILayout.EnumPopup(_characterToShow);
			if (GUILayout.Button("apply"))
				Target.Show(_characterToShow);
			GUILayout.EndHorizontal();
		}
	}
}