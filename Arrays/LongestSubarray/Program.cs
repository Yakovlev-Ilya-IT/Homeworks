using System;

namespace LongestSubarray
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

            int mostRepeatedNumber = array[random.Next(0, array.Length)];
            int maxAmountOfRepetion = 0;

            int amountOfRepetion = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                {
                    amountOfRepetion++;

                    if (amountOfRepetion > maxAmountOfRepetion)
                    {
                        maxAmountOfRepetion = amountOfRepetion;
                        mostRepeatedNumber = array[i];
                    }
                }
                else
                {
                    amountOfRepetion = 0;
                }
            }

            Console.WriteLine($"\nЧисло {mostRepeatedNumber} повторяется больше всего раз подряд: {maxAmountOfRepetion} раз");
        }
    }
}
