using System;

namespace DepthOfBrackets
{
    internal class Program
    {
        private const char LeftBracket = '(';
        private const char RightBracket = ')';
        private const char EmptySymbol = ' ';
        private const int MinDepth = 1;

        static void Main(string[] args)
        {
            Console.WriteLine(@"Введите строку из круглых скобок ""("" или "")"" ");

            string input = Console.ReadLine();

            int maxDepth = MinDepth;
            int currentDepth = MinDepth;
            int leftBracketCounter = 0;
            int rightBracketCounter = 0;
            char previousSymbol = EmptySymbol;

            foreach (char symbol in input)
            {
                if(symbol != LeftBracket && symbol != RightBracket)
                {
                    Console.WriteLine("Введен некорректный символ");
                    break;
                }

                if (previousSymbol == symbol)
                {
                    currentDepth++;

                    if (currentDepth > maxDepth)
                        maxDepth = currentDepth;
                }
                else
                {
                    currentDepth = MinDepth;
                    previousSymbol = symbol;
                }

                if(previousSymbol == EmptySymbol)
                    previousSymbol = symbol;

                if (symbol == LeftBracket)
                    leftBracketCounter++;

                if(symbol == RightBracket)
                    rightBracketCounter++;
            }

            if (rightBracketCounter == leftBracketCounter)
            {
                Console.WriteLine($"Строка корректная и максимум глубины: {maxDepth}");
            }
            else
            {
                Console.WriteLine($"Строка некорректная");
            }
        }
    }
}
