﻿using Gem;
using UnityEngine;
using Random = System.Random;

namespace SPRPG.Battle
{
	public class BattleDef
	{
#if UNITY_EDITOR
		public bool UseDataInput;
#endif

		public readonly StageId Stage;
		public readonly PartyDef PartyDef;

		public bool PlayImmediate = true;

		public BattleDef(StageId stage, PartyDef party)
		{
			Stage = stage;
			PartyDef = party;
		}
	}

	public class Battle 
	{
		public static readonly Random KeyGen = new Random();

		public readonly Random Random = new Random();
		public readonly SymbolBinding Binding;

		public bool IsPlaying { get; private set; }
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

		public Battle(BattleDef def)
		{
			Binding = BattleSymbolBinding.Create(this); 
			IsPlaying = def.PlayImmediate;
			_realtime = new BattleRealtime(Clock);
			PlayerClock = new RelativeClock(Clock);
			Fsm = new BattleFsm(this);

#if UNITY_EDITOR
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

			Clock.OnProceed += OnTick;

			BindCallbacks();
		}

		~Battle()
		{
			Clock.OnProceed -= OnTick;
		}

		public void Update(float dt)
		{
			if (IsPlaying)
				_realtime.Update(Time.deltaTime);

			if (!Fsm.IsResult && _inputReceiver != null)
				_inputReceiver.Update(PlayerClock, dt);
		}

		public void SetPlaying(bool playing)
		{
			IsPlaying = playing;
			Events.OnPlayingChanged.CheckAndCall(playing);
		}

		private void OnTick(Clock clock)
		{
			if (Fsm.IsIdle)
				Fsm.Cycle();
			if (PlayerClock.IsPerfectTerm)
				Events.OnClockPerfectTerm.CheckAndCall();
		}

		private void BindCallbacks()
		{
			_party.StunCallback += (idx, character) => { PlayerClock.Rebase(); };
		}
	}
}