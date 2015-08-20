using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudDamagePoper : MonoBehaviour
	{
		[SerializeField]
		private HudDamageMessage _messagePrefab;

		public void Pop(Damage damage)
		{
			var message = _messagePrefab.Instantiate();
			message.transform.SetParent(transform, false);
			message.transform.localPosition = Vector3.zero;
			message.Show(damage);
		}
	}
}