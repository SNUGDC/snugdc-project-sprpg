using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gem;

namespace SPRPG.Battle
{
	[Serializable]
	public struct PartyDef
	{
		public CharacterDef _1;
		public CharacterDef _2;
		public CharacterDef _3;

		public PartyDef(CharacterDef character1, CharacterDef character2, CharacterDef character3)
		{
			_1 = character1;
			_2 = character2;
			_3 = character3;
		}
	}

	public struct CharacterAndIdx
	{
		public OriginalPartyIdx Idx;
		public Character Character;

		public CharacterAndIdx(OriginalPartyIdx idx, Character character)
		{
			Idx = idx;
			Character = character;
		}
	}

	public class Party : IEnumerable<Character>
	{
		public Character Leader { get { return this[ShiftedPartyIdx._1]; } }
		public OriginalPartyIdx LeaderIdx { get { return ShiftedToOriginalIdx(ShiftedPartyIdx._1); } }
		public int ShiftCount { get; private set; }

		private readonly Character[] _members = new Character[SPRPG.Party.Size];

		public Action<OriginalPartyIdx, Character> StunCallback;

		public ShiftedPartyIdx OriginalToShiftedIdx(OriginalPartyIdx value)
		{
			return (ShiftedPartyIdx) CSharpHelper.ModPositive((int)value - ShiftCount, SPRPG.Party.Size);
		}

		public OriginalPartyIdx ShiftedToOriginalIdx(ShiftedPartyIdx value)
		{
			return (OriginalPartyIdx)((int)(value + ShiftCount) % SPRPG.Party.Size);
		}

		public Character this[OriginalPartyIdx idx]
		{
			get { return _members[idx.ToArrayIndex()]; }
			private set { _members[idx.ToArrayIndex()] = value; }
		}
		
		public Character this[ShiftedPartyIdx idx] { get { return this[ShiftedToOriginalIdx(idx)]; } }

		public Party(PartyDef def, Battle context)
		{
			InitCharacter(context, OriginalPartyIdx._1, def._1);
			InitCharacter(context, OriginalPartyIdx._2, def._2);
			InitCharacter(context, OriginalPartyIdx._3, def._3);
		}

		private void InitCharacter(Battle context, OriginalPartyIdx idx, CharacterDef def)
		{
			var member = new Character(def, context);
			this[idx] = member;

			var characterEvents = Events.GetCharacter(idx);
			member.OnHpChanged += (member2, cur, old) => characterEvents.OnHpChanged.CheckAndCall(idx, member2, old);
			member.OnDead += (member2) => characterEvents.OnDead.CheckAndCall(idx, member2);
			member.OnStun += (member2) => OnStun(idx, member2);
		}

		public void Shift() { ++ShiftCount; }

		public bool IsSomeoneAlive()
		{
			foreach (var member in this)
				if (member.IsAlive) return true;
			return false;
		}

		public OriginalPartyIdx? FindMemberIdx(Character character)
		{
			var i = 0;
			foreach (var member in this)
			{
				if (member == character)
					return (OriginalPartyIdx)(++i);
			}
			return null;
		}

		public IEnumerable<CharacterAndIdx> GetAliveMembers()
		{
			var i = 0;
			foreach (var member in _members)
				yield return new CharacterAndIdx(BattleHelper.MakeOriginalPartyIdxFromIndex(i++), member);
		}

		public Character GetAliveLeaderOrMember()
		{
			if (Leader.IsAlive)
				return Leader;

			foreach (var member in this)
			{
				if (member.IsAlive)
					return member;
			}

			return null;
		}

		public CharacterAndIdx? TryGetRandomAliveMember()
		{
			var aliveMembers = GetAliveMembers().ToArray();
			if (aliveMembers.Empty()) return null;
			return aliveMembers.Rand();
		}

		public List<CharacterAndIdx> TryGetRandomAliveMembers(int num)
		{
			var aliveMembers = GetAliveMembers().ToList();
			aliveMembers.Shuffle();
			var numValid = Math.Min(num, aliveMembers.Count);
			return aliveMembers.GetRange(0, numValid).ToList();
		}

		public void BeforeTurn()
		{
			foreach (var member in this)
				member.TickBeforeTurn();
		}

		public void AfterTurn()
		{
			foreach (var member in this)
				member.AfterTurn();
		}

		private void OnStun(OriginalPartyIdx idx, Character character)
		{
			StunCallback.CheckAndCall(idx, character);
		}

		public IEnumerator<Character> GetEnumerator()
		{
			yield return this[ShiftedPartyIdx._1];
			yield return this[ShiftedPartyIdx._2];
			yield return this[ShiftedPartyIdx._3];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}