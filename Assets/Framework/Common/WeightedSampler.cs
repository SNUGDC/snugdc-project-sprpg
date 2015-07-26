using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SPRPG
{
	public class WeightedSampler<T>
	{
		private struct ProportionValue
		{
			public Proportion Proportion;
			public T Value;
		}

		private readonly List<ProportionValue> _candidates = new List<ProportionValue>();
		private Proportion _totalProportion;

		public void Add(Proportion proportion, T value)
		{
			_candidates.Add(new ProportionValue { Proportion = proportion, Value = value });
			_totalProportion = _totalProportion + (int)proportion;
		}

		public T Sample(Random random)
		{
			if (_totalProportion == 0)
			{
				Debug.LogError("total proportion is zero");
				return default(T);
			}

			var rnd = random.Next();
			rnd = rnd% (int)_totalProportion;

			foreach (var item in _candidates)
			{
				if (rnd < (int)item.Proportion)
					return item.Value;
				rnd -= (int)item.Proportion;
			}

			Debug.LogError("total proportion is not sum of candidates.");
			return default(T);
		}
	}
}