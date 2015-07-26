using System;
using System.Collections.Generic;

namespace SPRPG.Battle
{
	using OnCharacterHpChanged = Box<Action<OriginalPartyIdx, Character, Hp>>;
	using OnCharacterDead = Box<Action<OriginalPartyIdx, Character>>;
	using OnBossHpChanged = Action<Boss, Hp>;
	using OnBossSkillStart = Action<Boss, BossSkillActor>;

	public static class Events
	{
		public static Action AfterTurn;  

		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged;
		private static readonly List<OnCharacterDead> OnCharacterDead;

		public static OnBossHpChanged OnBossHpChanged;
		public static OnBossSkillStart OnBossSkillStart;

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
