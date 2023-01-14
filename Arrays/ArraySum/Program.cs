using System;

namespace ArraySum
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const string SumCommand = "sum";

        static void Main(string[] args)
        {
            int[] storedNumbers = new int[0];
            int[] tempStoredNumbers;

            bool programmIsOpen = true;

            Console.WriteLine("Для добавления введите число");
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
                        if (storedNumbers.Length == 0)
                        {
                            Console.WriteLine("Вы не ввели ни одного числа");
                            break;
                        }

                        int storedNumbersSum = 0;
                        Console.WriteLine("Все введенные числа:");

                        foreach (int item in storedNumbers)
                        {
                            Console.Write(item + " ");
                            storedNumbersSum += item;
                        }

                        Console.WriteLine($"\nСумма всех введенных чисел: {storedNumbersSum}");

                        break;
                    default:
                        tempStoredNumbers = new int[storedNumbers.Length + 1];

                        for (int i = 0; i < storedNumbers.Length; i++)
                        {
                            tempStoredNumbers[i] = storedNumbers[i];
                        }

                        tempStoredNumbers[tempStoredNumbers.Length - 1] = int.Parse(input);
                        storedNumbers = tempStoredNumbers;
                        break;
                }
            }
        }
    }
}
