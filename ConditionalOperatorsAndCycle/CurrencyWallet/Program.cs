using System;

namespace CurrencyWallet
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const string ConvertingRubleToDollarCommand = "1";
        private const string ConvertingDollarToRubbleCommand = "2";
        private const string ConvertingEuroToRubbleCommand = "3";
        private const string ConvertingRubbleToEuroCommand = "4";
        private const string ConvertingEuroToDollarCommand = "5";
        private const string ConvertingDollarToEuroCommand = "6";

        static void Main(string[] args)
        {
            float rubleToDollarExchangeRate = 70.3f, rubleToEuroExchangeRate = 75.08f;
            float dollarToRubleExchangeRate = 0.014f, dollarToEuroExchangeRate = 1.08f;
            float euroToRubbleExchangeRate = 0.013f, euroToDollarExchangeRate = 0.92f;

            float rublesInWallet = 1000;
            float dollarsInWallet = 1000;
            float euroInWallet = 1000;

            string userInputCommand;
            bool programmIsCompleted = false;

            Console.WriteLine("Добро пожаловать в банк, для выхода наберите команду exit");

            do
            {
                Console.WriteLine("Ваш баланс на текущий момент: ");
                Console.WriteLine($"{rublesInWallet} рублей");
                Console.WriteLine($"{dollarsInWallet} долларов");
                Console.WriteLine($"{euroInWallet} евро");

                Console.WriteLine("Выберите необходимую операцию:");
                Console.WriteLine($"{ConvertingRubleToDollarCommand} - обменять рубли на доллары");
                Console.WriteLine($"{ConvertingDollarToRubbleCommand} - обменять доллары на рубли");
                Console.WriteLine($"{ConvertingEuroToRubbleCommand} - обменять евро на рубли");
                Console.WriteLine($"{ConvertingRubbleToEuroCommand} - обменять рубли на евро");
                Console.WriteLine($"{ConvertingEuroToDollarCommand} - обменять евро на доллары");
                Console.WriteLine($"{ConvertingDollarToEuroCommand} - обменять доллары на евро");

                userInputCommand = Console.ReadLine();

                float exchangeCurrencyCount;
                switch (userInputCommand)
                {
                    case ExitCommand:
                        programmIsCompleted = true;
                        break;
                    case ConvertingRubleToDollarCommand:
                        Console.WriteLine("Обмен рублей на доллары");
                        Console.WriteLine("Сколько рублей вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if(rublesInWallet >= exchangeCurrencyCount)
                        {
                            rublesInWallet -= exchangeCurrencyCount;
                            dollarsInWallet += exchangeCurrencyCount / rubleToDollarExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает рублей на счете");
                        }

                        break;
                    case ConvertingDollarToRubbleCommand:
                        Console.WriteLine("Обмен долларов на рубли");
                        Console.WriteLine("Сколько долларов вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if (dollarsInWallet >= exchangeCurrencyCount)
                        {
                            dollarsInWallet -= exchangeCurrencyCount;
                            rublesInWallet += exchangeCurrencyCount / dollarToRubleExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает долларов на счете");
                        }

                        break;
                    case ConvertingEuroToRubbleCommand:
                        Console.WriteLine("Обмен евро на рубли");
                        Console.WriteLine("Сколько евро вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if (euroInWallet >= exchangeCurrencyCount)
                        {
                            euroInWallet -= exchangeCurrencyCount;
                            rublesInWallet += exchangeCurrencyCount / euroToRubbleExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает евро на счете");
                        }

                        break;
                    case ConvertingRubbleToEuroCommand:
                        Console.WriteLine("Обмен рубли на евро");
                        Console.WriteLine("Сколько рублей вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if (rublesInWallet >= exchangeCurrencyCount)
                        {
                            rublesInWallet -= exchangeCurrencyCount;
                            euroInWallet += exchangeCurrencyCount / rubleToEuroExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает рублей на счете");
                        }

                        break;
                    case ConvertingEuroToDollarCommand:
                        Console.WriteLine("Обмен евро на доллары");
                        Console.WriteLine("Сколько евро вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if (euroInWallet >= exchangeCurrencyCount)
                        {
                            euroInWallet -= exchangeCurrencyCount;
                            dollarsInWallet += exchangeCurrencyCount / euroToDollarExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает евро на счете");
                        }

                        break;
                    case ConvertingDollarToEuroCommand:
                        Console.WriteLine("Обмен доллары на евро");
                        Console.WriteLine("Сколько долларов вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());

                        if (dollarsInWallet >= exchangeCurrencyCount)
                        {
                            dollarsInWallet -= exchangeCurrencyCount;
                            euroInWallet += exchangeCurrencyCount / dollarToEuroExchangeRate;
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает долларов на счете");
                        }

                        break;
                    default:
                        break;
                }
            } while (!programmIsCompleted);
        }
    }
}
