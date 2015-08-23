﻿using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BattleController : MonoBehaviour
	{
		[SerializeField]
		private BattleWrapper _battleWrapper;
		private Battle Context { get { return _battleWrapper.Battle; } }

		[SerializeField]
		private PartyView _partyView;
		public PartyView PartyView { get { return _partyView; } }
		private PartyController _partyController;

		[SerializeField]
		private Transform _bossOrigin;
		public BossView BossView;

		void Start()
		{
			_partyController = PartyController.CreateWithInstantiatingCharacters(_partyView, Context.Party);
			BossView = R.Boss.GetView(Context.Boss.Id).Instantiate();
			BossView.transform.SetParent(_bossOrigin, false);

			Events.Boss.OnSkillStart += OnBossSkillStart;
		}

		void OnDestroy()
		{
			Events.Boss.OnSkillStart -= OnBossSkillStart;
		}

		public void AfterTurn()
		{
			_partyController.AfterTurn();
		}

		private void OnBossSkillStart(Boss boss, BossSkillActor skillActor)
		{
			BossView.PlaySkillStart(skillActor.Data, skillActor.MakeViewArgument());
		}
	}
}
