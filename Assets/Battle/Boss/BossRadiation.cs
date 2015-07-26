using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossRadioactiveAreaPassiveArguments
	{
		public readonly Damage Damage;

		public BossRadioactiveAreaPassiveArguments(JsonData data)
		{
			Damage = data["Damage"].ToObject<Damage>();
		}
	}

	public class BossRadioactiveAreaPassive : BossPassive
	{
		public const Tick Cooltime = Const.Term;

		private readonly Cooltimer _cooltimer = new Cooltimer(Cooltime);
		private readonly BossRadioactiveAreaPassiveArguments _arguments;

		public BossRadioactiveAreaPassive(Battle context, Boss boss, BossPassiveBalanceData data) : base(context, boss, data)
		{
			_arguments = new BossRadioactiveAreaPassiveArguments(data.Arguments);
		}

		protected override void DoTick()
		{
			if (!_cooltimer.Tick())
				return;

			foreach (var member in Context.Party)
				member.Hit(_arguments.Damage);
		}
	}

	public class BossRadiationAttackSkillActor : BossSkillActor
	{
		private readonly Damage _damage;

		private Job _hitJob;
		private Job _stopJob;

		public BossRadiationAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = Data.Arguments["Damage"].ToObject<Damage>();
		}

		protected override void DoStart()
		{
			_hitJob = Context.AddBossPerform((Tick)3, () => Context.Party.Leader.Hit(_damage));
			_stopJob = Context.AddAfterTurn((Tick) 5, Stop);
		}

		protected override void DoCancel()
		{
			_hitJob.Cancel();
			_stopJob.Cancel();
		}
	}

	public sealed class BossRadiationPassiveFactory : BossPassiveFactory
	{
		public static readonly BossRadiationPassiveFactory _ = new BossRadiationPassiveFactory();

		public override BossPassive Create(Battle context, Boss boss, BossPassiveBalanceData data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			var key = data.Key;	
			switch (key)
			{
				case BossPassiveLocalKey.RadioactiveArea:
					return new BossRadioactiveAreaPassive(context, boss, data);
				default:
					Debug.LogError("boss " + key + "'s skill " + key + " not handled.");
					return new BossNonePassive(context, boss);
			}
		}
	}
}
