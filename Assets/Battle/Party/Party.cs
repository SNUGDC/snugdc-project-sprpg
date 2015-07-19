using System;
using System.Collections;
using System.Collections.Generic;
using Gem;

namespace SPRPG.Battle
{
	[Serializable]
	public struct PartyDef
	{
		public CharacterId _1;
		public CharacterId _2;
		public CharacterId _3;

		public PartyDef(CharacterId character1, CharacterId character2, CharacterId character3)
		{
			_1 = character1;
			_2 = character2;
			_3 = character3;
		}
	}

	public class Party : IEnumerable<Character>
	{
		public Character Leader { get { return this[ShiftedPartyIdx._1]; } }
		public int ShiftCount { get; private set; }

		private readonly Character[] _characters = new Character[SPRPG.Party.Size];

		public ShiftedPartyIdx OriginalToShiftedIdx(OriginalPartyIdx value)
		{
			return (ShiftedPartyIdx) ((int)(value + ShiftCount) % SPRPG.Party.Size);
		}

		public OriginalPartyIdx ShiftedToOriginalIdx(ShiftedPartyIdx value)
		{
			return (OriginalPartyIdx)CSharpHelper.ModPositive((int)value - ShiftCount, SPRPG.Party.Size);
		}

		public Character this[OriginalPartyIdx idx]
		{
			get { return _characters[idx.ToArrayIndex()]; }
			private set { _characters[idx.ToArrayIndex()] = value; }
		}
		
		public Character this[ShiftedPartyIdx idx] { get { return this[ShiftedToOriginalIdx(idx)]; } }

		public Party(PartyDef def)
		{
			InitCharacter(OriginalPartyIdx._1, def._1);
			InitCharacter(OriginalPartyIdx._2, def._2);
			InitCharacter(OriginalPartyIdx._3, def._3);
		}

		private void InitCharacter(OriginalPartyIdx idx, CharacterId id)
		{
			var member = new Character(CharacterDb._.Find(id));
			this[idx] = member;
		}

		public void Shift() { ++ShiftCount; }

		public bool IsSomeoneAlive()
		{
			foreach (var member in this)
				if (member.IsAlive) return true;
			return false;
		}

		public void BeforeTurn()
		{
			foreach (var member in this)
				member.BeforeTurn();
		}

		public void AfterTurn()
		{
			foreach (var member in this)
				member.AfterTurn();
		}

		public IEnumerator<Character> GetEnumerator()
		{
			yield return this[ShiftedPartyIdx._1];
			yield return this[ShiftedPartyIdx._2];
			yield return this[ShiftedPartyIdx._3];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}