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

            Console.WriteLine("Введите длину бара");

            int length = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите символ отрисовки");

            char fillingSymbol = Console.ReadLine()[0];

            DrawBar(x, y, length, percentageFilling, fillingSymbol);
        }

        private static void DrawBar(int x, int y, int length, int percentageFilling, char fillingSymbol)
        {
            char emptySymbol = '_';
            char leftLimitSymbol = '[';
            char rightLimitSymbol = ']';

            float percentageConverter = 1 / 100f;

            int fillingValue = (int)(percentageFilling * length * percentageConverter);

            if(fillingValue > length)
                fillingValue = length;

            string bar = "";

            for (int i = 0; i < fillingValue; i++)
                bar += fillingSymbol;

            for (int i = fillingValue; i < length; i++)
                bar += emptySymbol;

            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.Write(leftLimitSymbol);
            Console.Write(bar);
            Console.WriteLine(rightLimitSymbol);
        }
    }
}
