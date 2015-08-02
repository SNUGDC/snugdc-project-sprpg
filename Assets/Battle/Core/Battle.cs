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
		public static readonly Random KeyGen = new Random();

		public readonly Random Random = new Random();
		public readonly SymbolBinding Binding;

		public bool RealtimeEnabled;
		private BattleRealtime _realtime;
		public readonly Clock Clock = new Clock();
		public readonly RelativeClock PlayerClock;
		public readonly BattleFsm Fsm;

		private readonly InputReceiver _inputReceiver;
		public InputReceiver InputReceiver { get { return _inputReceiver; } }

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

			Binding = BattleSymbolBinding.Create(this); 
			RealtimeEnabled = def.RealtimeEnabled;
			_realtime = new BattleRealtime(Clock);
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

			_party = new Party(def.PartyDef, this);
			_boss = BossFactory.Create(this, def.Stage.ToBossId());
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
			if (RealtimeEnabled)
				_realtime.Update(Time.deltaTime);

			if (!Fsm.IsResult && _inputReceiver != null)
				_inputReceiver.Update(PlayerClock, dt);
		}

		private void OnTick(Clock clock)
		{
			if (Fsm.IsIdle)
				Fsm.Cycle();
		}
	}
}