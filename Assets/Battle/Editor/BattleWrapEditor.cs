using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
	[CustomEditor(typeof(BattleWrapper))]
	public class BattleWrapEditor : ComponentEditor<BattleWrapper>
	{
		private Battle _battle { get { return Target.EditBattle(); } }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			RenderScheduler();
			RenderInput();
		}

		private void RenderScheduler()
		{
			var scheduler = _battle.Scheduler;

			GUILayout.BeginHorizontal();
			GUILayout.Label("base: " + scheduler.Base);
			GUILayout.Label("current: " + scheduler.Current);
			GUILayout.Label("relative: " + scheduler.Relative);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("term: " + scheduler.CloseTerm);
			GUILayout.Label("period: " + scheduler.Period);
			GUILayout.Label("distance: " + scheduler.GetCloseTermAndDistance().Distance);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("proceed"))
				scheduler.Proceed();

			if (GUILayout.Button("term"))
			{
				scheduler.Proceed();
				while (!scheduler.IsPerfectTerm)
					scheduler.Proceed();
			}

			if (GUILayout.Button("period"))
			{
				scheduler.Proceed();
				while (scheduler.RelativePeriodic != default(Tick))
					scheduler.Proceed();
			}

			if (GUILayout.Button("rebase"))
				scheduler.Rebase();

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
	}
}