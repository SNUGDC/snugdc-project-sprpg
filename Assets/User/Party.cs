using System;
using System.Collections;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	using OnAddMember = Action<PartyMember>;
	using OnRemoveMember = Action<CharacterId>;

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

	public class PartyMember
	{
		public PartyIdx Idx { get; private set; }
		public readonly UserCharacter Character;

		public PartyMember(PartyIdx idx, UserCharacter character)
		{
			Idx = idx;
			Character = character;
		}

		public PartyMember(PartyIdx idx, SaveData.PartyMember data)
		{
			Idx = idx;
			Character = UserCharacters.Find((CharacterId)data.Character);
		}

		public void JustSetIdx(PartyIdx idx)
		{
			Idx = idx;
		}

		public SaveData.PartyMember ToSaveData()
		{
			return new SaveData.PartyMember(Character.Id);
		}
	}

	public class Party : IEnumerable<PartyMember>
	{
		public const int Size = 3;

		public static Party _ = new Party();

		private readonly List<PartyMember> _members = new List<PartyMember>(Size) {null, null, null};

		public Action<PartyMember> OnAdd;
		public Action<PartyIdx, CharacterId> OnRemove;

		private readonly Box<OnAddMember>[] _onAdd = { new Box<OnAddMember>(), new Box<OnAddMember>(), new Box<OnAddMember>() };
		private readonly Box<OnRemoveMember>[] _onRemove = { new Box<OnRemoveMember>(), new Box<OnRemoveMember>(), new Box<OnRemoveMember>() };

		public Action OnReorder;

		public bool IsEmpty
		{
			get
			{
				foreach (var member in _members)
				{
					if (member != null)
						return false;
				}

				return true;
			}
		}

		public bool IsFull
		{
			get
			{
				foreach (var member in _members)
				{
					if (member == null)
						return false;
				}

				return true;
			}
		}

		private Battle.CharacterDef MakeCharacterDefOrDefault(PartyIdx idx)
		{
			var member = Get(idx);
			if (member == null)
			{
				Debug.LogError("member " + idx + " is null.");
				return new Battle.CharacterDef(CharacterBalance._.Find(CharacterId.Warrior), null);
			}
			return new Battle.CharacterDef(member.Character.Data.Balance, member.Character.SkillSet);
		}

		public Battle.PartyDef MakeDef()
		{
			return new Battle.PartyDef(MakeCharacterDefOrDefault(PartyIdx._1), MakeCharacterDefOrDefault(PartyIdx._2), MakeCharacterDefOrDefault(PartyIdx._3));
		}

		public PartyMember Find(CharacterId id)
		{
			foreach (var member in _members)
			{
				if (member == null)
					continue;
				if (member.Character == id)
					return member;
			}
			return null;
		}

		public PartyMember Get(PartyIdx idx)
		{
			return _members[idx.ToArrayIndex()];
		}

		private void Set(PartyIdx idx, PartyMember member)
		{
			_members[idx.ToArrayIndex()] = member;
		}

		public bool TryAdd(UserCharacter character)
		{
			var emptyMember = GetFirstEmpty();
			if (!emptyMember.HasValue)
				return false;

			TrySet(emptyMember.Value, character);
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

			var member = new PartyMember(idx, character);
			Set(idx, member);
			AfterAdd(member);
			return true;
		}

		private void AfterAdd(PartyMember member)
		{
			GetOnAdd(member.Idx).Value.CheckAndCall(member);
			OnAdd.CheckAndCall(member);
		}

		public bool Remove(PartyIdx idx)
		{
			var intIdx = idx.ToArrayIndex();
			var member = _members[intIdx];
			if (member == null)
			{
				Debug.LogWarning("party member " + idx + " already does not exist.");
				return false;
			}

			_members[intIdx] = null;
			AfterRemove(idx, member.Character);
			return true;
		}

		public bool Remove(CharacterId id)
		{
			var idxInt = _members.SetFirstIf(null, member => (member != null && member.Character == id));
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

			foreach (var member in _members)
			{
				if (member == null)
					return ret;
				++ret;
			}

			return null;
		}

		public void Reorder(Func<PartyIdx, PartyIdx> func)
		{
			var old = new List<PartyMember>(_members);
			_members.Clear();
			_members.Resize(Size);

			foreach (var member in old)
			{
				if (member == null) continue;
				var newIdx = func(member.Idx);
				member.JustSetIdx(newIdx);
				_members[newIdx.ToArrayIndex()] = member;
			}

			OnReorder.CheckAndCall();
		}

		public Box<OnAddMember> GetOnAdd(PartyIdx idx)
		{
			return _onAdd[idx.ToArrayIndex()];
		}

		public Box<OnRemoveMember> GetOnRemove(PartyIdx idx)
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
				var member = saveData[i];
				if (member != null)
					Set(i, new PartyMember(i, member));
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
				var member = _members[idxInt];
				if (member != null)
					ret[i] = member.ToSaveData();
			}

			return ret;
		}

		public IEnumerator<PartyMember> GetEnumerator()
		{
			return _members.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}