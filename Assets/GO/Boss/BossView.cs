using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossView : MonoBehaviour
	{
		public BattleController Context;

		[SerializeField]
		private BossId _id;
		public BossId Id { get { return _id; } }
		[SerializeField]
		private Animator _animator;
		public Animator Animator { get { return _animator; } }
		[SerializeField]
		private BossViewPoints _points;
		public BossViewPoints Points { get { return _points; } }

		void Start()
		{
#if UNITY_EDITOR
			gameObject.AddComponent<BossViewDebugger>();
#endif
		}

		public virtual void PlaySkillStart(BossSkillBalanceData data, object arguments) { }
	}
}