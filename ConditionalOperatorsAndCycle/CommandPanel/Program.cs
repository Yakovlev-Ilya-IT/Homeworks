using System;

namespace CommandPanel
{
    internal class Program
    {
        const string ExitCommand = "0";
        const string ClearConsoleCommand = "1";
        const string ChangeTextColorCommand = "2";
        const string ChangeProgramTitleCommand = "3";
        const string GetJokeCommand = "4";
        const string ChangeConsoleSizeCommand = "5";

        static void Main(string[] args)
        {
            Console.WriteLine("Рад приветствовать на борту моего пианино, можете попробовать любую из команд:)");

            string userInputCommand;

            do
            {
                Console.WriteLine("Выберите необходимую команду:");
                Console.WriteLine($"{ExitCommand} - выйти из программы");
                Console.WriteLine($"{ClearConsoleCommand} - очистить консоль");
                Console.WriteLine($"{ChangeTextColorCommand} - изменить цвет текста на случайный");
                Console.WriteLine($"{ChangeProgramTitleCommand} - изменить название программы");
                Console.WriteLine($"{GetJokeCommand} - получить гениальную шутку:)");
                Console.WriteLine($"{ChangeConsoleSizeCommand} - изменить размеры консоли");

                userInputCommand = Console.ReadLine();
                Random random = new Random();

                switch (userInputCommand)
                {
                    case ExitCommand:
                        Console.WriteLine("Нееет, я буду ждать вашего возвращения!");
                        break;
                    case ClearConsoleCommand:
                        Console.Clear();
                        break;
                    case ChangeTextColorCommand:
                        Console.ForegroundColor = (ConsoleColor)random.Next(0, Enum.GetValues(typeof(ConsoleColor)).Length);
                        break;
                    case ChangeProgramTitleCommand:
                        Console.Write("Введите новое название: ");
                        string inputProgramName = Console.ReadLine();
                        Console.Title = inputProgramName;
                        Console.WriteLine($"Название успешно сменено на {inputProgramName}");
                        break;
                    case GetJokeCommand:
                        Console.WriteLine("Колобок повесился, хаХахАхахАха, колобок понял??? Повесился! АПХхаПХАХа");
                        Console.WriteLine("Шутка успешно выдана");
                        break;
                    case ChangeConsoleSizeCommand:
                        Console.Write("Введите желаемую длину: ");
                        int windowWidth = int.Parse(Console.ReadLine());

                        if(windowWidth > Console.LargestWindowWidth)
                        {
                            Console.WriteLine($"Прости, но максимально возможная длина это {Console.LargestWindowWidth}");
                            break;
                        }

                        Console.Write("Введите желаемую высоту: ");
                        int windowHeight = int.Parse(Console.ReadLine());

                        if (windowHeight > Console.LargestWindowHeight)
                        {
                            Console.WriteLine($"Прости, но максимально возможная высота это {Console.LargestWindowHeight}");
                            break;
                        }

                        Console.SetWindowSize(windowWidth,windowHeight);
                        Console.WriteLine("Размеры успешно изменены");
                        break;
                    default:
                        Console.WriteLine("Такая команда, к сожалению, недоступна :(");
                        break;
                }

            } while (userInputCommand != ExitCommand);
        }
    }
}
