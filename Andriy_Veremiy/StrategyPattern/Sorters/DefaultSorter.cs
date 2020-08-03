using System;
using StrategyPattern.SortingAlgorithms;
using System.Collections.Generic;
using System.Diagnostics;

namespace StrategyPattern.Sorters
{
    class DefaultSorter<T> : ISortable<T>
        where T : IComparable<T>
    {
        public ISortable<T> Sorter { private get; set; }

        public DefaultSorter(ISortable<T> sortable)
        {
            Sorter = sortable;
        }

        public void Sort(IEnumerable<T> values)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Sorter.Sort(values);
            stopwatch.Stop();

            SortingAlgorithmsPrinter<T>.PrintSortingTime(stopwatch.Elapsed);
        }
    }
}
