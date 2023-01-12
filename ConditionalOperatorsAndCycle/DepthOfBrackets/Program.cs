using System;

namespace DepthOfBrackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const char leftBracket = '(';
            const char rightBracket = ')';

            Console.WriteLine(@"Введите строку из круглых скобок ""("" или "")"" ");

            string input = Console.ReadLine();

            int maxDepth = 0;
            int currentDepth = 0;

            foreach (char symbol in input)
            {
                if(symbol != leftBracket && symbol != rightBracket)
                {
                    Console.WriteLine("Введен некорректный символ");
                    return;
                }

                if (symbol == leftBracket)
                {
                    currentDepth++;

                    if (currentDepth > maxDepth)
                        maxDepth = currentDepth;
                }
                else
                {
                    currentDepth--;

                    if (currentDepth < 0)
                        break;
                }
            }

            if(currentDepth == 0)
                Console.WriteLine($"Строка корректная и максимум глубины: {maxDepth}");
            else
                Console.WriteLine($"Строка некорректная");
        }
    }
}
