using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof(CharacterDb))]
	public class CharacterDbEditor : ComponentEditor<CharacterDb>
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
				var data = ScriptableObjectUtility.CreateAsset<CharacterData>("Character" + newId);
				data.Id = newId;
				datas.Add(data);
			}
		}
	}
}