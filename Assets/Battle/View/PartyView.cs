using System.Linq;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PartyView : MonoBehaviour
	{
		[SerializeField]
		private CharacterView[] _members;

		public CharacterView this[OriginalPartyIdx idx]
		{
			get { return _members[idx.ToArrayIndex()]; }
		}

		void Start()
		{
			Debug.Assert(_members.Count() == SPRPG.Party.Size);
		}
	}
}