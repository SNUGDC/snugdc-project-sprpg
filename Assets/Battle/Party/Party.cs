using System;
using System.Collections;
using System.Collections.Generic;

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
		public Character Leader { get { return this[PartyIdx._1]; } }

		private readonly Character[] _characters = new Character[SPRPG.Party.Size];

		public Character this[PartyIdx idx]
		{
			get { return _characters[idx.ToArrayIndex()]; }
			private set { _characters[idx.ToArrayIndex()] = value; }
		}

		public Party(PartyDef def)
		{
			InitCharacter(PartyIdx._1, def._1);
			InitCharacter(PartyIdx._2, def._2);
			InitCharacter(PartyIdx._3, def._3);
		}

		private void InitCharacter(PartyIdx idx, CharacterId id)
		{
			this[idx] = new Character(CharacterDb._.Find(id));
		}

		public void Shift()
		{
			var tmp = this[PartyIdx._3];
			this[PartyIdx._3] = this[PartyIdx._2];
			this[PartyIdx._2] = this[PartyIdx._1];
			this[PartyIdx._1] = tmp;
		}

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
			yield return this[PartyIdx._1];
			yield return this[PartyIdx._2];
			yield return this[PartyIdx._3];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}