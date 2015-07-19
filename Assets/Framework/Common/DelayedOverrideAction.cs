using System;
using Gem;

namespace SPRPG
{
	public struct DelayedOverrideAction
	{
		public bool IsReserved { get; private set; }
		public Action Action;

		public void Reserve()
		{
			IsReserved = true;
		}

		public void TryInvoke()
		{
			if (!IsReserved) return;
			IsReserved = false;
			Action.CheckAndCall();
		}
	}

	public struct DelayedOverrideAction<T1>
	{
		public bool IsReserved { get; private set; }
		public Action<T1> Action;

		private T1 _arg1;

		public void Reserve(T1 arg1)
		{
			IsReserved = true;
			_arg1 = arg1;
		}

		public void TryInvoke()
		{
			if (!IsReserved) return;
			IsReserved = false;
			Action.CheckAndCall(_arg1);
			_arg1 = default(T1);
		}
	}

	public struct DelayedOverrideAction<T1, T2>
	{
		public bool IsReserved { get; private set; }
		public Action<T1, T2> Action;

		private T1 _arg1;
		private T2 _arg2;

		public void Reserve(T1 arg1, T2 arg2)
		{
			IsReserved = true;
			_arg1 = arg1;
			_arg2 = arg2;
		}

		public void TryInvoke()
		{
			if (!IsReserved) return;
			IsReserved = false;
			Action.CheckAndCall(_arg1, _arg2);
			_arg1 = default(T1);
			_arg2 = default(T2);
		}
	}

	public struct DelayedOverrideAction<T1, T2, T3>
	{
		public bool IsReserved { get; private set; }
		public Action<T1, T2, T3> Action;

		private T1 _arg1;
		private T2 _arg2;
		private T3 _arg3;

		public void Reserve(T1 arg1, T2 arg2, T3 arg3)
		{
			IsReserved = true;
			_arg1 = arg1;
			_arg2 = arg2;
			_arg3 = arg3;
		}

		public void TryInvoke()
		{
			if (!IsReserved) return;
			IsReserved = false;
			Action.CheckAndCall(_arg1, _arg2, _arg3);
			_arg1 = default(T1);
			_arg2 = default(T2);
			_arg3 = default(T3);
		}
	}
}
