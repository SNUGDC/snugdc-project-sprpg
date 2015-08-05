using System;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle.View
{
	public class HudPauseButton : MonoBehaviour
	{
		[SerializeField]
		private Sprite _pauseSprite;
		[SerializeField]
		private Sprite _resumeSprite;

		private bool _pauseEnabled;
		[SerializeField]
		private Image _image;

		public Action<bool> OnToggle;

		public void Toggle()
		{
			ForceSetToggle(!_pauseEnabled);
			OnToggle.CheckAndCall(_pauseEnabled);
		}

		public void ForceSetToggle(bool pauseEnabled)
		{
			_pauseEnabled = pauseEnabled;
			var _isNextStatePaused = !_pauseEnabled;
			_image.sprite = _isNextStatePaused ? _pauseSprite : _resumeSprite;
		}

		public void OnClick()
		{
			Toggle();
		}
	}
}
