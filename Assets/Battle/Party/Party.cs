﻿namespace SPRPG.Battle
{
	public struct PartyDef
	{
		public CharacterID _1;
		public CharacterID _2;
		public CharacterID _3;
	}

	public class Party
	{
		public Character Leader { get; private set; }

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

			Leader = this[PartyIdx._1];
		}

		private void InitCharacter(PartyIdx idx, CharacterID id)
		{
			this[idx] = new Character(CharacterDB._.Find(id));
		}
	}
}