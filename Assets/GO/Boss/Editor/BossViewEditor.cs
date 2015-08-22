using Gem;
using UnityEngine;
using UnityEditor;

namespace SPRPG.Battle.View
{
	[CustomEditor(typeof(BossView))]
	public class BossViewEditor : ComponentEditor<BossView>
	{
		private BossBalanceData _data;

		protected override void OnEnable()
		{
			base.OnEnable();
			if (!Application.isPlaying) return;
			_data = BossBalance._.Find(Target.Id);
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (!Application.isPlaying) return;
			foreach (var kv in _data.Skills)
				RenderSkillButton(kv.Value, null);
		}

		private void RenderSkillButton(BossSkillBalanceData data, object arguments)
		{
			if (GUILayout.Button(data.Key.ToString()))
				Target.PlaySkillStart(data, null);
		}
	}
}