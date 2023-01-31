using System;

namespace Shuffle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            Console.WriteLine("Массив до перестановки");

            ShowArray(array);

            Shuffle(array);

            Console.WriteLine("\nМассив после перестановки");

            ShowArray(array);
        }

        static void Shuffle(int[] array)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int randomIndexForTemp = random.Next(array.Length);
                int randomIndexForSource = random.Next(array.Length);

                int temp = array[randomIndexForTemp];
                array[randomIndexForTemp] = array[randomIndexForSource];
                array[randomIndexForSource] = temp;
            }
        }

        static void ShowArray(int[] array)
        {
            foreach (int item in array)
                Console.Write($"{item} ");
        }
    }
}
