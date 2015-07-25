namespace SPRPG.Battle
{
	public class Passive
	{
		public virtual void Tick()
		{
			// do nothing.
		}

#if UNITY_EDITOR
		public virtual void OnInspectorGUI()
		{
			// do nothing.
		}
#endif
	}
}