using System;
using Gem;

namespace SPRPG.Battle
{
	public class Boss : Pawn<Boss>
	{
		public BossId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly BossBalanceData Data;
		
		private readonly Clock _clock = new Clock();
		private readonly Schedule _skillSchedule;
		
		private readonly BossPassiveManager _passiveManager;
		private readonly BossAi _ai;
		public BossAi Ai { get { return _ai; } }

		public Boss(Battle context, BossBalanceData data)
			: base(data.Stats)
		{
			_context = context;
			Data = data;
			_skillSchedule = new Schedule(_clock);
			_passiveManager = new BossPassiveManager(context, this);
			_ai = BossFactory.CreateAi(this);
		}

		public void TickBeforeTurn()
		{
			if (IsFreezed) return;
			_clock.Proceed();
		}

		public void TickPassive()
		{
			if (IsDead) return;
			if (IsFreezed) return;
			_passiveManager.Tick();
		}

		public void TickSkill()
		{
			if (IsFreezed) return;
			if (Ai.CanPerform) Ai.TryPerform(_context);
			Ai.Tick();
		}

		public Job ScheduleSkill(Tick delay, Action callback)
		{
			return new Job(_skillSchedule, delay, callback);
		}

		protected override void AfterHpChanged(Hp old)
		{
			base.AfterHpChanged(old);
			Events.Boss.OnHpChanged.CheckAndCall(this, Hp);
		}

		protected override void Stun()
		{
			_passiveManager.ResetByStun();
			_ai.ResetByStun();
		}
	}

	public static class BossHelper
	{
		public static BossSkillBalanceData Find(this Boss thiz, BossSkillLocalKey key)
		{
			BossSkillBalanceData ret;
			thiz.Data.Skills.TryGet(key, out ret);
			return ret;
		}
	}
}