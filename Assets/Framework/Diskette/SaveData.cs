using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	public class SaveData
	{
		public struct Character
		{}

		public class PartyEntry
		{
			public CharacterId Character;

			public PartyEntry()
			{}

			public PartyEntry(CharacterId id)
			{
				Character = id;
			}
		}

		public struct Party_
		{
			public PartyEntry _1;
			public PartyEntry _2;
			public PartyEntry _3;

			public PartyEntry this[PartyIdx idx]
			{
				get
				{
					switch (idx)
					{
						case PartyIdx._1:
							return _1;
						case PartyIdx._2:
							return _2;
						case PartyIdx._3:
							return _3;
					}

					Debug.Assert(false, LogMessages.EnumUndefined(idx));
					return null;
				}

				set
				{
					switch (idx)
					{
						case PartyIdx._1:
							_1 = value;
							return;
						case PartyIdx._2:
							_2 = value;
							return;
						case PartyIdx._3:
							_3 = value;
							return;
					}

					Debug.Assert(false, LogMessages.EnumUndefined(idx));
				}
			}
		}

		public Dictionary<string, Character> Characters;
		public Party_ Party;
	}
}
