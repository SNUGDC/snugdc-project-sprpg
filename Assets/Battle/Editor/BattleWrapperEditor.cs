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

			Target.Battle.SetPlaying(EditorGUILayout.Toggle("pause", Target.Battle.IsPlaying));

			RenderClock();
			RenderInput();
			RenderFsm();
			RenderResult();
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
			GUILayout.Label("distance: " + playerClock.GetCurrentTermAndDistance().Distance);
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
				_battle.InputReceiver.ForceCaptureSkill(_battle.PlayerClock);
			if (GUILayout.Button("shift"))
				_battle.InputReceiver.ForceCaptureShift(_battle.PlayerClock);
			GUILayout.EndHorizontal();
		}

		private void RenderFsm()
		{
			_battle.Fsm.SkipBossPhase = EditorGUILayout.Toggle("skip boss phase", _battle.Fsm.SkipBossPhase);
		}

		private void RenderResult()
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("win"))
				_battle.Fsm.ForceResultInNextTick = Result.Win;
			if (GUILayout.Button("lose"))
				_battle.Fsm.ForceResultInNextTick = Result.Lose;
			GUILayout.EndHorizontal();
		}
	}
}