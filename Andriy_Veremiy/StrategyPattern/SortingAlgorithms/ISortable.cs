using System.Collections.Generic;

namespace StrategyPattern.SortingAlgorithms
{
    interface ISortable<T>
    {
        void Sort(IEnumerable<T> values);
    }
}
