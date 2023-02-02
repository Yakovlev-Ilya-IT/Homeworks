using System;
using System.Collections.Generic;

namespace ListSum
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const string SumCommand = "sum";

        static void Main(string[] args)
        {
            List<int> storedNumbers = new List<int>();

            bool programmIsOpen = true;

            Console.WriteLine("Для добавления введите целое число");
            Console.WriteLine($"Для выхода из программы введите {ExitCommand}");
            Console.WriteLine($"Для суммирования введенных чисел введите {SumCommand}\n");

            while (programmIsOpen)
            {
                Console.Write("Ввод: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case ExitCommand:
                        programmIsOpen = false;
                        break;

                    case SumCommand:
                        Sum(storedNumbers);
                        break;

                    default:
                        HandleAddInput(input, storedNumbers);
                        break;
                }
            }
        }

        static void Sum(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("Вы не ввели ни одного числа");
                return;
            }

            int storedNumbersSum = 0;
            Console.WriteLine("Все введенные числа:");

            foreach (int item in numbers)
            {
                Console.Write(item + " ");
                storedNumbersSum += item;
            }

            Console.WriteLine($"\nСумма всех введенных чисел: {storedNumbersSum}");
        }

        static void HandleAddInput(string input, List<int> storedNumbers)
        {
            if (int.TryParse(input, out int number))
                storedNumbers.Add(number);
            else
                Console.WriteLine("Ой, похоже вы ввели что-то не то:(");
        }
    }
}
