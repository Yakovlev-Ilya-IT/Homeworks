using System;

namespace SequenceOfNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int firstNumber = 5;
            int lastNumber = 96;
            int step = 7;

            for (int currentNumber = firstNumber; currentNumber <= lastNumber; currentNumber += step)
            {
                Console.WriteLine(currentNumber);
            }
        }
    }
}
