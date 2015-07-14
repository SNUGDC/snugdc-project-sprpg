using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Battle
{
	public class SkillManager : IEnumerable<SkillActor>
	{
		private readonly SkillActor[] _actors = new SkillActor[SPRPG.Const.SkillSlotSize];

		public SkillActor this[SkillSlot slot]
		{
			get { return _actors[slot.ToIndex()]; }
			private set
			{
				Debug.Assert(slot.ToTear() == value.Data.Tear, "tear and slot does not match.");
				_actors[slot.ToIndex()] = value;
			}
		}

		public SkillManager(SkillSetDef def)
		{
			this[SkillSlot._1] = SkillFactory.Create(def._1);
			this[SkillSlot._2] = SkillFactory.Create(def._2);
			this[SkillSlot._3] = SkillFactory.Create(def._3);
			this[SkillSlot._4] = SkillFactory.Create(def._4);
		}

		public IEnumerator<SkillActor> GetEnumerator()
		{
			yield return this[SkillSlot._1];
			yield return this[SkillSlot._2];
			yield return this[SkillSlot._3];
			yield return this[SkillSlot._4];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
