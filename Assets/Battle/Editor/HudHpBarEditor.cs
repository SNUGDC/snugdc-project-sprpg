using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG.Battle
{
    [CustomEditor(typeof(HudHpBar))]
    public class HudHpBarEditor : ComponentEditor<HudHpBar>
    {
        private bool _isTestEnabled;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            _isTestEnabled = EditorGUILayout.Toggle("test enabled", _isTestEnabled);
            if (_isTestEnabled)
            {
                Target.SetHp((Hp)(10*Time.time)); //Time = class, time = static property/field
            }

            EditorUtility.SetDirty(target);
        }
    }
}
