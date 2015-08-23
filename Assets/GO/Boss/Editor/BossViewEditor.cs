using Gem;
using UnityEngine;
using UnityEditor;

namespace SPRPG.Battle.View
{
	[CustomEditor(typeof(BossViewDebugger))]
	public class BossViewDebuggerEditor : ComponentEditor<BossViewDebugger>
	{
		private BossBalanceData _data;

		protected override void OnEnable()
		{
			base.OnEnable();
			if (!Application.isPlaying) return;
			_data = BossBalance._.Find(Target.View.Id);
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (!Application.isPlaying) return;
			if (GUILayout.Button("hit"))
				Target.View.PlayHit();
			foreach (var kv in _data.Skills)
				RenderSkillButton(kv.Value, null);
		}

		private void RenderSkillButton(BossSkillBalanceData data, object arguments)
		{
			if (GUILayout.Button(data.Key.ToString()))
				Target.View.PlaySkillStart(data, null);
		}
	}
}