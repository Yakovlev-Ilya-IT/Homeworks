using System;

namespace Multiples
{
    internal class Program
    {
        private const int LowerBoundOfNumberGeneration = 1;
        private const int UpperBoundOfNumberGeneration = 28;

        private const int StartOfNumericalInterval = 100;
        private const int EndOfNumericalInterval = 1000;

        static void Main(string[] args)
        {
            Random random = new Random();
            int number = random.Next(LowerBoundOfNumberGeneration, UpperBoundOfNumberGeneration);

            Console.WriteLine($"Сгенирированное число: {number}");

            int result = 0;

            for (int i = 0; i < EndOfNumericalInterval; i += number)
                if(i >= StartOfNumericalInterval)
                    result++;

            Console.WriteLine($"количество трехзначных натуральных чисел, которые кратны {number}: {result}");
        }
    }
}
