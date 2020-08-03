using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern
{
    static class SortingAlgorithmsPrinter<T>
    {
		public static void PrintIteration(IEnumerable<T> values, int iteration)
		{
			Console.Write($"Iteration {iteration}: ");
			PrintList(values);
		}

		public static void PrintList(IEnumerable<T> values)
		{

			foreach (T value in values)
            {
				Console.Write(value);
				Console.Write(" ");
			}

			Console.WriteLine();
		}

		public static void PrintSortComplete(string algorithmName, int iterationsCount)
		{
			Console.WriteLine();
			Console.WriteLine($"{algorithmName} completed after {iterationsCount} iterations.");
		}

		public static void PrintSortingTime(TimeSpan time)
		{
			Console.WriteLine($"Time of sorting - {time}");
		}
	}
}
