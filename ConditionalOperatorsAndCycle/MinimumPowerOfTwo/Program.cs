using System;

namespace MinimumPowerOfTwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int basis = 2;
            int minLimit = 1;
            int maxLimit = 100;

            Random random = new Random();
            int number = random.Next(minLimit, maxLimit);

            Console.WriteLine($"Сгенирированное число: {number}");

            int degreeCounter = 0;

            while(number > 0)
            {
                number /= basis;
                degreeCounter++;
            }

            int result = (int)Math.Pow(basis, degreeCounter);

            Console.WriteLine($"Степень: {degreeCounter} \nЧисло в степени: {result}");
        }
    }
}
