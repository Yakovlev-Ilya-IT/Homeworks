using System;

namespace ForCycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите сообщения для повтора:");

                string message = Console.ReadLine();

                Console.WriteLine("Введите количество повторений:");

                int numberOfRepetitions = int.Parse(Console.ReadLine());

                for (int i = 0; i < numberOfRepetitions; i++)
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
