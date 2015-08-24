using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class PassiveFactory
	{
		public static Passive Create(PassiveKey key, Battle context, Character owner)
		{
			switch (key)
			{
				case PassiveKey.None:
					return new Passive(context, owner);
				case PassiveKey.Archer:
					return new ArcherPassive(context, owner);
				case PassiveKey.Thief:
					return new ThiefPassive(context, owner);
			}

			Debug.LogError(LogMessages.EnumNotHandled(key));
			return new Passive(context, owner);
		}
	}
}
