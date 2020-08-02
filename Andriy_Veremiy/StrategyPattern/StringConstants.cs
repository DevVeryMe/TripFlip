using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyPattern
{
    static class StringConstants
    {
        public static readonly string EnterCountRequest = "Enter count of values:";
        public static readonly string HorizontalSeparator = "\n--------------------------------------------------------------";
        public static readonly string MenuText = "\nHello, dear user. This app lets you to generate random numbers" +
            "\nand to sort them with chosen sorting algorithm (By default we use BubbleSort)." +
            "\nChoose option:" +
            "\n1 - Start sorting." +
            "\n2 - Change algorithm to BubbleSort." +
            "\n3 - Change algorithm to BogoSort." +
            "\n4 - Change algorithm to StoogeSort." +
            "\n5 - Change algorithm to PancakeSort." +
            "\n6 - Show values." +
            "\n7 - Reset values." +
            "\n0 - Exit.";
        public static readonly string WrongSymbol = "Wrong symbol entered.";
        public static readonly string BogoSort = "BogoSort";
        public static readonly string BubbleSort = "BubbleSort";
        public static readonly string StoogeSort = "StoogeSort";
        public static readonly string PancakeSort = "PancakeSort";
    }
}
