using System;

namespace LocalMaximumInArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[30];

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

            for (int i = 0; i < array.Length; i++)
            {
                if(i == 0)
                {
                    if (array[i + 1] <= array[i])
                        Console.Write(array[i] + " ");

                    continue;
                }

                if(i == array.Length - 1)
                {
                    if (array[i - 1] <= array[i])
                        Console.Write(array[i] + " ");

                    continue;
                }

                if(array[i - 1] <= array[i] && array[i + 1] <= array[i])
                    Console.Write(array[i] + " ");
            }
        }
    }
}
