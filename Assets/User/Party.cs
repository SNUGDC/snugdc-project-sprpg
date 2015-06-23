using System;
using Gem;
using UnityEngine;

namespace SPRPG
{
	using OnAddEntry = Action<PartyEntry>;
	using OnRemoveEntry = Action<CharacterID>;

	public enum PartyIdx { _1 = 0, _2 = 1, _3 = 2 }

	public static class PartyHelper
	{
		public static PartyIdx MakeIdxFromArrayIndex(int val)
		{
			return (PartyIdx)val;
		}

		public static int ToArrayIndex(this PartyIdx _this)
		{
			return (int)_this;
		}
	}

	public class PartyEntry
	{
		public readonly PartyIdx Idx;
		public readonly UserCharacter Character;

		public PartyEntry(PartyIdx idx, UserCharacter character)
		{
			Idx = idx;
			Character = character;
		}

		public PartyEntry(PartyIdx idx, SaveData.PartyEntry data)
		{
			Idx = idx;
			Character = UserCharacters.Find((CharacterID)data.Character);
		}

		public SaveData.PartyEntry ToSaveData()
		{
			return new SaveData.PartyEntry(Character.ID);
		}
	}

	public class Party
	{
		public const int Size = 3;

		public static Party _ = new Party();

		private readonly PartyEntry[] _entries = new PartyEntry[Size];

		public Action<PartyEntry> OnAdd;
		public Action<PartyIdx, CharacterID> OnRemove;

		private readonly Box<OnAddEntry>[] _onAdd = { new Box<OnAddEntry>(), new Box<OnAddEntry>(), new Box<OnAddEntry>() };
		private readonly Box<OnRemoveEntry>[] _onRemove = { new Box<OnRemoveEntry>(), new Box<OnRemoveEntry>(), new Box<OnRemoveEntry>() };

		public bool IsEmpty
		{
			get
			{
				foreach (var entry in _entries)
				{
					if (entry != null)
						return false;
				}

				return true;
			}
		}

		public bool IsFull
		{
			get
			{
				foreach (var entry in _entries)
				{
					if (entry == null)
						return false;
				}

				return true;
			}
		}

		private static uint ConvertIdxToInt(PartyIdx idx)
		{
			var idxInt = (uint)idx;
			Debug.Assert(idxInt < 3);
			return idxInt;
		}

		private static PartyIdx ConvertIntToIdx(uint idxInt)
		{
			Debug.Assert(idxInt < 3);
			return (PartyIdx)idxInt;
		}

		public PartyEntry Find(CharacterID id)
		{
			foreach (var entry in _entries)
			{
				if (entry == null)
					continue;
				if (entry.Character == id)
					return entry;
			}
			return null;
		}

		public PartyEntry Get(PartyIdx idx)
		{
			return _entries[ConvertIdxToInt(idx)];
		}

		private void Set(PartyIdx idx, PartyEntry entry)
		{
			_entries[ConvertIdxToInt(idx)] = entry;
		}

		public bool TryAdd(UserCharacter character)
		{
			var emptyEntry = GetFirstEmpty();
			if (!emptyEntry.HasValue)
				return false;

			TrySet(emptyEntry.Value, character);
			return true;
		}

		public bool TrySet(PartyIdx idx, UserCharacter character)
		{
			var id = character.ID;

			if (Find(id) != null)
			{
				Debug.LogError("trying to set character " + id + " into " + idx
					+ ", but " + id);
				return false;
			}

			if (Get(idx) != null)
			{
				Debug.LogError("trying to set character " + id + " into " + idx
					+ ", but already occupied by " + id);
				return false;
			}

			var entry = new PartyEntry(idx, character);
			Set(idx, entry);
			AfterAdd(entry);
			return true;
		}

		private void AfterAdd(PartyEntry entry)
		{
			GetOnAdd(entry.Idx).Value.CheckAndCall(entry);
			OnAdd.CheckAndCall(entry);
		}

		public bool Remove(PartyIdx idx)
		{
			var intIdx = ConvertIdxToInt(idx);
			var entry = _entries[intIdx];
			if (entry == null)
			{
				Debug.LogWarning("party entry " + idx + " already does not exist.");
				return false;
			}

			_entries[intIdx] = null;
			AfterRemove(idx, entry.Character);
			return true;
		}

		public bool Remove(CharacterID id)
		{
			var idxInt = _entries.SetFirstIf(null, entry => (entry != null && entry.Character == id));
			if (idxInt.HasValue) AfterRemove(ConvertIntToIdx((uint)idxInt.Value), id);
			return idxInt.HasValue;
		}

		private void AfterRemove(PartyIdx idx, CharacterID character)
		{
			GetOnRemove(idx).Value.CheckAndCall(character);
			OnRemove.CheckAndCall(idx, character);
		}

		public PartyIdx? GetFirstEmpty()
		{
			var ret = PartyIdx._1;

			foreach (var entry in _entries)
			{
				if (entry == null)
					return ret;
				++ret;
			}

			return null;
		}
		
		public Box<OnAddEntry> GetOnAdd(PartyIdx idx)
		{
			return _onAdd[ConvertIdxToInt(idx)];
		}

		public Box<OnRemoveEntry> GetOnRemove(PartyIdx idx)
		{
			return _onRemove[ConvertIdxToInt(idx)];
		}

		public bool Load(SaveData.Party_ saveData)
		{
			if (!IsEmpty)
			{
				Debug.LogWarning("party is not empty.");
				return false;
			}

			return LoadForced(saveData);
		}

		public bool LoadForced(SaveData.Party_ saveData)
		{
			for (var i = PartyIdx._1; i <= PartyIdx._3; ++i)
			{
				var entry = saveData[i];
				if (entry != null)
					Set(i, new PartyEntry(i, entry));
				else
					Set(i, null);
			}

			return true;
		}

		public SaveData.Party_ Save()
		{
			var ret = new SaveData.Party_();

			for (var i = PartyIdx._1; i <= PartyIdx._3; ++i)
			{
				var idxInt = ConvertIdxToInt(i);
				var entry = _entries[idxInt];
				if (entry != null)
					ret[i] = entry.ToSaveData();
			}

			return ret;
		}
	}
}