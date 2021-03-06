﻿using System;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
{
	public class CampCharacter : MonoBehaviour
	{
		private const float OpenToolOffsetY = 0.5f;

		public static Transform ForegroundRoot;

		[SerializeField]
		private CharacterData _data;
		public CharacterData Data { get { return _data; } }

		public CharacterView CharacterView { get; private set; }

		private Transform _foregroundParent;
		[SerializeField]
		private Button _boundingButton;

		public Action<CampCharacter, bool> OnSelectCallback;

		public static CampCharacter Instantiate(UserCharacter character)
		{
			var ret = Assets._.CampCharacter.Instantiate();
			ret._data = character.Data;
			return ret;
		}

		void Start()
		{
			Debug.Assert(_data != null);

			SetupRenderer(_data);

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
				transform.localScale = new Vector3((float)campData.Scale.X, (float)campData.Scale.Y, 1);
				SetFlip(campData.Flip);
				SetBoundingRect(campData.BoundingRect);
			}

			if (DebugConfig.Draw)
			{
				_boundingButton.GetComponent<Image>().color = new Color(0, 1, 0, 0.2f);
			}
		}

		void SetFlip(bool val)
		{
			var scaleX = Mathf.Abs(transform.GetLScaleX());
			transform.SetLScaleX(val ? -scaleX : scaleX);
		}

		void SetBoundingRect(Rect rect)
		{
			var rectTf = (RectTransform) _boundingButton.transform;
			rectTf.offsetMin = rect.min;
			rectTf.offsetMax = rect.max;
		}

		void SetupRenderer(CharacterData data)
		{
			CharacterView = data.CharacterView.Instantiate();
			CharacterView.name = "Skeleton";
			CharacterView.transform.SetParent(transform, false);
			CharacterView.transform.localPosition = Vector3.zero;
		}

		public void SyncPosition()
		{
			_foregroundParent.transform.position = transform.position;
		}

		public void OnSelect()
		{
			var tool = CharacterTool.Open(_data.Id);
			tool.transform.SetParent(_foregroundParent, false);

			var height = ((RectTransform) _boundingButton.transform).offsetMax.y;
			tool.transform.position = transform.position;
			tool.transform.Translate(new Vector3(0, height/100.0f + OpenToolOffsetY));

			tool.OnBeforeClose += characterTool => OnSelectCallback.CheckAndCall(this, false);
			OnSelectCallback.CheckAndCall(this, true);
		}
	}
}