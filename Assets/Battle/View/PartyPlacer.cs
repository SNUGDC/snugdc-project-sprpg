using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PartyPlacer
	{
		private struct EntryData
		{
			public OriginalPartyIdx Idx;
			public Character Character;
			public Transform Transform;
			public bool IsManaged;
		}

		private readonly Party _party;
		private readonly EntryData[] _entries = new EntryData[3];

		public PartyPlacer(Party party, PartyView view)
		{
			_party = party;

			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				_entries[idx.ToArrayIndex()] = new EntryData
				{
					Idx = idx,
					Character = party[idx],
					Transform = view[idx].transform,
					IsManaged = true,
				};
			}
		}

		public void ResetPosition()
		{
			foreach (var entryData in _entries)
			{
				var balance = BattleBalance._.Data;
				var idx = entryData.Idx.ToArrayIndex();
				var position = balance.PartyPositions[idx];
				entryData.Transform.localPosition = position;
			}
		}

		public void AfterTurn()
		{
			LerpPosition();
		}

		private void LerpPosition()
		{
			foreach (var entryData in _entries)
			{
				if (!entryData.IsManaged) continue;

				var balance = BattleBalance._.Data;
				var shiftedIdx = _party.OriginalToShiftedIdx(entryData.Idx);

				var position = balance.PartyPositions[shiftedIdx.ToArrayIndex()];
				var orgPosition = entryData.Transform.localPosition;
				entryData.Transform.localPosition = Vector2.Lerp(orgPosition, position, balance.PartyPlaceLerp);
			}
		}
	}
}