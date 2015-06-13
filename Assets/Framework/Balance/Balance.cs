using Gem;

namespace SPRPG
{
	public class Balance<T> where T : class, new()
	{
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
			// todo: load from Assets when BALANCE is undefined.
			T tmp;
			var success = JsonHelper.Load("Raw/Balance/" + _name + ".json", out tmp);

			if (success)
			{
				_data = tmp;
			}
			else if (_data == null)
			{
				_data = new T();
			}

			AfterLoad(success);
		}

		public void Load()
		{
			if (_data == null)
				LoadForced();
		}

		protected virtual void AfterLoad(bool success) {}
	}
}