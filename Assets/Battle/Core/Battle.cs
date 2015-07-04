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

	public class Battle : MonoBehaviour
	{
		public static BattleDef DefToInit;

		public readonly Scheduler Scheduler = new Scheduler();

		private bool _isInited;
		private InputReceiver _inputReceiver;
		private SkillInputProcessor _skillInputProcessor;
		private ShiftInputProcessor _shiftInputProcessor;

		private Party _party;
		private Boss _boss;

		void Start()
		{
			if (DefToInit != null)
				Init(DefToInit);
		}

		public void Init(BattleDef def)
		{
			if (_isInited)
			{
				Debug.LogError("already inited.");
				return;
			}

			_isInited = true;

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
		}


#if UNITY_EDITOR
		public InputReceiver EditInputReceiver() { return _inputReceiver; }
#endif
	}
}