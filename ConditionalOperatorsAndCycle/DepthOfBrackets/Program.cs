using System;

namespace DepthOfBrackets
{
    internal class Program
    {
        private const char LeftBracket = '(';
        private const char RightBracket = ')';

        static void Main(string[] args)
        {
            Console.WriteLine(@"Введите строку из круглых скобок ""("" или "")"" ");

            string input = Console.ReadLine();

            int maxDepth = 0;
            int currentDepth = 0;

            foreach (char symbol in input)
            {
                if(symbol != LeftBracket && symbol != RightBracket)
                {
                    Console.WriteLine("Введен некорректный символ");
                    return;
                }

                if (symbol == LeftBracket)
                {
                    currentDepth++;

                    if (currentDepth > maxDepth)
                        maxDepth = currentDepth;
                }
                else
                {
                    if(currentDepth <= 0)
                    {
                        Console.WriteLine($"Строка некорректная");
                        return;
                    }

                    currentDepth--;
                }
            }

            Console.WriteLine($"Строка корректная и максимум глубины: {maxDepth}");
        }
    }
}
