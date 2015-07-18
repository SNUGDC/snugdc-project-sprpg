using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Battle
{
	public class SkillManager : IEnumerable<SkillActor>
	{
		public bool IsRunning { get { return Running != null; } }
		public SkillActor Running { get; private set; }

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

		public SkillManager(SkillSetDef def, Character owner)
		{
			this[SkillSlot._1] = SkillFactory.Create(def._1, owner);
			this[SkillSlot._2] = SkillFactory.Create(def._2, owner);
			this[SkillSlot._3] = SkillFactory.Create(def._3, owner);
			this[SkillSlot._4] = SkillFactory.Create(def._4, owner);
		}

		public void Perform(SkillSlot idx)
		{
			if (IsRunning)
			{
				Debug.LogError("run again, performing: " + Running.Key);
				return;
			}

			Running = this[idx];
			Running.OnStop += OnStop;
			Running.Start();
		}

		private void OnStop(SkillActor skillActor)
		{
			Running.OnStop -= OnStop;
			Running = null;
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
