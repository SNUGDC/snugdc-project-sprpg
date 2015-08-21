using UnityEngine;

namespace SPRPG
{
	public class DestroySelf : MonoBehaviour
	{
		public bool ExecuteOnStart;

		void Start()
		{
			if (ExecuteOnStart)
				Execute();
		}

		public void Execute()
		{
			Destroy(gameObject);
		}
	}
}