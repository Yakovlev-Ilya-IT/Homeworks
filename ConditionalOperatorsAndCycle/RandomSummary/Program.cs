using System;

namespace RandomSummary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lowerBound = 0;
            int upperBound = 101;

            int firstDivisor = 3;
            int secondDivisor = 5;

            Random random = new Random();
            int number = random.Next(lowerBound, upperBound);

            Console.WriteLine($"Полученое число: {number}");

            int sum = 0;
            for (int i = 0; i <= number; i++)
            {
                if (i % firstDivisor == 0 || i % secondDivisor == 0)
                    sum += i;
            }

            Console.WriteLine($"Сумма: {sum}");
        }
    }
}
