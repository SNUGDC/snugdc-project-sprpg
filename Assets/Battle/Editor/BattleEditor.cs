using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
	[CustomEditor(typeof(Battle))]
	public class BattleEditor : ComponentEditor<Battle>
	{
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
			var scheduler = Target.Scheduler;

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
				while (scheduler.Relative != default(Tick))
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
				Target.EditInputReceiver().ForceSkill();
			if (GUILayout.Button("shift"))
				Target.EditInputReceiver().ForceShift();
			GUILayout.EndHorizontal();
		}
	}
}