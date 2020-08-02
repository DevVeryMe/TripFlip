using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.SortingAlgorithms
{
    class PancakeSort<T> : ISortable<T>
        where T : IComparable<T>
    {
		private List<T> _valueList;

		public void Sort(ref IEnumerable<T> values)
        {
            _valueList = values.ToList();

            int iteration = 0;

            for (int currentSize = _valueList.Count; currentSize > 1; --currentSize)
            {
                SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
                ++iteration;

                int mi = FindMax(currentSize);

                if (mi != currentSize - 1)
                {
                    Flip(mi);
                    Flip(currentSize - 1);
                }

            }

            SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
            SortingAlgorithmsPrinter<T>.PrintSortComplete(StringConstants.PancakeSort, iteration);

            values = _valueList;
        }

        // Reverses _valueList[0..i] 
        private void Flip(int i)
        {
            int start = 0;

            while (start < i)
            {
                Swap(start, i);

                start++;
                i--;
            }

        }
 
        // Used in Sort() method.
        private int FindMax(int n)
        {
            int mi, i;

            for (mi = 0, i = 0; i < n; ++i)
            {

                if (_valueList[i].CompareTo(_valueList[mi]) > 0)
                {
                    mi = i;
                }

            }

            return mi;
        }

        // Used in Flip method.
        private void Swap(int firstIndex, int secondIndex)
        {
            T swapValue = _valueList[firstIndex];
            _valueList[firstIndex] = _valueList[secondIndex];
            _valueList[secondIndex] = swapValue;
        }
	}
}
