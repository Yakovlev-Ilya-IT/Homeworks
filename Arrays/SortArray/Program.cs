using System;

namespace SortArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[50];

            Random random = new Random();
            int lowerGenerationLimit = 1;
            int upperGenerationLimit = 10;

            Console.WriteLine("Массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(lowerGenerationLimit, upperGenerationLimit);
                Console.Write(array[i] + " ");
            }

            int temp = 0;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i] < array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;    
                    }
                }
            }

            Console.WriteLine("\nОтсортированный массив: ");

            foreach (int item in array)
                Console.Write(item + " ");
        }
    }
}
