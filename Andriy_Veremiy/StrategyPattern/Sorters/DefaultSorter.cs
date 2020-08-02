using System;
using StrategyPattern.SortingAlgorithms;
using System.Collections.Generic;
using System.Diagnostics;

namespace StrategyPattern.Sorters
{
    class DefaultSorter<T> : ISortable<T>
        where T : IComparable<T>
    {
        public ISortable<T> Sortable { private get; set; }

        public DefaultSorter(ISortable<T> sortable)
        {
            Sortable = sortable;
        }

        public void Sort(ref IEnumerable<T> values)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Sortable.Sort(ref values);
            stopwatch.Stop();

            SortingAlgorithmsPrinter<T>.PrintSortingTime(stopwatch.Elapsed);
        }
    }
}
