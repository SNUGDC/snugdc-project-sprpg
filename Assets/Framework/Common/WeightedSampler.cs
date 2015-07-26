using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SPRPG
{
	public class WeightedSampler<T>
	{
		private struct WeightedValue
		{
			public Weight Weight;
			public T Value;
		}

		private readonly List<WeightedValue> _candidates = new List<WeightedValue>();
		private Weight _totalWeight;

		public void Add(Weight weight, T value)
		{
			_candidates.Add(new WeightedValue { Weight = weight, Value = value });
			_totalWeight = _totalWeight + (int)weight;
		}

		public T Sample(Random random)
		{
			if (_totalWeight == 0)
			{
				Debug.LogError("total weight is zero");
				return default(T);
			}

			var rnd = random.Next();
			rnd = rnd% (int)_totalWeight;

			foreach (var item in _candidates)
			{
				if (rnd < (int)item.Weight)
					return item.Value;
				rnd -= (int)item.Weight;
			}

			Debug.LogError("total weight is not sum of candidates.");
			return default(T);
		}
	}
}