using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;

namespace SPRPG
{
	public class SkillSet : IEnumerable<SkillKey>
	{
		private readonly SkillKey[] _skills = new SkillKey[Const.SkillSlotSize];

		public SkillKey this[SkillSlot slot]
		{
			get { return _skills[slot.ToIndex()]; }
			set { _skills[slot.ToIndex()] = value; }
		}

		public SkillSet(IList<SkillKey> skills)
		{
			for (var i = 0; i < _skills.Count(); ++i)
				_skills[i] = skills[i];
		}

		public SkillSet(JsonData data)
		{
			foreach (var slot in SkillHelper.GetSlotEnumerable())
			{
				var skillStr = (string) data[slot.ToIndex()];
				this[slot] = EnumHelper.ParseOrDefault<SkillKey>(skillStr);
			}
		}

		public SkillSet Clone()
		{
			return new SkillSet(_skills);
		}

		public IEnumerator<SkillKey> GetEnumerator()
		{
			return ((IEnumerable<SkillKey>) _skills).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class CharacterBalanceData
	{
		[JsonIgnore]
		public CharacterId Id;
		public string FlavorText;
		public JsonStats Stats;
		public PassiveKey Passive;
		public SkillSet SkillSetDefault;
		public JsonData Special;
	}

	public class ArcherSpecialBalanceData
	{
		public int ArrowInitial;
		public int ArrowMax;
		public Tick ArrowRechargeCooltime;
	}

	public class CharacterBalance : Balance<Dictionary<string, CharacterBalanceData>>
	{
		public static readonly CharacterBalance _ = new CharacterBalance();
		
		protected CharacterBalance() : base("character")
		{ }
	
		public CharacterBalanceData Find(CharacterId id)
		{
			CharacterBalanceData ret;
			Data.TryGet(id.ToString(), out ret);
			return ret;
		}

		protected override void AfterLoad(bool success)
		{
			if (!success) return;
			foreach (var kv in Data)
				EnumHelper.TryParse(kv.Key, out kv.Value.Id);
		}
	}
}