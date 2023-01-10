using System;

namespace SecretPassword
{
    internal class Program
    {
        private const int NumberOfTries = 3;
        private const string SecretMessage = "А ты хороооош мужик... хорооош!";

        static void Main(string[] args)
        {
            Console.WriteLine("Введите первоначальный пароль: ");
            string password = Console.ReadLine();

            for (int i = 0; i < NumberOfTries; i++)
            {
                Console.WriteLine("Введите пароль: ");
                string userInput = Console.ReadLine();

                if(userInput == password)
                {
                    Console.WriteLine(SecretMessage);
                    break;
                }

                int remainingAttempts = NumberOfTries - (i + 1);

                if(remainingAttempts <= 0)
                {
                    Console.WriteLine("так и знал, что ты вор, а ну вон отсюда!");
                    break;
                }

                Console.WriteLine($"Пароль неверный, оставшееся количество попыток: {remainingAttempts}");
            }
        }
    }
}
