using System.Collections;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PartyView : MonoBehaviour, IEnumerable<CharacterView>
	{
		private readonly CharacterView[] _members = new CharacterView[SPRPG.Party.Size];
		public CharacterView this[OriginalPartyIdx idx] { get { return _members[idx.ToArrayIndex()]; } }

		public CharacterView InstantiateAndAssign(OriginalPartyIdx idx, CharacterData data)
		{
			var characterViewOld = _members[idx.ToArrayIndex()];
			if (characterViewOld != null)
			{
				Debug.LogError("character view already exist.");
				return characterViewOld;
			}

			var characterView = data.CharacterView.Instantiate();
			characterView.transform.SetParent(transform, false);
			_members[idx.ToArrayIndex()] = characterView;
			characterView.transform.SetLScaleX(-1);
			return characterView;
		}

		public void SetAnimatorEnabled(bool enabled)
		{
			foreach (var member in this)
				member.Animator.enabled = enabled;
		}

		public IEnumerator<CharacterView> GetEnumerator()
		{
			return ((IEnumerable<CharacterView>) _members).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}