using System;

namespace ArrayCarousel
{
    internal class Program
    {
        private const string ExitCommand = "exit";

        static void Main(string[] args)
        {
            int[] array = new int[10];

            Random random = new Random();
            int lowerGenerationLimit = 1;
            int upperGenerationLimit = 10;

            Console.WriteLine("Массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(lowerGenerationLimit, upperGenerationLimit);
                Console.Write(array[i] + " ");
            }

            bool programmIsOpen = true;

            while (programmIsOpen)
            {
                Console.Write($"\nВведите на сколько позиций сдвинуть массив влево или {ExitCommand} для выхода: ");

                string input = Console.ReadLine();

                if(input == ExitCommand)
                {
                    programmIsOpen = false;
                }
                else
                {
                    int shiftsAmount = int.Parse(input);
                    int tempElemnt;

                    for (int i = 0; i < shiftsAmount; i++)
                    {
                        tempElemnt = array[0];

                        for (int j = 1; j < array.Length; j++)
                            array[j - 1] = array[j];

                        array[array.Length - 1] = tempElemnt;
                    }

                    foreach (int item in array)
                        Console.Write(item + " ");
                }
            }
        }
    }
}
