using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudNumberPoper : MonoBehaviour
	{
		[SerializeField]
		private HudNumberMessage _healPrefab;
		[SerializeField]
		private HudNumberMessage _damagePrefab;

		private static Vector3 RandomPosisition()
		{
			return new Vector3(Random.Range(-30f, 30f), Random.Range(-50f, 50f), 0f);
		}

		private void Pop(HudNumberMessage prefab, int num)
		{
			var message = prefab.Instantiate();
			message.transform.SetParent(transform, false);
			message.GetRectTransform().localPosition = RandomPosisition();
			message.Show(num);
		}

		public void PopHeal(Hp hp)
		{
			Pop(_healPrefab, (int)hp);
		}

		public void PopDamage(Damage damage)
		{
			Pop(_damagePrefab, -((int)damage.Value));
		}
	}
}