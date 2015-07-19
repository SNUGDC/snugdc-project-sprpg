using System;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class HudController : MonoBehaviour
	{
		public static HudController _ { get; private set; }
		public static Battle Context { get { return _._battleWrapper.Battle; } }
		
		[SerializeField]
		private BattleWrapper _battleWrapper;

		[SerializeField]
		private HudClock _clock;

		[SerializeField]
		private PartyView _party;
		private PartyPlacer _partyPlacer;

		void Start()
		{
			Debug.Assert(_ == null);
			_ = this;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				_party[idx].SetInitData(Context.Party[idx].Data);
			_party.gameObject.SetActive(true);
			
			_partyPlacer = new PartyPlacer(Context.Party, _party);

			Events.AfterTurn += AfterTurn;
		}

		void OnDestroy()
		{
			Events.AfterTurn -= AfterTurn;

			Debug.Assert(_ == this);
			_ = null;
		}

		void Update()
		{
			_clock.RefreshTime(_battleWrapper.Battle.PlayerClock.Relative);
		}

		public void AfterTurn()
		{
			_partyPlacer.AfterTurn();
		}
	}
}