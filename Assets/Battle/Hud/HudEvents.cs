using System.Collections.Generic;

namespace SPRPG.Battle.View
{
	using OnCharacterHpChanged = Box<DelayedOverrideAction<OriginalPartyIdx, Character, Hp>>;
	using OnCharacterDead = Box<DelayedOverrideAction<OriginalPartyIdx, Character>>;

	public static class HudEvents
	{
		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged;
		private static readonly List<OnCharacterDead> OnCharacterDead;

		static HudEvents()
		{
			Events.AfterTurn += AfterTurn;

			// initialize CharacterHpChanged
			OnCharacterHpChanged = new List<OnCharacterHpChanged>
			{
				new OnCharacterHpChanged(), new OnCharacterHpChanged(), new OnCharacterHpChanged(),
			};

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var onCharacterHpChanged = GetOnCharacterHpChanged(idx);
				Events.GetOnCharacterHpChanged(idx).Value += (idx2, character, hpOld) => onCharacterHpChanged.Value.Reserve(idx2, character, hpOld);
			}

			// initialize CharacterDead
			OnCharacterDead = new List<OnCharacterDead>
			{
				new OnCharacterDead(), new OnCharacterDead(), new OnCharacterDead(),
			};

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var action = GetOnCharacterDead(idx);
				Events.GetOnCharacterDead(idx).Value += (idx2, character) => action.Value.Reserve(idx2, character);
			}
		}

		private static void AfterTurn()
		{
			foreach (var action in OnCharacterHpChanged)
				action.Value.TryInvoke();

			foreach (var action in OnCharacterHpChanged)
				action.Value.TryInvoke();
		}

		public static OnCharacterHpChanged GetOnCharacterHpChanged(OriginalPartyIdx idx)
		{
			return OnCharacterHpChanged[idx.ToArrayIndex()];
		}

		public static OnCharacterDead GetOnCharacterDead(OriginalPartyIdx idx)
		{
			return OnCharacterDead[idx.ToArrayIndex()];
		}
	}
}
