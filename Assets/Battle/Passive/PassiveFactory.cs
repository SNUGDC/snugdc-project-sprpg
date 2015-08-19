using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class PassiveFactory
	{
		public static Passive Create(PassiveKey key)
		{
			switch (key)
			{
				case PassiveKey.None:
					return new Passive();
				case PassiveKey.Archer:
					return new ArcherPassive();
			}

			Debug.LogError(LogMessages.EnumNotHandled(key));
			return new Passive();
		}
	}
}
