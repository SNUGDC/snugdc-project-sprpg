using Gem;
using UnityEngine;
using UnityEditor;

namespace SPRPG.Battle.View
{
	[CustomEditor(typeof(HudNumberPoper))]
	public class HudDamagePoperEditor : ComponentEditor<HudNumberPoper> 
	{
		public int Hp = 100;
		public Element Element;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			Hp = EditorGUILayout.IntField("hp", Hp);
			Element = (Element)EditorGUILayout.EnumPopup("element", Element);
			if (GUILayout.Button("pop")) Target.PopDamage(new Damage((Hp)Hp, Element));
		}
	}
}