using UnityEngine;

namespace SPRPG.World
{
	public class WorldController : MonoBehaviour
	{
		public void Back()
		{
			Transition.TransferToCamp();
		}

		public void TransferToBattle()
		{
			// todo: pass stage.
			Transition.TransferToBattleWithUserBattleDef(StageId.Radiation);
		}
	}
}