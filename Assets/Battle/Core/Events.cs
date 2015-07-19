using System;
using System.Collections.Generic;

namespace SPRPG.Battle
{
	using OnSomeCharacterHpChanged = Action<OriginalPartyIdx, Character, Hp>;
	using OnCharacterHpChanged = Box<Action<Character, Hp>>;

	public static class Events
	{
		public static Action AfterTurn;  

		public static OnSomeCharacterHpChanged OnSomeCharacterHpChanged;

		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged = new List<OnCharacterHpChanged>
		{
			new OnCharacterHpChanged(), new OnCharacterHpChanged(), new OnCharacterHpChanged(),
		};

		public static OnCharacterHpChanged GetOnCharacterHpChanged(OriginalPartyIdx idx)
		{
			return OnCharacterHpChanged[((PartyIdx) idx).ToArrayIndex()];
		}
	}
}
