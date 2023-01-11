﻿using System;

namespace NameInRectangle
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const int OutlineSymbolLength = 1;
        private const int MinOutlineRectWidth = 2;
        private const int OutlineRectHeight = 3;
        private const int NumberOfNameRow = 1;

        static void Main(string[] args)
        {
            string inputCommand;

            do
            {
                Console.WriteLine("Введите имя, а я его украшу:)");

                string name = Console.ReadLine();

                Console.WriteLine("А теперь введите символ для украшения:)");

                string outlineSymbol = Console.ReadLine();

                if (outlineSymbol.Length != OutlineSymbolLength)
                {
                    Console.WriteLine("Упс, это не похоже на символ :(");
                }
                else
                {
                    int outlineRectLength = name.Length + MinOutlineRectWidth;

                    for (int i = 0; i < OutlineRectHeight; i++)
                    {
                        if(i == NumberOfNameRow)
                        {
                            Console.Write(outlineSymbol);
                            Console.Write(name);
                            Console.WriteLine(outlineSymbol);
                        }
                        else
                        {
                            for (int j = 0; j < outlineRectLength; j++)
                                Console.Write(outlineSymbol);
                            Console.WriteLine();
                        }
                    }
                }

                Console.WriteLine($"Для выхода из программы введите {ExitCommand}");
                Console.WriteLine($"Для продолжения введите любое сообщение или нажмите Enter");

                inputCommand = Console.ReadLine();

            } while (inputCommand != ExitCommand);
        }
    }
}