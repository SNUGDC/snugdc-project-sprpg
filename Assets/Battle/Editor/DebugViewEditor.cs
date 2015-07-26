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

			RenderParty(Battle.Party);
		}

		private static void RenderParty(Party party)
		{
			if (party == null) return;
			foreach (var character in party)
				RenderCharacter(character);
		}

		private static void RenderCharacter(Character character)
		{
			GUILayout.Label(character.Id.ToString());
			GUILayout.Label(String.Format("hp: {0} ({1})", character.Hp, character.HpMax));

			character.Passive.OnInspectorGUI();

			foreach (var actor in character.SkillManager)
				RenderSkillActor(actor, character.SkillManager.Running == actor);
		}

		private static void RenderSkillActor(SkillActor actor, bool isPerforming)
		{
			GUILayout.BeginHorizontal();

			var color = isPerforming ? Color.green : Color.gray;
			var style = new GUIStyle { normal = { textColor = color } };
			GUILayout.Label("O", style);

			GUILayout.Label(actor.Key.ToString());
			GUILayout.Label(actor.Data.Describe());

			GUILayout.EndHorizontal();
		}
	}
	
	[CustomEditor(typeof (BossDebugView))]
	public class BossDebugViewEditor : ComponentEditor<BossDebugView>
	{
		private Battle Battle { get { return Target.Battle; } }
		private Boss Boss { get { return Battle.Boss; } }
		private BossAi BossAi { get { return Battle.BossAi; } }

		public override void OnInspectorGUI()
		{
			if (Boss == null) return;
			RenderStatus();
			RenderCurrentSkill();
			RenderAllSkills();
			EditorUtility.SetDirty(Target);
		}

		private void RenderStatus()
		{
			GUILayout.Label("boss: " + Boss.Hp);
		}

		private void RenderCurrentSkill()
		{
			if (BossAi.Current == null)
			{
				GUILayout.Label("current: no skill");
				return;
			}

			GUILayout.Label("current: " + BossAi.Current.LocalKey);
		}

		private void RenderAllSkills()
		{
			foreach (var kv in Boss.Data.Skills)
				RenderSkill(kv.Value);
		}

		private void RenderSkill(BossSkillBalanceData data)
		{
			GUILayout.Label("[" + data.Key + "] weight:" + data.Weight);
		}
	}
}
