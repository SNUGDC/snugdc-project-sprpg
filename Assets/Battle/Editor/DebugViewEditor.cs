using System;
using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
	[CustomEditor(typeof(PartyDebugView))]
	public class PartyDebugViewEditor : ComponentEditor<PartyDebugView>
	{
		private Battle Battle { get { return Target.Battle; } }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			RenderParty();
		}

		private void RenderParty()
		{
			if (Battle.Party == null) return;
			foreach (var character in Battle.Party)
				RenderCharacter(character);
		}

		private void RenderCharacter(Character character)
		{
			GUILayout.Label(character.Id.ToString());
			GUILayout.Label(String.Format("hp: {0} ({1})", character.Hp, character.HpMax));
		}
	}
}
