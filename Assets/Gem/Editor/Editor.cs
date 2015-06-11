using UnityEngine;

namespace Gem
{
	public class ComponentEditor<T> : UnityEditor.Editor 
		where T: Component
	{
		private new T target;

		public T Target {
			get { return target ?? (target = (T) base.target); }
		}

		protected virtual void OnEnable()
		{}
	}

	public class ScriptableObjectEditor<T> : UnityEditor.Editor
		where T : ScriptableObject
	{
		private new T target;

		public T Target
		{
			get { return target ?? (target = (T)base.target); }
		}

		protected virtual void OnEnable()
		{ }
	}

}