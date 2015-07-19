using System.Collections.Generic;

namespace SPRPG.Battle.View
{
	using OnSomeCharacterHpChanged = DelayedOverrideAction<OriginalPartyIdx, Character, Hp>;
	using OnCharacterHpChanged = Box<DelayedOverrideAction<Character, Hp>>;
	using OnSomeCharacterDead = DelayedOverrideAction<OriginalPartyIdx, Character>;
	using OnCharacterDead = Box<DelayedOverrideAction<Character>>;

	public static class HudEvents
	{
		public static OnSomeCharacterHpChanged OnSomeCharacterHpChanged;
		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged;

		public static OnSomeCharacterDead OnSomeCharacterDead;
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
				Events.GetOnCharacterHpChanged(idx).Value += (character, hpOld) => onCharacterHpChanged.Value.Reserve(character, hpOld);
			}

			Events.OnSomeCharacterHpChanged += (idx, character, hpOld) => OnSomeCharacterHpChanged.Reserve(idx, character, hpOld);

			// initialize CharacterDead
			OnCharacterDead = new List<OnCharacterDead>
			{
				new OnCharacterDead(), new OnCharacterDead(), new OnCharacterDead(),
			};

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var action = GetOnCharacterDead(idx);
				Events.GetOnCharacterDead(idx).Value += (character) => action.Value.Reserve(character);
			}

			Events.OnSomeCharacterDead += (idx, character) => OnSomeCharacterDead.Reserve(idx, character);
		}

		private static void AfterTurn()
		{
			foreach (var action in OnCharacterHpChanged)
				action.Value.TryInvoke();
			OnSomeCharacterHpChanged.TryInvoke();

			foreach (var action in OnCharacterHpChanged)
				action.Value.TryInvoke();
			OnSomeCharacterDead.TryInvoke();
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
