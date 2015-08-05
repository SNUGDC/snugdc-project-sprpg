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

		void Start()
		{
			_partyController = PartyController.CreateWithInstantiatingCharacters(_partyView, Context.Party);
		}

		public void AfterTurn()
		{
			_partyController.AfterTurn();
		}
	}
}
