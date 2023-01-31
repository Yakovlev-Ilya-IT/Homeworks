using System;

namespace BarDrawing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите позицию бара по x");

            int x = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите позицию бара по y");

            int y = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите процент заполненности бара");

            int percentageFilling = int.Parse(Console.ReadLine());

            DrawBar(x, y, percentageFilling);
        }

        private static void DrawBar(int x, int y, int percentageFilling)
        {
            char fillingSymbol = '#';
            char emptySymbol = '_';
            char leftLimitSymbol = '[';
            char rightLimitSymbol = ']';

            int maxValue = 10;
            int fillingValue = (int)(percentageFilling / 100f * maxValue);

            string bar = "";

            for (int i = 0; i < fillingValue; i++)
                bar += fillingSymbol;

            for (int i = fillingValue; i < maxValue; i++)
                bar += emptySymbol;

            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write(leftLimitSymbol);
            Console.Write(bar);
            Console.WriteLine(rightLimitSymbol);
        }
    }
}
