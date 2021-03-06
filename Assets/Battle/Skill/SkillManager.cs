﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
				Debug.Assert(slot.ToTier() == value.Data.Tier, "tier and slot does not match.");
				_actors[slot.ToIndex()] = value;
			}
		}

		public SkillManager(SkillSet def, Battle context, Character owner)
		{
			for (var i = 0; i < _actors.Count(); ++i)
			{
				var skill = def[SkillHelper.MakeSlotWithIndex(i)];
				_actors[i] = SkillFactory.Create(skill, context, owner);
			}
		}

		public SkillActor TryPerform(SkillSlot idx)
		{
			if (IsRunning)
			{
				Debug.LogError("run again, performing: " + Running.Key);
				return null;
			}

			Running = this[idx];
			Running.OnStop += OnStop;
			Running.Start();
			return Running;
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
