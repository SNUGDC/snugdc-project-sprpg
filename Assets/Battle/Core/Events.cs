using System;
using System.Collections.Generic;

namespace SPRPG.Battle
{
	using OnSomeCharacterHpChanged = Action<OriginalPartyIdx, Character, Hp>;
	using OnCharacterHpChanged = Box<Action<Character, Hp>>;
	using OnSomeCharacterDead = Action<OriginalPartyIdx, Character>;
	using OnCharacterDead = Box<Action<Character>>;
	using OnBossHpChanged = Action<Boss, Hp>;

	public static class Events
	{
		public static Action AfterTurn;  

		public static OnSomeCharacterHpChanged OnSomeCharacterHpChanged;
		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged;

		public static OnSomeCharacterDead OnSomeCharacterDead;
		private static readonly List<OnCharacterDead> OnCharacterDead;

		public static OnBossHpChanged OnBossHpChanged;

		public static Action OnWin;
		public static Action OnLose;

		static Events()
		{
			OnCharacterHpChanged = new List<OnCharacterHpChanged>
			{
				new OnCharacterHpChanged(), new OnCharacterHpChanged(), new OnCharacterHpChanged(),
			};

			OnCharacterDead = new List<OnCharacterDead>
			{
				new OnCharacterDead(), new OnCharacterDead(), new OnCharacterDead(),
			};
		}

		public static OnCharacterHpChanged GetOnCharacterHpChanged(OriginalPartyIdx idx)
		{
			return OnCharacterHpChanged[((PartyIdx) idx).ToArrayIndex()];
		}

		public static OnCharacterDead GetOnCharacterDead(OriginalPartyIdx idx)
		{
			return OnCharacterDead[((PartyIdx)idx).ToArrayIndex()];
		}
	}
}
