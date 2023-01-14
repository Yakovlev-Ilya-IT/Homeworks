using System;

namespace TwoDimensionalArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[5,5];

            int rowSum = 0;
            int columnMultiplication = 1;
            int numberOfCalculatedRow = 1;
            int numberOfCalculatedColumn = 0;

            Random random = new Random();
            int lowerGenerationLimit = 1;
            int upperGenerationLimit = 10;

            Console.WriteLine("Исходная матрица: ");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i,j] = random.Next(lowerGenerationLimit, upperGenerationLimit);
                    Console.Write(array[i,j] + " ");

                    if(i == numberOfCalculatedRow)
                        rowSum += array[i,j];

                    if(j == numberOfCalculatedColumn)
                        columnMultiplication *= array[i,j]; 
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Сумма {numberOfCalculatedRow + 1} строки: {rowSum}");
            Console.WriteLine($"Произведение {numberOfCalculatedColumn + 1} столбца: {columnMultiplication}");
        }
    }
}
