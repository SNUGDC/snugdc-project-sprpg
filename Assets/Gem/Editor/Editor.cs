using UnityEngine;

namespace Gem
{
	public class Editor<T> : UnityEditor.Editor 
		where T: Component
	{
		private new T target;

		public T Target {
			get { return target ?? (target = (T) base.target); }
		}

		protected virtual void OnEnable()
		{}
	}

}