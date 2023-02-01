using System;
using System.Collections.Generic;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exitCommand = "exit";

            Dictionary<string, string> wordToMeaning = new Dictionary<string, string>()
            {
                {"арбуз", "это ягода" },
                {"ютуб", "это видеохостинг" },
                {"множество", "это совокупность объектов" },
            };

            bool programmIsRunning = true;

            while (programmIsRunning)
            {
                Console.WriteLine("Введите слово и узнайте его значение, для выхода введите exit");

                string input = Console.ReadLine();

                if (wordToMeaning.ContainsKey(input))
                    Console.WriteLine(wordToMeaning[input]);
                else
                    Console.WriteLine("Такого слова нет в словаре:(");

                if (input == exitCommand)
                    programmIsRunning = false;
            }
        }
    }
}
