﻿using System;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleDef
	{
#if BALANCE
		public bool UseDataInput;
#endif

		public readonly StageId Stage;
		public readonly PartyDef PartyDef;

		public BattleDef(StageId stage, PartyDef party)
		{
			Stage = stage;
			PartyDef = party;
		}
	}

	public class Battle 
	{
		public readonly Scheduler Scheduler = new Scheduler();
		public readonly BattleFsm Fsm;

		private readonly InputReceiver _inputReceiver;
		private readonly SkillInputProcessor _skillInputProcessor;
		private readonly ShiftInputProcessor _shiftInputProcessor;

		private readonly Party _party;
		public Party Party { get { return _party; } }
		private readonly Boss _boss;
		public Boss Boss { get { return _boss; } }

		public Battle(BattleDef def)
		{
			Fsm = new BattleFsm(this);

#if BALANCE
			if (def.UseDataInput)
			{
				// todo
				_inputReceiver = null;
			}
			else
#endif
			{
				_inputReceiver = InputReceiverFactory.CreatePlatformPreferred();
			}

			_skillInputProcessor = new SkillInputProcessor(_inputReceiver, Scheduler);
			_skillInputProcessor.OnInvoke += PerformSkill;
			_shiftInputProcessor = new ShiftInputProcessor(_inputReceiver, Scheduler);
			_shiftInputProcessor.OnInvoke += PerformShift;

			_party = new Party(def.PartyDef);
			_boss = BossFactory.Create(def.Stage.ToBossId());

			SkillActor.Reset(this);

			Scheduler.OnProceed += OnTick;
		}

		void OnDestroy()
		{
			Scheduler.OnProceed -= OnTick;
			SkillActor.Reset(null);
		}

		void Update()
		{
			var dt = Time.deltaTime;

			if (_inputReceiver != null)
				_inputReceiver.Update(dt);
		}

		private void OnTick(Scheduler scheduler)
		{
			if (Fsm.IsIdle)
				Fsm.Cycle();
		}

		private void PerformSkill(Term term, InputGrade inputGrade)
		{
			Debug.Log("term: " + term + ", grade: " + inputGrade);
			if (inputGrade == InputGrade.Bad) return;
			_party.Leader.PerformSkill(term.ToSkillSlot());
		}

		private void PerformShift(Term term, InputGrade inputGrade)
		{
			Debug.Log("term: " + term + ", grade: " + inputGrade);
			if (inputGrade == InputGrade.Bad) return;
			_party.Shift();
		}


#if UNITY_EDITOR
		public InputReceiver EditInputReceiver() { return _inputReceiver; }
#endif
	}

	public static class BattleHelper
	{
		public static JobId AddPlayerPerform(this Battle thiz, Tick delay, Action callback)
		{
			return thiz.Fsm.PlayerPerform.JobQueue.AddRelative(delay, callback);
		}
		public static bool RemovePlayerPerform(this Battle thiz, JobId jobId)
		{
			return thiz.Fsm.PlayerPerform.JobQueue.Remove(jobId);
		}
	}
}