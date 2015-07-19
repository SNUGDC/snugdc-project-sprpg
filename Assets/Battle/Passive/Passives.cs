using UnityEngine;

namespace SPRPG.Battle
{
	public class ArcherPassive : Passive
	{
		public int Arrows { get; private set; }
		
		public bool IsArrowLeft { get { return Arrows > 0; } }

		public ArcherPassive()
		{
			Arrows = 3;
		}


		public void DecreaseArrow()
		{
			Debug.Log("decreasing arrows.");
			if (Arrows > 0)
				--Arrows;
			else
			{
				Debug.LogError("No more arrows!");
			}
		}
	}
}
