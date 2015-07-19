using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Camp
{
	public class CargoController : MonoBehaviour
	{
		[SerializeField]
		private List<CargoEntry> _entries;

		private Party _party { get { return Party._; } }

		public CargoEntry this[PartyIdx idx]
		{
			get { return _entries[idx.ToArrayIndex()]; }
		}

		void Start()
		{
			var cargoData = CampBalance._.Data.Cargo;
			transform.localPosition = cargoData.Position;

			var entries = cargoData.Entries;
			for (var i = 0; i != Party.Size; ++i)
				_entries[i].transform.localPosition = entries[i].Position;

			_party.OnAdd += OnAdd;
			_party.OnRemove += OnRemove;
			_party.OnReorder += OnReorder;
		}

		void OnDestroy()
		{
			_party.OnAdd -= OnAdd;
			_party.OnRemove -= OnRemove;
			_party.OnReorder -= OnReorder;
		}

		private void OnAdd(PartyMember entry)
		{
			this[entry.Idx].SetCharacter(entry.Character);
		}

		private void OnRemove(PartyIdx idx, CharacterId id)
		{
			this[idx].RemoveCharacter();
		}

		private void OnReorder()
		{
			foreach (var idx in PartyHelper.GetEnumerable())
			{
				var cargoEntry = this[idx];
				var entry = _party.Get(idx);

				if (entry != null)
				{
					cargoEntry.SetCharacter(entry.Character);
				}
				else
				{
					cargoEntry.RemoveCharacter();	
				}
			}
		}
	}
}
