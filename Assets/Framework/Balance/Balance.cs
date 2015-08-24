using Gem;
using UnityEngine;

namespace SPRPG
{
	public class Balance<T> where T : class, new()
	{
		public bool IsLoaded { get { return _data != null; } }

		private readonly string _name;

		private T _data;
		public T Data
		{
			get
			{
				Load();
				return _data; 
			}
		}

		protected Balance(string name)
		{
			_name = name;
		}

		public void LoadForced()
		{
			string path = "Data/Balance/" + _name;

			T tmp;
			var success = JsonHelper.LoadFromResources(path, out tmp);

			if (success)
			{
				_data = tmp;
			}
			else if (_data == null)
			{
				Debug.LogError("load " + path + " failed.");
				_data = new T();
			}

			AfterLoad(success);
		}

		public void Load()
		{
			if (!IsLoaded)
				LoadForced();
		}

		protected virtual void AfterLoad(bool success) {}
	}
}