using Gem;

namespace SPRPG.Battle
{
	public class Boss : Pawn<Boss>
	{
		public BossId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly BossBalanceData Data;
		private readonly BossPassiveManager _passiveManager;

		public Boss(Battle context, BossBalanceData data)
			: base(data.Stats)
		{
			_context = context;
			Data = data;
			_passiveManager = new BossPassiveManager(context, this);
		}

		public void TickPassive()
		{
			_passiveManager.Tick();
		}

		protected override void AfterHpChanged(Hp old)
		{
			base.AfterHpChanged(old);
			Events.OnBossHpChanged.CheckAndCall(this, Hp);
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