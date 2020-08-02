using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern.SortingAlgorithms
{
    class StoogeSort<T> : ISortable<T>
        where T : IComparable<T>
    {
        private List<T> _valueList;

        public void Sort(ref IEnumerable<T> values)
        {
            _valueList = values.ToList();

            int iteration = 0;
            DoNextIteration(0, _valueList.Count - 1, ref iteration);

            SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
            SortingAlgorithmsPrinter<T>.PrintSortComplete(StringConstants.StoogeSort, iteration);

            values = _valueList;
        }

        // Used in Sort() method.
        private void DoNextIteration(int startIndex, int endIndex, ref int iteration)
        {
            SortingAlgorithmsPrinter<T>.PrintIteration(_valueList, iteration);
            ++iteration;

            if (_valueList[startIndex].CompareTo(_valueList[endIndex]) > 0)
            {
                Swap(startIndex, endIndex);
            }

            if (endIndex - startIndex > 1)
            {
                int length = (endIndex - startIndex + 1) / 3;
                DoNextIteration(startIndex, endIndex - length, ref iteration);
                DoNextIteration(startIndex + length, endIndex, ref iteration);
                DoNextIteration(startIndex, endIndex - length, ref iteration);
            }

        }

        // Used in DoNextIteration method.
        private void Swap(int firstIndex, int secondIndex)
        {
            T swapValue = _valueList[firstIndex];
            _valueList[firstIndex] = _valueList[secondIndex];
            _valueList[secondIndex] = swapValue;
        }
    }
}
