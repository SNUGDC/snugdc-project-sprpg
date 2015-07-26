using System;
using UnityEngine;

namespace SPRPG
{
	public enum StatKey
	{
		Hp,
		DamageModifier,
	}

	public enum StatHp {}
	public enum StatDamageModifier { }

	public static class StatConst
	{
		public const StatDamageModifier DefaultDamageModifier = (StatDamageModifier) 1000;
	}

	[Serializable]
	public class Stats
	{
		[SerializeField]
		private int _hp;
		public StatHp Hp
		{
			get { return (StatHp)_hp; }
			set { _hp = (int) value; }
		}

		[SerializeField]
		private int _damageModifier;
		public StatDamageModifier DamageModifier
		{
			get { return (StatDamageModifier)_damageModifier; }
			set { _damageModifier = (int) value; }
		}

		public StatDamageModifier AddDamageModifier(StatDamageModifier val)
		{
			DamageModifier += (int)val;
			return DamageModifier;
		}

		public StatDamageModifier RemoveDamageModifier(StatDamageModifier val)
		{
			var neg = (StatDamageModifier)(-(int) val);
			return AddDamageModifier(neg);
		}
	}

	public struct JsonStats
	{
		public StatHp Hp;
		public StatDamageModifier DamageModifier;

		public static implicit operator Stats(JsonStats thiz)
		{
			return new Stats
			{
				Hp = thiz.Hp,
				DamageModifier = thiz.DamageModifier,
			};
		}
	}

	public static partial class ExtensionMethods
	{
		public static Damage Apply(this StatDamageModifier thiz, Damage value)
		{
			var multiplier = 1 + (int) thiz/1000f;
			var newHp = (Hp)(int)((int) value.Value*multiplier);
			return new Damage(newHp, value.Element);
		}
	}
}
