using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.SortingAlgorithms
{
    class BogoSort <T> : ISortable<T>
        where T: IComparable <T>
    {
		private List<T> _valueList;

		public void Sort(ref IEnumerable<T> values)
		{
			_valueList = values.ToList();

			int iteration = 0;

			while (!IsSorted())
			{
				SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
				iteration++;

				_valueList = Remap();
			}

			SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
			SortingAlgorithmsPrinter<T>.PrintSortComplete(StringConstants.BogoSort, iteration);

			values = _valueList;
		}

		// Used in Sort() method to check if _valueList is sorted.
		private bool IsSorted()
		{
			for (int i = 0; i < _valueList.Count - 1; i++)
			{
				T currentElement = _valueList[i];
				T nextElement = _valueList[i + 1];

				if (currentElement.CompareTo(nextElement) > 0)
				{
					return false;
				}

			}

			return true;
		}

		// Used in Sort() method to randomly remap _valueList.
		private List<T> Remap()
		{
			int randomIndex;
			List<T> listToReturn = new List<T>();
			Random random = new Random();

			while (_valueList.Count > 0)
			{
				randomIndex = random.Next(_valueList.Count);
				listToReturn.Add(_valueList[randomIndex]);
				_valueList.RemoveAt(randomIndex);
			}

			return listToReturn;
		}
	}
}
