using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
	public class HudHpBar : MonoBehaviour
	{
		[SerializeField]
		private Image _bar, _mark, _avatar;
		[SerializeField]
		private Transform _markAnchor;

		public Hp MaxHp { get; set; }

		void Start()
		{
			RefreshMark();
		}

		public void SetHp(Hp hp)
		{
			if (hp > MaxHp)
			{
				Debug.LogWarning("trying to set hp bigger than max value.");
				hp = MaxHp;
			}

			if (MaxHp == 0)
			{
				Debug.LogError("MaxHp is zero.");
				return;
			}

			SetBarScale((int) hp/(float) (int) MaxHp);
			RefreshMark();
		}

		private void SetBarScale(float scale)
		{
			_bar.transform.SetLScaleX(scale);
		}

		private void RefreshMark()
		{
			_mark.transform.position = _markAnchor.position;
		}

		public void SetIcon(Sprite sprite)
		{
			_avatar.SetSpriteAsNativeAndAssignPivot(sprite);
		}
	}
}
