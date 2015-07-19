using UnityEngine;

namespace SPRPG.Battle.View
{
	public class PartyPlacer
	{
		private struct EntryData
		{
			public Character Character;
			public Transform Transform;
			public bool IsManaged;
		}

		private readonly EntryData[] _party = new EntryData[3];

		public PartyPlacer(Party party, PartyView view)
		{
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
			{
				_party[idx.ToArrayIndex()] = new EntryData
				{
					Character = party[idx],
					Transform = view[idx].transform,
					IsManaged = true,
				};
			}
		}

		public void AfterTurn()
		{
			foreach (var entryData in _party)
			{
				// todo: lerp to right position.
				if (entryData.IsManaged)
					entryData.Transform.localPosition = Vector2.one;
			}
		}
	}
}