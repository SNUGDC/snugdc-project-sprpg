﻿using UnityEngine;

namespace SPRPG
{
	public enum PartyIdx { _1 = 0, _2 = 1, _3 = 2 }

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

	public static class Party
	{
		private static readonly PartyEntry[] _entries = new PartyEntry[3];

		public static bool IsEmpty
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

		public static bool IsFull
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

		public static PartyEntry Find(CharacterID id)
		{
			foreach (var entry in _entries)
			{
				if (entry == null)
					continue;
				if (entry.Character.ID == id)
					return entry;
			}
			return null;
		}

		public static PartyEntry Get(PartyIdx idx)
		{
			return _entries[ConvertIdxToInt(idx)];
		}

		public static bool TrySet(PartyIdx idx, UserCharacter character)
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

			_entries[ConvertIdxToInt(idx)] = new PartyEntry(idx, character);
			return true;
		}

		public static bool Remove(PartyIdx idx)
		{
			var intIdx = ConvertIdxToInt(idx);
			if (_entries[intIdx] != null)
			{
				Debug.LogWarning("party entry " + idx + " already does not exist.");
				return false;
			}

			_entries[intIdx] = null;
			return true;
		}

		public static PartyIdx? GetEmptyEntry()
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

		public static bool Load(SaveData.Party_ saveData)
		{
			if (!IsEmpty)
			{
				Debug.LogWarning("party is not empty.");
				return false;
			}

			return LoadForced(saveData);
		}

		public static bool LoadForced(SaveData.Party_ saveData)
		{
			for (var i = PartyIdx._1; i <= PartyIdx._3; ++i)
			{
				var idxInt = ConvertIdxToInt(i);
				var entry = saveData[i];
				if (entry != null)
				{
					_entries[idxInt] = new PartyEntry(i, entry);
				}
				else
				{
					_entries[idxInt] = null;
				}
			}

			return true;
		}

		public static SaveData.Party_ Save()
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