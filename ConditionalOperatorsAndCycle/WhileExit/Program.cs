using System;

namespace WhileExit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exitCondition = "exit";
            string userInput;

            do
            {
                Console.WriteLine("Введите exit, что бы завершить программу: ");
                userInput = Console.ReadLine();

            } while (userInput != exitCondition);
        }
    }
}
