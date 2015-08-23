using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudNumberPoper : MonoBehaviour
	{
		[SerializeField]
		private HudNumberMessage _damagePrefab;

		private static Vector3 RandomPosisition()
		{
			return new Vector3(Random.Range(-30f, 30f), Random.Range(-50f, 50f), 0f);
		}

		public void PopDamage(Damage damage)
		{
			var message = _damagePrefab.Instantiate();
			message.transform.SetParent(transform, false);
			message.GetRectTransform().localPosition = RandomPosisition();
			message.Show(damage);
		}
	}
}