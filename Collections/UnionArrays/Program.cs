using System;
using System.Collections.Generic;

namespace UnionArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] firstList = {"1", "2", "3", "1", "2"};
            string[] secondList = {"1", "6", "4", "5", "2"};

            HashSet<string> result = new HashSet<string>();
            result.UnionWith(firstList);
            result.UnionWith(secondList);

            ShowArrayWithMessage(firstList, "Первый массив:");
            ShowArrayWithMessage(secondList, "Второй массив:");
            ShowArrayWithMessage(result, "Результат:");
        }

        static void ShowArrayWithMessage(IEnumerable<string> array, string message)
        {
            Console.WriteLine(message);

            foreach (string item in array)
                Console.Write(item);

            Console.WriteLine();
        }
    }
}
