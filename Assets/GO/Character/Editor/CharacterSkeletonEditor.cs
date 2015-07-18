using Gem;
using UnityEditor;
using UnityEngine;

namespace SPRPG
{
	[CustomEditor(typeof (CharacterSkinData))]
	public class CharacterSkinDataEditor : ScriptableObjectEditor<CharacterSkinData>
	{
		private CharacterId _id;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.BeginHorizontal();
			GUILayout.Label("auto");
			_id = (CharacterId)EditorGUILayout.IntField((int)_id);

			if (GUILayout.Button("apply"))
				AutoApply(Target, _id);

			GUILayout.EndHorizontal();
		}

		private struct Loader
		{
			public Loader(CharacterId id)
			{
				var idStr = ((int)id).ToString();
				_dir = "Assets/R/Character/" + idStr + "/ch_" + idStr + "_";
			}

			public Sprite Load(string name)
			{
				return AssetDatabase.LoadAssetAtPath<Sprite>(_dir + name + ".png");
			}

			public void LoadIfNecessary(string name, ref Sprite sprite)
			{
				if (sprite != null) return;
				sprite = Load(name);
			}

			public void LoadIfNecessary(string first, string second, ref Sprite sprite)
			{
				LoadIfNecessary(first, ref sprite);
				LoadIfNecessary(second, ref sprite);
			}

			private readonly string _dir;
		}

		public static void AutoApply(CharacterSkinData data, CharacterId id)
		{
			var loader = new Loader(id);

			loader.LoadIfNecessary("hip", ref data.Hip);
			loader.LoadIfNecessary("chest", ref data.Chest);
			loader.LoadIfNecessary("head", ref data.Head);
			loader.LoadIfNecessary("eye_l", ref data.EyeL);
			loader.LoadIfNecessary("eye_r", "eye_l", ref data.EyeR);

			loader.LoadIfNecessary("arm_l_u", ref data.ArmLU);
			loader.LoadIfNecessary("arm_l_l", ref data.ArmLL);

			loader.LoadIfNecessary("arm_r_u", "arm_l_u", ref data.ArmRU);
			loader.LoadIfNecessary("arm_r_l", "arm_l_l", ref data.ArmRL);

			loader.LoadIfNecessary("leg_l_u", ref data.LegLU);
			loader.LoadIfNecessary("leg_l_l", ref data.LegLL);
			loader.LoadIfNecessary("leg_r_u", "leg_l_u", ref data.LegRU);
			loader.LoadIfNecessary("leg_r_l", "leg_l_l", ref data.LegRL);

			EditorUtility.SetDirty(data);
		}
	}

	[CustomEditor(typeof(CharacterSkeleton))]
	public class CharacterSkeletonEditor : ComponentEditor<CharacterSkeleton>
	{
		private CharacterId _id;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
				return;

			GUILayout.BeginHorizontal();

			_id = (CharacterId)EditorGUILayout.IntField((int) _id);
			if (GUILayout.Button("apply"))
			{
				var data = CharacterDb._.Find(_id);
				if (data) Target.SetSkin(data.Skin);
			}

			GUILayout.EndHorizontal();
		}
	}
}