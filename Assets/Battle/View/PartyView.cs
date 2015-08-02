using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PartyView : MonoBehaviour
	{
		private readonly CharacterView[] _members = new CharacterView[SPRPG.Party.Size];
		public CharacterView this[OriginalPartyIdx idx] { get { return _members[idx.ToArrayIndex()]; } }

		public CharacterView InstantiateAndAssign(OriginalPartyIdx idx, CharacterData data)
		{
			var characterView = data.CharacterView.Instantiate();
			_members[idx.ToArrayIndex()] = characterView;
			return characterView;
		}
	}
}