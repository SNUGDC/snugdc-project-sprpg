using System;
using UnityEngine;

namespace SPRPG
{
	public enum StatKey
	{
		Hp,
	}

	public enum StatHp {}

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
	}

	public struct JsonStats
	{
		public int Hp;

		public static implicit operator Stats(JsonStats thiz)
		{
			return new Stats
			{
				Hp = (StatHp)thiz.Hp,
			};
		}
	}
}
