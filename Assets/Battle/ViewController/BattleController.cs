using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BattleController : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;
		private Battle Context { get { return _battleWrapper.Battle; } }

		[SerializeField]
		private PartyView _partyView;
		private PartyController _partyController;

		[SerializeField]
		private Transform _bossOrigin;
		private GameObject _bossSkeleton;

		void Start()
		{
			_partyController = PartyController.CreateWithInstantiatingCharacters(_partyView, Context.Party);
			_bossSkeleton = R.Boss.GetSkeleton(Context.Boss.Id).Instantiate();
			_bossSkeleton.transform.SetParent(_bossOrigin, false);
		}

		public void AfterTurn()
		{
			_partyController.AfterTurn();
		}
	}
}
