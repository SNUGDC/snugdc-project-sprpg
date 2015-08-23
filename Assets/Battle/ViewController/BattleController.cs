using Gem;
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

		public AudioSource DrumSe;

		void Start()
		{
			_partyController = PartyController.CreateWithInstantiatingCharacters(_partyView, Context.Party);
			_partyController.SetContext(this);

			BossView = R.Boss.GetView(Context.Boss.Id).Instantiate();
			BossView.transform.SetParent(_bossOrigin, false);
			BossView.Context = this;

			Events.OnClockPerfectTerm += OnClockPerfectTerm;
			Events.Boss.OnHit += OnBossHit;
			Events.Boss.OnSkillStart += OnBossSkillStart;
		}

		void OnDestroy()
		{
			Events.OnClockPerfectTerm -= OnClockPerfectTerm;
			Events.Boss.OnHit -= OnBossHit;
			Events.Boss.OnSkillStart -= OnBossSkillStart;
		}

		public void AfterTurn()
		{
			_partyController.AfterTurn();
		}

		public CharacterView FindCharacterView(Character character)
		{
			var idx = Context.Party.FindMemberIdx(character);
			return idx.HasValue ? _partyView[idx.Value] : null;
		}

		private void OnClockPerfectTerm()
		{
			DrumSe.Play();
		}

		private void OnBossHit(Boss boss, Damage damage)
		{
			BossView.PlayHit();
		}

		private void OnBossSkillStart(Boss boss, BossSkillActor skillActor)
		{
			BossView.PlaySkillStart(skillActor.Data, skillActor.MakeViewArgument());
		}
	}
}
