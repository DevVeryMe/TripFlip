using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.SortingAlgorithms
{
    class BubbleSort<T> : ISortable<T>
        where T : IComparable<T>
    {
		private IList<T> _valueList;

		public void Sort(IEnumerable<T> values)
		{
			_valueList = values.ToList();

			int iteration = 0;
			bool isSwapped = true;

			for (int j = 0; (j <= _valueList.Count - 2) && isSwapped; j++)
			{
				isSwapped = false;

				for (int i = 0; i <= _valueList.Count - 2; i++)
				{
					SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
					++iteration;

					var currentElement = _valueList[i];
					var nextElement = _valueList[i + 1];

					if (currentElement.CompareTo(nextElement) > 0)
					{
						Swap(i, i + 1);
						isSwapped = true;
					}

				}

			}

			SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
			SortingAlgorithmsPrinter<T>.PrintSortComplete(StringConstants.BubbleSort, iteration);
		}

		// Used in Sort() method.
		private void Swap(int firstIndex, int secondIndex)
		{
			var swapValue = _valueList[firstIndex];
			_valueList[firstIndex] = _valueList[secondIndex];
			_valueList[secondIndex] = swapValue;
		}
	}
}
