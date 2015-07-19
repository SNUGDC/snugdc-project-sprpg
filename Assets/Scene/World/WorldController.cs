﻿using Gem;
using UnityEngine;

namespace SPRPG.World
{
	public class WorldController : MonoBehaviour
	{
		public BossButton BossButtonPrefab;

		void Start()
		{
			CreateAllBossButton();
		}

		BossButton CreateBossButton()
		{
			var button = BossButtonPrefab.Instantiate();
			button.transform.SetParent(transform, false);
			return button;
		}

		void CreateAllBossButton()
		{
			foreach (var stageData in WorldDb.GetEnumerable())
			{
				var bossButton = CreateBossButton();
				bossButton.transform.localPosition = stageData.ButtonPosition;
				bossButton.SetIcon(stageData.Boss);
			}
		}
	}
}