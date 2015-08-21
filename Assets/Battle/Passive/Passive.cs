namespace SPRPG.Battle
{
	public class Passive
	{
		public readonly Battle Context;
		public readonly Character Owner;

		public Passive(Battle context, Character owner)
		{
			Context = context;
			Owner = owner;
		}

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