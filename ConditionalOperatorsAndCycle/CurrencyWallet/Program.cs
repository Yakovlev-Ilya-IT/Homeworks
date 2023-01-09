using System;

namespace CurrencyWallet
{
    internal class Program
    {
        const float RubleToDollarExchangeRate = 70.3f, RubleToEuroExchangeRate = 75.08f;
        const float DollarToRubleExchangeRate = 0.014f, DollarToEuroExchangeRate = 1.08f;
        const float EuroToRubbleExchangeRate = 0.013f, EuroToDollarExchangeRate = 0.92f;

        const string ExitCommand = "exit";
        const string ConvertingRubleToDollarCommand = "1";
        const string ConvertingDollarToRubbleCommand = "2";
        const string ConvertingEuroToRubbleCommand = "3";
        const string ConvertingRubbleToEuroCommand = "4";
        const string ConvertingEuroToDollarCommand = "5";
        const string ConvertingDollarToEuroCommand = "6";

        static void Main(string[] args)
        {
            float rublesInWallet = 1000;
            float dollarsInWallet = 1000;
            float euroInWallet = 1000;

            Console.WriteLine("Добро пожаловать в банк, для выхода наберите команду exit");

            while (true)
            {
                Console.WriteLine("Ваш баланс на текущий момент: ");
                Console.WriteLine($"{rublesInWallet} рублей");
                Console.WriteLine($"{dollarsInWallet} долларов");
                Console.WriteLine($"{euroInWallet} евро");

                Console.WriteLine("Выберите необходимую операцию:");
                Console.WriteLine("1 - обменять рубли на доллары");
                Console.WriteLine("2 - обменять доллары на рубли");
                Console.WriteLine("3 - обменять евро на рубли");
                Console.WriteLine("4 - обменять рубли на евро");
                Console.WriteLine("5 - обменять евро на доллары");
                Console.WriteLine("6 - обменять доллары на евро");

                string userInputCommand = Console.ReadLine();

                if (userInputCommand == ExitCommand)
                    break;

                float exchangeCurrencyCount;
                switch (userInputCommand)
                {   
                    case ConvertingRubleToDollarCommand:
                        Console.WriteLine("Обмен рублей на доллары");
                        Console.WriteLine("Сколько рублей вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if(rublesInWallet >= exchangeCurrencyCount)
                        {
                            rublesInWallet -= exchangeCurrencyCount;
                            dollarsInWallet += exchangeCurrencyCount / RubleToDollarExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает рублей на счете");
                        break;
                    case ConvertingDollarToRubbleCommand:
                        Console.WriteLine("Обмен долларов на рубли");
                        Console.WriteLine("Сколько долларов вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if (dollarsInWallet >= exchangeCurrencyCount)
                        {
                            dollarsInWallet -= exchangeCurrencyCount;
                            rublesInWallet += exchangeCurrencyCount / DollarToRubleExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает долларов на счете");
                        break;
                    case ConvertingEuroToRubbleCommand:
                        Console.WriteLine("Обмен евро на рубли");
                        Console.WriteLine("Сколько евро вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if (euroInWallet >= exchangeCurrencyCount)
                        {
                            euroInWallet -= exchangeCurrencyCount;
                            rublesInWallet += exchangeCurrencyCount / EuroToRubbleExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает евро на счете");
                        break;
                    case ConvertingRubbleToEuroCommand:
                        Console.WriteLine("Обмен рубли на евро");
                        Console.WriteLine("Сколько рублей вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if (rublesInWallet >= exchangeCurrencyCount)
                        {
                            rublesInWallet -= exchangeCurrencyCount;
                            euroInWallet += exchangeCurrencyCount / RubleToEuroExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает рублей на счете");
                        break;
                    case ConvertingEuroToDollarCommand:
                        Console.WriteLine("Обмен евро на доллары");
                        Console.WriteLine("Сколько евро вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if (euroInWallet >= exchangeCurrencyCount)
                        {
                            euroInWallet -= exchangeCurrencyCount;
                            dollarsInWallet += exchangeCurrencyCount / EuroToDollarExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает евро на счете");
                        break;
                    case ConvertingDollarToEuroCommand:
                        Console.WriteLine("Обмен доллары на евро");
                        Console.WriteLine("Сколько долларов вы хотите обменять?");
                        exchangeCurrencyCount = float.Parse(Console.ReadLine());
                        if (dollarsInWallet >= exchangeCurrencyCount)
                        {
                            dollarsInWallet -= exchangeCurrencyCount;
                            euroInWallet += exchangeCurrencyCount / DollarToEuroExchangeRate;
                            break;
                        }

                        Console.WriteLine("У вас не хватает долларов на счете");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
