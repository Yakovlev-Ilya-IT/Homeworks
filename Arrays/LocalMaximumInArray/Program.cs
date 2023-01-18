using System;

namespace LocalMaximumInArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 30;
            int[] array = new int[size];

            Random random = new Random();
            int lowerGenerationLimit = 1;
            int upperGenerationLimit = 10;

            Console.WriteLine("Массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(lowerGenerationLimit, upperGenerationLimit);
                Console.Write(array[i] + " ");
            }

            Console.WriteLine("\nЛокальные максимумы: ");

            if (array[1] <= array[0])
                Console.Write(array[0] + " ");

            for (int i = 1; i < array.Length - 1; i++)
                if(array[i - 1] <= array[i] && array[i + 1] <= array[i])
                    Console.Write(array[i] + " ");

            if (array[array.Length - 2] <= array[array.Length - 1])
                Console.Write(array[array.Length - 1] + " ");
        }
    }
}
