using System;
using System.Collections.Generic;

namespace Dossiers
{
    internal class Program
    {
        private const string ExitCommand = "0";
        private const string AddPositionCommand = "1";
        private const string ShowAllPositionsCommand = "2";
        private const string DeletePositionCommand = "3";

        static void Main(string[] args)
        {
            Dictionary<string, string> fullNamesToPosition = new Dictionary<string, string>();

            bool isProgrammRunning = true;

            Console.WriteLine("Добро пожаловать в отдел досье");

            while (isProgrammRunning)
            {
                ShowMenu();

                string userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case ExitCommand:
                        isProgrammRunning = false;
                        break;
                    case AddPositionCommand:
                        AddPosition(fullNamesToPosition);
                        break;
                    case ShowAllPositionsCommand:
                        ShowAllPositions(fullNamesToPosition);
                        break;
                    case DeletePositionCommand:
                        DeletePosition(fullNamesToPosition);
                        break;
                    default:
                        Console.WriteLine("Неверная команда, попробуйте еще");
                        break;
                }
            }
        }

        static void DeletePosition(Dictionary<string, string> fullNamesToPosition)
        {
            if (CheckListForEmptiness(fullNamesToPosition, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Console.WriteLine("Введите номер досье, которое хотите удалить");

            if (int.TryParse(Console.ReadLine(), out int inputIndex))
            {
                if (CheckOutOfBounds(inputIndex - 1, fullNamesToPosition) == false)
                {
                    Console.WriteLine("Введенный номер за пределами списка");
                    return;
                }

                DeleteDictionaryElementAtIndex(fullNamesToPosition, inputIndex - 1);

                Console.WriteLine("Досье успешно удалено");
            }
            else
            {
                Console.WriteLine("Попробуйте ввести число");
            }
        }

        private static void DeleteDictionaryElementAtIndex(Dictionary<string, string> dictionary, int index)
        {
            List<string> keys = new List<string>(dictionary.Keys);

            dictionary.Remove(keys[index]);
        }

        static void ShowAllPositions(Dictionary<string, string> fullNamesToPosition)
        {
            if (CheckListForEmptiness(fullNamesToPosition, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            int index = 1;

            foreach (var item in fullNamesToPosition)
            {
                Console.WriteLine($"{index}. {item.Key} - {item.Value}");
                index++;
            }
        }

        static void AddPosition(Dictionary<string, string> fullNamesToPosition)
        {
            Console.WriteLine("Введите ФИО через пробел");

            string fullName = Console.ReadLine();

            Console.WriteLine("Введите должность");

            string position = Console.ReadLine();

            fullNamesToPosition.Add(fullName, position);

            Console.WriteLine("Досье успешно добавлено");
        }

        static bool CheckOutOfBounds(int index, Dictionary<string, string> fullNamesToPosition) => index < fullNamesToPosition.Count && index >= 0;

        static bool CheckListForEmptiness(Dictionary<string, string> fullNamesToPosition, out string errorMessage)
        {
            if (fullNamesToPosition.Count == 0)
            {
                errorMessage = "Список пуст";
                return true;
            }

            errorMessage = string.Empty;
            return false;
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Введите одну из команд:");
            Console.WriteLine($"{ExitCommand} - выйти из программы");
            Console.WriteLine($"{AddPositionCommand} - добавить досье");
            Console.WriteLine($"{ShowAllPositionsCommand} - вывести все досье");
            Console.WriteLine($"{DeletePositionCommand} - удалить досье по номеру");
            Console.WriteLine();
        }
    }
}
