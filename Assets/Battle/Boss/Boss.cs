using Gem;

namespace SPRPG.Battle
{
	public class Boss : Pawn<Boss>
	{
		public BossId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly BossBalanceData Data;
		private readonly BossPassiveManager _passiveManager;
		private readonly BossAi _ai;
		public BossAi Ai { get { return _ai; } }

		public Boss(Battle context, BossBalanceData data)
			: base(data.Stats)
		{
			_context = context;
			Data = data;
			_passiveManager = new BossPassiveManager(context, this);
			_ai = BossFactory.CreateAi(this);
		}

		public void TickPassive()
		{
			if (HasStatusCondition(StatusConditionType.Freeze)) return;
			if (IsDead) return;
			_passiveManager.Tick();
		}

		public void TickSkill()
		{
			if (HasStatusCondition(StatusConditionType.Freeze)) return;
			if (Ai.CanPerform) Ai.TryPerform(_context);
			Ai.Tick();
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