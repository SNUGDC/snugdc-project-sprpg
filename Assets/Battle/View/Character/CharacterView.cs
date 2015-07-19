using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class CharacterView : MonoBehaviour
	{
		private CharacterData _data;
		private CharacterSkeleton _skeleton;

		void Start()
		{
			Debug.Assert(_data != null);
			_skeleton = Assets._.CharacterSkeleton.Instantiate();
			_skeleton.transform.SetParent(transform, false);
			SetSkin(_data.Skin);
		}

		public void SetInitData(CharacterData data)
		{
			_data = data;
		}

		private void SetSkin(CharacterSkinData data)
		{
			_skeleton.SetSkin(data);
		}
	}
}