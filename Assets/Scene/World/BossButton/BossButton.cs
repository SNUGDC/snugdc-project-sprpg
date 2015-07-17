using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.World
{
	public class BossButton : MonoBehaviour 
	{
		public void SetIcon(BossId id)
		{
			var icon = Resources.Load<Sprite>("World/Boss" + id);
			var image = GetComponent<Image>();
			image.sprite = icon;
			image.SetNativeSize();
		}
	}
}
