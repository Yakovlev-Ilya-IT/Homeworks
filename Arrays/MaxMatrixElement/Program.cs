using System;

namespace MaxMatrixElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rowsNumber = 10;
            int columnsNumber = 10;

            int[,] matrix = new int[rowsNumber, columnsNumber];

            int maxElement = int.MinValue;
            int replacementElement = 0;

            Random random = new Random();
            int lowerGenerationLimit = 1;
            int upperGenerationLimit = 10;

            Console.WriteLine("Исходная матрица: ");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(lowerGenerationLimit, upperGenerationLimit);
                    Console.Write(matrix[i, j] + " ");

                    if(matrix[i, j] > maxElement)
                        maxElement = matrix[i, j];
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nНаибольший элемент: {maxElement}\n");
            Console.WriteLine("Полученная матрица: ");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == maxElement)
                        matrix[i, j] = replacementElement;

                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine();
            }

        }
    }
}
