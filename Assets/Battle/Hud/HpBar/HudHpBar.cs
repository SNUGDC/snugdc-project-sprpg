using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
    public class HudHpBar : MonoBehaviour
    {
        [SerializeField]
        private Image _background, _bar, _avatar;

        private Hp _maxHp = (Hp)100;
        public Hp MaxHp 
        {
            get { return _maxHp; }
        }

        public void SetHp(Hp hp)
        {
            if (hp > _maxHp)
            {
                Debug.LogWarning("trying to set hp bigger than max value.");
                hp = _maxHp;
            }

            var length = _background.rectTransform.GetWidth();
            SetBarLength((int)hp / (float)(int)_maxHp * length);
        }

        public void SetMaxHp(Hp hp)
        {
            _maxHp = hp;
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
