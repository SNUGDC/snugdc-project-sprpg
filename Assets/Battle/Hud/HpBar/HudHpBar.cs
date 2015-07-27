using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
	public class HudHpBar : MonoBehaviour
	{
		[SerializeField]
		private Image _background, _bar, _avatar;

		public Hp MaxHp { get; set; }

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

			var length = _background.rectTransform.GetWidth();
			SetBarLength((int)hp / (float)(int)MaxHp * length);
		}

		private void SetBarLength(float length)
		{
			var offsetMax = _bar.rectTransform.offsetMax; //offsetMax = distance in anchor?
			var offsetMin = _bar.rectTransform.offsetMin;
			offsetMax.x = length + offsetMin.x;
			_bar.rectTransform.offsetMax = offsetMax;
		}
	}
}
