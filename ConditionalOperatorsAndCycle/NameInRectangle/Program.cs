using System;

namespace NameInRectangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exitCommand = "exit";
            int outlineSymbolLength = 1;

            string inputCommand;

            do
            {
                Console.WriteLine("Введите имя, а я его украшу:)");

                string name = Console.ReadLine();

                Console.WriteLine("А теперь введите символ для украшения:)");

                string outlineSymbol = Console.ReadLine();

                if (outlineSymbol.Length != outlineSymbolLength)
                {
                    Console.WriteLine("Упс, это не похоже на символ :(");
                }
                else
                {
                    string nameInOutline = $"{outlineSymbol}{name}{outlineSymbol}";
                    string outline = new string(outlineSymbol[0], nameInOutline.Length);

                    Console.WriteLine(outline);
                    Console.WriteLine(nameInOutline);
                    Console.WriteLine(outline);
                }

                Console.WriteLine($"Для выхода из программы введите {exitCommand}");
                Console.WriteLine($"Для продолжения введите любое сообщение или нажмите Enter");

                inputCommand = Console.ReadLine();

            } while (inputCommand != exitCommand);
        }
    }
}
