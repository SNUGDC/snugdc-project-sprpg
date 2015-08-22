using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.World
{
	public class BossDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _name;
		[SerializeField]
		private Text _flavorText;

		public void Show(Battle.BossBalanceData data)
		{
			_name.text = data.Name;
			_flavorText.text = data.FlavorText;
		}
	}
}
