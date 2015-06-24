using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Camp
{
	[CustomEditor(typeof(CampCharacter))]
	public class CampCharacterEditor : ComponentEditor<CampCharacter>
	{
		void OnSceneGUI()
		{
			var data = Target.Data;
			if (data == null)
				return;

			Handles.Label(Target.transform.position + Vector3.down * 0.1f,
				data.ID.ToString(),
				new GUIStyle { fontSize = 16 }); 
		}
	}

}