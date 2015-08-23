using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	[Serializable]
	public class ObjectBattlePoints
	{
		public Transform Center;
		public Transform Bottom;
		public Transform Front;
		public Transform Back;

		public GameObject InstantiateOnCenter(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(Center); }
		public GameObject InstantiateOnBottom(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(Bottom); }
		public GameObject InstantiateOnFront(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(Front); }
		public GameObject InstantiateOnBack(GameObject prefab) { return prefab.InstantiateToParentAndToggleActive(Back); }
	}

	public class BattlePoints : MonoBehaviour
	{
		public static BattlePoints _ { get; private set; }

		public ObjectBattlePoints Leader { get { return Characters[0]; } }
		public ObjectBattlePoints[] Characters;
		public ObjectBattlePoints Boss;

		void Start()
		{
			_ = this;
		}

		void OnDestroy()
		{
			if (_ == this)
				_ = null;
		}
	}
}