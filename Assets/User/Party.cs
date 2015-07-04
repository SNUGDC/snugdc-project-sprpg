using System;
using System.Collections;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	using OnAddEntry = Action<PartyEntry>;
	using OnRemoveEntry = Action<CharacterId>;

	public enum PartyIdx { _1 = 0, _2 = 1, _3 = 2 }

	public static class PartyHelper
	{
		public static PartyIdx MakeIdxFromArrayIndex(int val)
		{
			return (PartyIdx)val;
		}

		public static int ToArrayIndex(this PartyIdx thiz)
		{
			return (int)thiz;
		}

		public static IEnumerable<PartyIdx> GetEnumerable()
		{
			yield return PartyIdx._1;
			yield return PartyIdx._2;
			yield return PartyIdx._3;
		}
	}

	public class PartyEntry
	{
		public PartyIdx Idx { get; private set; }
		public readonly UserCharacter Character;

		public PartyEntry(PartyIdx idx, UserCharacter character)
		{
			Idx = idx;
			Character = character;
		}

		public PartyEntry(PartyIdx idx, SaveData.PartyEntry data)
		{
			Idx = idx;
			Character = UserCharacters.Find((CharacterId)data.Character);
		}

		public void JustSetIdx(PartyIdx idx)
		{
			Idx = idx;
		}

		public SaveData.PartyEntry ToSaveData()
		{
			return new SaveData.PartyEntry(Character.Id);
		}
	}

	public class Party : IEnumerable<PartyEntry>
	{
		public const int Size = 3;

		public static Party _ = new Party();

		private readonly List<PartyEntry> _entries = new List<PartyEntry>(Size) {null, null, null};

		public Action<PartyEntry> OnAdd;
		public Action<PartyIdx, CharacterId> OnRemove;

		private readonly Box<OnAddEntry>[] _onAdd = { new Box<OnAddEntry>(), new Box<OnAddEntry>(), new Box<OnAddEntry>() };
		private readonly Box<OnRemoveEntry>[] _onRemove = { new Box<OnRemoveEntry>(), new Box<OnRemoveEntry>(), new Box<OnRemoveEntry>() };

		public Action OnReorder;

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

		public PartyEntry Find(CharacterId id)
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
			return _entries[idx.ToArrayIndex()];
		}

		private void Set(PartyIdx idx, PartyEntry entry)
		{
			_entries[idx.ToArrayIndex()] = entry;
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
			var id = character.Id;

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
			var intIdx = idx.ToArrayIndex();
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

		public bool Remove(CharacterId id)
		{
			var idxInt = _entries.SetFirstIf(null, entry => (entry != null && entry.Character == id));
			if (idxInt.HasValue) AfterRemove(PartyHelper.MakeIdxFromArrayIndex(idxInt.Value), id);
			return idxInt.HasValue;
		}

		private void AfterRemove(PartyIdx idx, CharacterId character)
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

		public void Reorder(Func<PartyIdx, PartyIdx> func)
		{
			var old = new List<PartyEntry>(_entries);
			_entries.Clear();
			_entries.Resize(Size);

			foreach (var entry in old)
			{
				if (entry == null) continue;
				var newIdx = func(entry.Idx);
				entry.JustSetIdx(newIdx);
				_entries[newIdx.ToArrayIndex()] = entry;
			}

			OnReorder.CheckAndCall();
		}

		public Box<OnAddEntry> GetOnAdd(PartyIdx idx)
		{
			return _onAdd[idx.ToArrayIndex()];
		}

		public Box<OnRemoveEntry> GetOnRemove(PartyIdx idx)
		{
			return _onRemove[idx.ToArrayIndex()];
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
				var idxInt = i.ToArrayIndex();
				var entry = _entries[idxInt];
				if (entry != null)
					ret[i] = entry.ToSaveData();
			}

			return ret;
		}

		public IEnumerator<PartyEntry> GetEnumerator()
		{
			return _entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}