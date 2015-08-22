using Gem;
using System;
using UnityEngine;

namespace SPRPG.World
{
	public class BossButton : MonoBehaviour
	{
		private bool _isSelected;
		[SerializeField]
		private Animator _animator;
		public Action<BossButton, bool> OnClickCallback;

		public void OnClick()
		{
			_isSelected = !_isSelected;
			_animator.SetTrigger(_isSelected ? "Select" : "Deselect");
			OnClickCallback.CheckAndCall(this, _isSelected);
		}
	}
}