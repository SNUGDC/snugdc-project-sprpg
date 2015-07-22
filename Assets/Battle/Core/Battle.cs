using System;
using UnityEngine;
using Random = System.Random;

namespace SPRPG.Battle
{
	public class BattleDef
	{
#if BALANCE
		public bool UseDataInput;
#endif

		public readonly StageId Stage;
		public readonly PartyDef PartyDef;

		public bool RealtimeEnabled = true;

		public BattleDef(StageId stage, PartyDef party)
		{
			Stage = stage;
			PartyDef = party;
		}
	}

	public class Battle 
	{
		public static Battle _ { get; private set; }

		public readonly Random Random = new Random();

		public readonly Clock Clock = new Clock();
		public readonly RelativeClock PlayerClock;
		public readonly BattleFsm Fsm;

		private readonly InputReceiver _inputReceiver;
		private readonly SkillInputProcessor _skillInputProcessor;
		private readonly ShiftInputProcessor _shiftInputProcessor;

		private readonly Party _party;
		public Party Party { get { return _party; } }

		private readonly Boss _boss;
		public Boss Boss { get { return _boss; } }
		private readonly BossAi _bossAi;
		public BossAi BossAi { get { return _bossAi; } }

		public Battle(BattleDef def)
		{
			Debug.Assert(_ == null);
			_ = this;

			PlayerClock = new RelativeClock(Clock);
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

			_skillInputProcessor = new SkillInputProcessor(_inputReceiver, PlayerClock);
			_skillInputProcessor.OnInvoke += PerformSkill;
			_shiftInputProcessor = new ShiftInputProcessor(_inputReceiver, PlayerClock);
			_shiftInputProcessor.OnInvoke += PerformShift;

			_party = new Party(def.PartyDef);
			_boss = BossFactory.Create(def.Stage.ToBossId());
			_bossAi = BossFactory.CreateAi(_boss);

			Clock.OnProceed += OnTick;
		}

		~Battle()
		{
			Debug.Assert(_ == this);
			_ = null;

			Clock.OnProceed -= OnTick;
		}

		public void Update(float dt)
		{
			if (!Fsm.IsResult && _inputReceiver != null)
				_inputReceiver.Update(dt);
		}

		private void OnTick(Clock clock)
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

	public static partial class BattleHelper
	{
		public static Job AddPlayerPerform(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.PlayerPerform.Schedule, delay, callback);
		}

		public static Job AddBossPerform(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.BossPerform.Schedule, delay, callback);
		}

		public static Job AddAfterTurn(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.AfterTurn.Schedule, delay, callback);
		}
	}
}