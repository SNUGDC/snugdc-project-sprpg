using System.Collections.Generic;

namespace SPRPG.Battle.Hud
{
	using OnSomeCharacterHpChanged = DelayedOverrideAction<OriginalPartyIdx, Character, Hp>;
	using OnCharacterHpChanged = Box<DelayedOverrideAction<Character, Hp>>;

	public static class HudEvents
	{
		public static OnSomeCharacterHpChanged OnSomeCharacterHpChanged;

		private static readonly List<OnCharacterHpChanged> OnCharacterHpChanged = new List<OnCharacterHpChanged>
		{
			new OnCharacterHpChanged(), new OnCharacterHpChanged(), new OnCharacterHpChanged(),
		};

		static HudEvents()
		{
			Events.AfterTurn += AfterTurn;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				var onCharacterHpChanged = GetOnCharacterHpChanged(idx);
				Events.GetOnCharacterHpChanged(idx).Value += (character, hpOld) => onCharacterHpChanged.Value.Reserve(character, hpOld);
			}

			Events.OnSomeCharacterHpChanged += (idx, character, hpOld) => OnSomeCharacterHpChanged.Reserve(idx, character, hpOld);
		}

		private static void AfterTurn()
		{
			foreach (var onCharacterHpChanged in OnCharacterHpChanged)
				onCharacterHpChanged.Value.TryInvoke();
			OnSomeCharacterHpChanged.TryInvoke();
		}

		public static OnCharacterHpChanged GetOnCharacterHpChanged(OriginalPartyIdx idx)
		{
			return OnCharacterHpChanged[idx.ToArrayIndex()];
		}
	}
}
