using UnityEngine;

namespace SPRPG.World
{
	public class WorldController : MonoBehaviour
	{
		[SerializeField]
		private BossButton _bossButton;
		[SerializeField]
		private BossDescription _bossDescription;

		void Start()
		{
			BgmPlayer._.PlayUiBgm();
			_bossButton.OnClickCallback += OnBossSelected;
		}

		public void TransferToBattle()
		{
			Transition.TransferToBattleWithUserBattleDef(StageId.Radiation);
		}

		private void OnBossSelected(BossButton button, bool isSelected)
		{
			if (isSelected)
			{
				var data = Battle.BossBalance._.Find(BossId.Radiation);
				_bossDescription.gameObject.SetActive(true);
				_bossDescription.Show(data);
			}
			else
			{
				_bossDescription.gameObject.SetActive(false);
			}
		}
	}
}