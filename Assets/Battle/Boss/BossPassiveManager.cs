using System.Collections.Generic;

namespace SPRPG.Battle
{
	public class BossPassiveManager
	{
		private readonly Dictionary<BossPassiveLocalKey, BossPassive> _passives = new Dictionary<BossPassiveLocalKey, BossPassive>();

		public BossPassiveManager(Battle context, Boss boss)
		{
			var factory = BossWholePassiveFactory.Map(boss.Id);
			foreach (var kv in boss.Data.Passives)
				_passives.Add(kv.Key, factory.Create(context, boss, kv.Value));
		}

		public void Tick()
		{
			foreach (var kv in _passives)
				kv.Value.Tick();
		}
	}
}
