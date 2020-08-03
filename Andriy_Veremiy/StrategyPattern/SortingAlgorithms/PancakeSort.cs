using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.SortingAlgorithms
{
    class PancakeSort<T> : ISortable<T>
        where T : IComparable<T>
    {
        private IList<T> _valueList;
        
        public void Sort(IEnumerable<T> values)
        {
            _valueList = values.ToList();

            int iteration = 0;

            for (int currentSize = _valueList.Count; currentSize > 1; --currentSize)
            {
                SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
                ++iteration;

                int maxElementIndex = FindMax(currentSize);

                if (maxElementIndex != currentSize - 1)
                {
                    Flip(maxElementIndex);
                    Flip(currentSize - 1);
                }

            }

            SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
            SortingAlgorithmsPrinter<T>.PrintSortComplete(StringConstants.PancakeSort, iteration);
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
        private int FindMax(int size)
        {
            int maxElementIndex = 0;

            for (int i = 0; i < size; ++i)
            {
                var currentElement = _valueList[i];
                var currentMaxElement = _valueList[maxElementIndex];

                if (currentElement.CompareTo(currentMaxElement) > 0)
                {
                    maxElementIndex = i;
                }

            }

            return maxElementIndex;
        }

        // Used in Flip method.
        private void Swap(int firstIndex, int secondIndex)
        {
            var swapValue = _valueList[firstIndex];
            _valueList[firstIndex] = _valueList[secondIndex];
            _valueList[secondIndex] = swapValue;
        }
	}
}
