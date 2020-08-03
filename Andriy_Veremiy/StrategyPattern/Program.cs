using System;
using System.Collections.Generic;
using StrategyPattern.SortingAlgorithms;
using StrategyPattern.Sorters;

namespace StrategyPattern
{
    class Program
    {
        private static IEnumerable<int> _values;

        static void Main(string[] args)
        {
            ShowMenu();
        }

        static void ShowMenu()
        {
            var sorter = new DefaultSorter<int>(new BubbleSort<int>());
            SetValues();

            bool isExitCommand = false;
            while (!isExitCommand)
            {
                PrintMenuText();
                int command;

                if (Int32.TryParse(Console.ReadLine(), out command))
                {
                    switch (command)
                    {
                        case 0:
                            isExitCommand = true;
                            break;
                        case 1:
                            sorter.Sort(_values);
                            break;
                        case 2:
                            sorter.Sorter = new BubbleSort<int>();
                            break;
                        case 3:
                            sorter.Sorter = new BogoSort<int>();
                            break;
                        case 4:
                            sorter.Sorter = new StoogeSort<int>();
                            break;
                        case 5:
                            sorter.Sorter = new PancakeSort<int>();
                            break;
                        case 6:
                            SortingAlgorithmsPrinter<int>.PrintList(_values);
                            break;
                        case 7:
                            SetValues();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(StringConstants.WrongSymbol);
                }
            }
        }

        // Used to fill _values field with random int values.
        static void SetValues()
        {
            int size;

            Console.WriteLine(StringConstants.EnterCountRequest);
            if (Int32.TryParse(Console.ReadLine(), out size))
            {
                _values = RandomNumbersGenerator.GetRandomIntsList(size);
            }    
        }

        static void PrintMenuText()
        {
            Console.WriteLine();
            Console.WriteLine(StringConstants.HorizontalSeparator);
            Console.WriteLine(StringConstants.MenuText);
            Console.WriteLine(StringConstants.HorizontalSeparator);
            Console.WriteLine();
        }
    }
}
