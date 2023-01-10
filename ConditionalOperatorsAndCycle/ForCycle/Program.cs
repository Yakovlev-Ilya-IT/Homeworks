using System;

namespace ForCycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exitCommand = "1";
            string inputCommand;

            do
            {
                Console.WriteLine("Введите сообщения для повтора:");

                string message = Console.ReadLine();

                Console.WriteLine("Введите количество повторений:");

                int numberOfRepetitions = int.Parse(Console.ReadLine());

                for (int i = 0; i < numberOfRepetitions; i++)
                {
                    Console.WriteLine(message);
                }

                Console.WriteLine($"Для выхода из программы введите {exitCommand}");
                Console.WriteLine($"Для продолжения введите любое сообщение или нажмите Enter");

                inputCommand = Console.ReadLine();
            } while(inputCommand != exitCommand);
        }
    }
}
