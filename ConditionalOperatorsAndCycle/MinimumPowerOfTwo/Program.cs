using System;

namespace MinimumPowerOfTwo
{
    internal class Program
    {
        private const int Basis = 2;

        private const int LowerBoundOfNumberGeneration = 1;
        private const int UpperBoundOfNumberGeneration = 100;

        static void Main(string[] args)
        {
            Random random = new Random();
            int number = random.Next(LowerBoundOfNumberGeneration, UpperBoundOfNumberGeneration);

            Console.WriteLine($"Сгенирированное число: {number}");

            int degreeCounter = 0;

            while(number > 0)
            {
                number /= Basis;
                degreeCounter++;
            }

            int result = (int)Math.Pow(Basis, degreeCounter);

            Console.WriteLine($"Степень: {degreeCounter} \nЧисло в степени: {result}");
        }
    }
}
