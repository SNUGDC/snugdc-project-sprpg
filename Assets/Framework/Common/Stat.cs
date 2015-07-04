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
		public StatHp Hp { get { return (StatHp)_hp; } }
	}
}
