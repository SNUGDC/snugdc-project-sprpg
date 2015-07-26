using System;
using System.Collections.Generic;
using Gem;
using LitJson;

namespace SPRPG
{
	using BindingInt = Func<int>;

	public class SymbolBinding
	{
		private readonly Dictionary<string, BindingInt> _bindingInt = new Dictionary<string, BindingInt>();

		public void Bind(string symbol, BindingInt binding)
		{
			_bindingInt[symbol] = binding;
		}

		public bool TryEval(string symbol, out int value)
		{
			BindingInt binding;
			if (!_bindingInt.TryGet(symbol, out binding))
			{
				value = default(int);
				return false;
			}
			value = binding();
			return true;
		}

		public int EvalOrDefault(string symbol, int defaultValue = default(int))
		{
			int ret;
			return TryEval(symbol, out ret) ? ret : defaultValue;
		}

		public int EvalOrDefault(JsonData data, int defaultValue = default(int))
		{
			return data.IsNatural ? (int) data : EvalOrDefault((string)data, defaultValue);
		}
	}
}
