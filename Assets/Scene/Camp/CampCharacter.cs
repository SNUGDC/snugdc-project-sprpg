using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CampCharacter : MonoBehaviour
	{
		[SerializeField]
		private CharacterData _data;
		public CharacterData Data { get { return _data; } }

		private CharacterSkeleton _skeleton;

		[SerializeField]
		private Animator _animator;

		public static CampCharacter Instantiate(UserCharacter character)
		{
			var ret = Assets._.CampCharacter.Instantiate();
			ret._data = character.Data;
			return ret;
		}

		void Start()
		{
			Debug.Assert(_data != null);

			SetupRenderer();

			_animator.runtimeAnimatorController = _data.CampAnimatorController;
		}

		void SetupRenderer()
		{
			_skeleton = Assets._.CharacterSkeleton.Instantiate();
			_skeleton.name = "Skeleton";
			_skeleton.transform.SetParent(transform, false);
			_skeleton.transform.localPosition = Vector3.zero;
			_skeleton.SetSkin(_data.Skin);
		}
	}
}