using UnityEngine;

namespace SPRPG
{
	public class CharacterView : MonoBehaviour
	{
		private class ActContext : Battle.ActContext
		{
			private readonly CharacterView _owner;
			public override Animator Animator { get { return _owner._animator; } }
			public ActContext(CharacterView owner) { _owner = owner; }
		}

		[SerializeField]
		private CharacterSkeleton _skeleton;
		[SerializeField]
		private Animator _animator;
		private ActContext _actContext;

		void Awake()
		{
			_actContext = new ActContext(this);
		}

		public void Attack()
		{
			_animator.SetTrigger("Attack");
		}

		public void PlaySkillStart(SkillBalanceData data)
		{
			var viewActs = data.ViewActs;
			if (viewActs == null) return;
			var onStart = viewActs.OnStart;
			if (onStart == null) return;
			onStart.Do(_actContext);
		}
	}
}