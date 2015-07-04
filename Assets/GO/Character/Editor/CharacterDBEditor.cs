using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof (CharacterData))]
	public class CharacterDataEditor : ScriptableObjectEditor<CharacterData>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if ((Target.Skin == null) && GUILayout.Button("create"))
			{
				var skinName = "Character" + Target.Id + "Skin";
				Target.Skin = Gem.ScriptableObjectUtility.CreateAsset<CharacterSkinData>(skinName);
				CharacterSkinDataEditor.AutoApply(Target.Skin, Target.Id);
				EditorUtility.SetDirty(Target);
			}
		}
	}

	[CustomEditor(typeof(CharacterDB))]
	public class CharacterDBEditor : ComponentEditor<CharacterDB>
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (Application.isPlaying)
				return;

			if (GUILayout.Button("add"))
			{
				var datas = Target.Edit();
				var newId = (CharacterId)datas.Count;
				var data = Gem.ScriptableObjectUtility.CreateAsset<CharacterData>("Character" + newId);
				data.Id = newId;
				datas.Add(data);
			}
		}
	}
}