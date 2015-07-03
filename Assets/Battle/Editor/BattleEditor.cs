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
		}

		private void RenderScheduler()
		{
			var scheduler = Target.Scheduler;

			GUILayout.Label("base: " + scheduler.Base);
			GUILayout.Label("current: " + scheduler.Current);
			GUILayout.Label("relative: " + scheduler.Relative);
			GUILayout.Label("term: " + scheduler.CloseTerm);
			GUILayout.Label("period: " + scheduler.Period);

			if (GUILayout.Button("proceed"))
				scheduler.Proceed();

			if (GUILayout.Button("proceed term"))
			{
				scheduler.Proceed();
				while (!scheduler.IsPerfectTerm)
					scheduler.Proceed();
			}

			if (GUILayout.Button("proceed period"))
			{
				scheduler.Proceed();
				while (scheduler.Relative != default(Tick))
					scheduler.Proceed();
			}
		}
	}
}