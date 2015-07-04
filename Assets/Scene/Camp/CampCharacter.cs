using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
{
	public class CampCharacter : MonoBehaviour
	{
		public static Transform ForegroundRoot;

		[SerializeField]
		private CharacterData _data;
		public CharacterData Data { get { return _data; } }

		private CharacterSkeleton _skeleton;

		[SerializeField]
		private Animator _animator;

		private Transform _foregroundParent;
		[SerializeField]
		private Button _boundingButton;

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

			if (ForegroundRoot)
			{
				_foregroundParent = new GameObject().transform;
				_foregroundParent.name = "Character" + _data.Id;
				_foregroundParent.transform.SetParent(ForegroundRoot, false);
				_boundingButton.transform.SetParent(_foregroundParent, false);
			}

			CampData.Character_ campData;
			if (CampBalance._.Data.CharacterDic.TryGet(_data.Id, out campData))
			{
				transform.localPosition = campData.Position;
				SetFlip(campData.Flip);
				SetBoundingRect(campData.BoundingRect);
			}

			_animator.runtimeAnimatorController = _data.CampAnimatorController;

			if (DebugConfig.Draw)
			{
				_boundingButton.GetComponent<Image>().color = new Color(0, 1, 0, 0.2f);
			}
		}

		void SetFlip(bool val)
		{
			transform.SetLEulerY(val ? 180 : 0);
		}

		void SetBoundingRect(Rect rect)
		{
			var rectTf = (RectTransform) _boundingButton.transform;
			rectTf.offsetMin = rect.min;
			rectTf.offsetMax = rect.max;
		}

		void SetupRenderer()
		{
			_skeleton = Assets._.CharacterSkeleton.Instantiate();
			_skeleton.name = "Skeleton";
			_skeleton.transform.SetParent(transform, false);
			_skeleton.transform.localPosition = Vector3.zero;
			_skeleton.SetSkin(_data.Skin);
		}

		void Update()
		{
			_foregroundParent.transform.position = transform.position;
		}

		public void OnSelect()
		{
			var tool = CharacterTool.Open(_data.Id);
			tool.transform.SetParent(_foregroundParent, false);

			var height = ((RectTransform) _boundingButton.transform).offsetMax.y;
			tool.transform.position = transform.position;
			tool.transform.Translate(new Vector3(0, height/100.0f));
		}
	}
}