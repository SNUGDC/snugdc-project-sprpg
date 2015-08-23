namespace SPRPG.Battle.View
{
	public class PartyController
	{
		private readonly PartyView _partyView;
		private readonly CharacterController[] _characters;
		private readonly PartyPlacer _partyPlacer;

		public static PartyController CreateWithInstantiatingCharacters(PartyView view, Party party)
		{
			return new PartyController(view, party);
		}

		private PartyController(PartyView view, Party party)
		{
			_partyView = view;

			_characters = new CharacterController[SPRPG.Party.Size];
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var member = party[idx];
				var data = CharacterDb._.Find(member.Id);
				var characterView = _partyView.InstantiateAndAssign(idx, data);
				_characters[idx.ToArrayIndex()] = new CharacterController(characterView, member);
			}
			
			_partyPlacer = new PartyPlacer(_partyView, party);
			_partyPlacer.ResetPosition();
		}

		public void SetContext(BattleController context)
		{
			foreach (var character in _characters)
				character.View.Context = context;
		}

		public void AfterTurn()
		{
			_partyPlacer.AfterTurn();
		}
	}
}
