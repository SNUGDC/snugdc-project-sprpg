using System;
using System.Collections.Generic;

namespace SPRPG.Battle
{
	public class CharacterEvents
	{
		public Action<OriginalPartyIdx, Character, Hp> OnHpChanged;
		public Action<OriginalPartyIdx, Character> OnDead;
	}

	public class BossEvents
	{
		public Action<Boss, Hp> OnHpChanged;
		public Action<Boss, BossSkillActor> OnSkillStart;
	}

	public static class Events
	{
		public static Action AfterTurn;

		public static Action<TermAndGrade> OnInputSkill;
		public static Action<TermAndGrade> OnInputShift;

		private static readonly List<CharacterEvents> Characters;
		public static readonly BossEvents Boss = new BossEvents();

		public static Action OnWin;
		public static Action OnLose;

		static Events()
		{
			Characters = new List<CharacterEvents>
			{
				new CharacterEvents(), new CharacterEvents(), new CharacterEvents(),
			};
		}

		public static CharacterEvents GetCharacter(OriginalPartyIdx idx)
		{
			return Characters[((PartyIdx) idx).ToArrayIndex()];
		}
	}
}
