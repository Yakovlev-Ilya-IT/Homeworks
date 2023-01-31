using System;

namespace ConvertNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет введи число, а я постараюсь его вывести:)");

            int inputNumber = GetNumberFromInput();

            Console.WriteLine($"Твое число: {inputNumber}");
        }

        static int GetNumberFromInput()
        {
            int resultNumber = 0;
            bool isInputConvertSuccessfull = false;

            while (!isInputConvertSuccessfull)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    resultNumber = number;
                    isInputConvertSuccessfull = true;
                }
                else
                {
                    Console.WriteLine("Что-то пошло не так, попробуй снова:)");
                }
            }

            return resultNumber;
        }
    }
}
