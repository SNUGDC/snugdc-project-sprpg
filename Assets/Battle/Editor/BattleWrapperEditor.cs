using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
	[CustomEditor(typeof(BattleWrapper))]
	public class BattleWrapperEditor : ComponentEditor<BattleWrapper>
	{
		private Battle _battle { get { return Target.Battle; } }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			RenderClock();
			RenderInput();
			RenderBoss();

			if (Target.RealtimeEnabled)
				EditorUtility.SetDirty(Target);
		}

		private void RenderClock()
		{
			var clock = _battle.Clock;
			var playerClock = _battle.PlayerClock;

			GUILayout.BeginHorizontal();
			GUILayout.Label("base: " + playerClock.Base);
			GUILayout.Label("current: " + playerClock.Current);
			GUILayout.Label("relative: " + playerClock.Relative);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("term: " + playerClock.CloseTerm);
			GUILayout.Label("period: " + playerClock.Period);
			GUILayout.Label("distance: " + playerClock.GetCloseTermAndDistance().Distance);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("proceed"))
				clock.Proceed();

			if (GUILayout.Button("term"))
			{
				clock.Proceed();
				while (!playerClock.IsPerfectTerm)
					clock.Proceed();
			}

			if (GUILayout.Button("period"))
			{
				clock.Proceed();
				while (playerClock.RelativePeriodic != default(Tick))
					clock.Proceed();
			}

			if (GUILayout.Button("rebase"))
				playerClock.Rebase();

			GUILayout.EndHorizontal();
		}

		private void RenderInput()
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("skill"))
				_battle.EditInputReceiver().ForceSkill();
			if (GUILayout.Button("shift"))
				_battle.EditInputReceiver().ForceShift();
			GUILayout.EndHorizontal();
		}

		private void RenderBoss()
		{
			if (_battle.Boss == null)
				return;

			GUILayout.Label("boss: " + _battle.Boss.Hp);
		}
	}
}