using System.Collections.Generic;

namespace SPRPG.Battle.View
{
	public class HudCharacterEvents
	{
		public DelayedOverrideAction<OriginalPartyIdx, Character, Hp> OnHpChanged;
		public DelayedOverrideAction<OriginalPartyIdx, Character> OnDead;
	}

	public static class HudEvents
	{
		private static readonly List<HudCharacterEvents> Characters;

		static HudEvents()
		{
			Events.AfterTurn += AfterTurn;

			// initialize Character
			Characters = new List<HudCharacterEvents>
			{
				new HudCharacterEvents(), new HudCharacterEvents(), new HudCharacterEvents(),
			};

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var globalCharacterEvents = Events.GetCharacter(idx);
				var hudCharacterEvents = GetCharacter(idx);
				globalCharacterEvents.OnHpChanged += (idx2, character, hpOld) => hudCharacterEvents.OnHpChanged.Reserve(idx2, character, hpOld);
				globalCharacterEvents.OnDead += (idx2, character) => hudCharacterEvents.OnDead.Reserve(idx2, character);
			}
		}

		private static void AfterTurn()
		{
			foreach (var characterEvents in Characters)
			{
				characterEvents.OnHpChanged.TryInvoke();
				characterEvents.OnDead.TryInvoke();
			}
		}

		public static HudCharacterEvents GetCharacter(OriginalPartyIdx idx)
		{
			return Characters[idx.ToArrayIndex()];
		}
	}
}
