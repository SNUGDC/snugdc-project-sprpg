using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossView : MonoBehaviour
	{
		[SerializeField]
		private BossId _id;
		public BossId Id { get { return _id; } }
		[SerializeField]
		private Animator _animator;
		public Animator Animator { get { return _animator; } }

		void Start()
		{
#if UNITY_EDITOR
			gameObject.AddComponent<BossViewDebugger>();
#endif
		}

		public virtual void PlaySkillStart(BossSkillBalanceData data, object arguments) { }
	}
}