using System;
using System.Collections.Generic;

namespace StrategyPattern
{
    static class RandomNumbersGenerator
    {
        public static List<int> GetRandomIntsList(int size)
        {
            Random random = new Random();
            var list = new List<int>();

            for (int i = 0; i < size; i++)
            {
                list.Add(random.Next(100));
            }

            return list;
        }
    }
}
