using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.World
{
	public class BossDescription : MonoBehaviour
	{
		public Text Text;

		public void SetDescription(StageId key)
		{
			SetDescription(GetDescription(key));
		}

		private string GetDescription(StageId key)
		{
		   return WorldDb.Find(key).Detail;
		}

		private void SetDescription(string description)
		{
			Text.text = description;
		}
	}
}
