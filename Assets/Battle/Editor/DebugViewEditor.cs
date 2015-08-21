using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class DebugViewHelper
	{
		private static readonly PawnInvincibleKey _invincibleKey = (PawnInvincibleKey)Battle.KeyGen.Next();

		public static void RenderPawn(Pawn pawn)
		{
			RenderHp(pawn);
			RenderStun(pawn);
			RenderAllStatusCondition(pawn);
			RenderFlags(pawn);
		}

		private static void RenderHp(Pawn pawn)
		{
			GUILayout.BeginHorizontal();
			var hpNew = (Hp)EditorGUILayout.IntField("hp", (int) pawn.Hp);
			if (hpNew != pawn.Hp) pawn.SetHpForced(hpNew);
			EditorGUILayout.LabelField("max: " + pawn.HpMax);
			GUILayout.EndHorizontal();
		}

		private static void RenderAllStatusCondition(Pawn pawn)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("sc: ");
			foreach (var statusCondition in EnumHelper.GetValues<StatusConditionType>())
				RenderStatusCondition(pawn, statusCondition);
			GUILayout.EndHorizontal();
		}

		private static void RenderStatusCondition(Pawn pawn, StatusConditionType type)
		{
			var color = Color.gray;
			switch (type)
			{
				case StatusConditionType.Freeze: color = Color.blue; break;
				case StatusConditionType.Poison: color = Color.magenta; break;
				case StatusConditionType.Blind: color = Color.black; break;
				default: Debug.LogError(LogMessages.EnumNotHandled(type)); break;
			}
			
			var isRunning = pawn.HasStatusCondition(type);
			var buttonLabel = isRunning ? "O" : "X";
			var style = new GUIStyle { normal = { textColor = color } };
			if (GUILayout.Button(buttonLabel, style))
			{
				if (isRunning) pawn.TryCure(type);
				else pawn.TestAndGrant(new StatusConditionTest(type, Percentage._100));
			}
		}

		private static void RenderStun(Pawn pawn)
		{
			if (GUILayout.Button("stun"))
				pawn.TestAndGrant(new StunTest(Percentage._100));
		}

		private static void RenderFlags(Pawn pawn)
		{
			var wasInvincible = pawn.HasInvincible(_invincibleKey);
			if (wasInvincible != EditorGUILayout.Toggle("invincible", wasInvincible))
			{
				if (wasInvincible) pawn.UnsetInvincible(_invincibleKey);
				else pawn.SetInvincible(_invincibleKey);
			}
		}
	}

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
			DebugViewHelper.RenderPawn(character);
			RenderGuard(character);
			character.Passive.OnInspectorGUI();
			foreach (var actor in character.SkillManager)
				RenderSkillActor(actor, character.SkillManager.Running == actor);
		}

		private static void RenderGuard(Character character)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("guard");
			if (GUILayout.Button(character.Guard.IsEnabled ? "disable" : "enable"))
			{
				if (character.Guard.IsEnabled)
					character.Guard.TryRelease();
				else
					character.Guard.Enable();
			}
			GUILayout.EndHorizontal();
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
			GUILayout.Label("boss: " + Boss.Id);
			DebugViewHelper.RenderPawn(Boss);
		}

		private void RenderCurrentSkill()
		{
			if (Boss.Ai.Running == null)
			{
				GUILayout.Label("current: no skill");
				return;
			}

			GUILayout.Label("current: " + Boss.Ai.Running.LocalKey);
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
