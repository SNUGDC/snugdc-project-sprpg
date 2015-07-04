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

		private readonly InputReceiver _inputReceiver;
		private readonly SkillInputProcessor _skillInputProcessor;
		private readonly ShiftInputProcessor _shiftInputProcessor;

		private readonly Party _party;
		public Party Party { get { return _party; } }
		private readonly Boss _boss;
		public Boss Boss { get { return _boss; } }

		public Battle(BattleDef def)
		{
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
		}

		void OnDestroy()
		{
			SkillActor.Reset(null);
		}

		void Update()
		{
			var dt = Time.deltaTime;

			if (_inputReceiver != null)
				_inputReceiver.Update(dt);
		}

		private void PerformSkill(Term term, InputGrade inputGrade)
		{
			Debug.Log("term: " + term + ", grade: " + inputGrade);
			_party.Leader.PerformSkill(term.ToSkillSlot());
		}

		private void PerformShift(Term term, InputGrade inputGrade)
		{
			Debug.Log("term: " + term + ", grade: " + inputGrade);
			_party.Shift();
		}


#if UNITY_EDITOR
		public InputReceiver EditInputReceiver() { return _inputReceiver; }
#endif
	}
}