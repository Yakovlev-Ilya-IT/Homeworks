using System;

namespace ListOfDossiers
{
    internal class Program
    {
        private const string ExitCommand = "0";
        private const string AddPositionCommand = "1";
        private const string ShowAllPositionsCommand = "2";
        private const string DeleteLastPositionCommand = "3";
        private const string SearchByLastNameCommand = "4";

        static void Main(string[] args)
        {
            string[] fullNames = new string[0];
            string[] positions = new string[0];

            bool programmIsRunning = true;

            Console.WriteLine("Добро пожаловать в отдел досье");

            while (programmIsRunning)
            {
                ShowMenu();

                string userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case ExitCommand:
                        programmIsRunning = false;
                        break;
                    case AddPositionCommand:
                        AddPosition(ref fullNames, ref positions);
                        break;
                    case ShowAllPositionsCommand:
                        ShowAllPositions(fullNames, positions);
                        break;
                    case DeleteLastPositionCommand:
                        DeleteLastPosition(ref fullNames, ref positions);
                        break;
                    case SearchByLastNameCommand:
                        if (TrySearchByLastName(fullNames, positions, out string fullName, out string position))
                            Console.WriteLine($"{fullName} - {position}");
                        else
                            Console.WriteLine("Данной фамилии нет");

                        break;
                    default:
                        Console.WriteLine("Неверная команда, попробуйте еще");
                        break;
                }
            }
        }

        static bool TrySearchByLastName(string[] fullNames, string[] positions, out string fullName, out string position)
        {
            Console.WriteLine("Введите фамилию для поиска");

            string inputSurname = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                string[] splitFullName = fullNames[i].Split(' ');

                if (splitFullName[0].ToUpper() == inputSurname.ToUpper())
                {
                    fullName = fullNames[i];
                    position = positions[i];
                    return true;
                }
            }

            fullName = string.Empty;
            position = string.Empty;

            return false;
        }

        static void DeleteLastPosition(ref string[] fullNames, ref string[] positions)
        {
            if (CheckListForEmptiness(fullNames, positions, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            fullNames = DecrementArraySize(fullNames);
            positions = DecrementArraySize(positions);
            Console.WriteLine("Последнее досье успешно удалено");
        }

        static void ShowAllPositions(string[] fullNames, string[] positions)
        {
            if (CheckListForEmptiness(fullNames, positions, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            for (int i = 0; i < fullNames.Length; i++)
                Console.WriteLine($"{i+1}. {fullNames[i]} - {positions[i]}");
        }

        static void AddPosition(ref string[] fullNames, ref string[] positions)
        {
            Console.WriteLine("Введите ФИО через пробел");
            
            string input = Console.ReadLine();

            fullNames = IncrementArraySize(fullNames);
            fullNames[fullNames.Length - 1] = input;

            Console.WriteLine("Введите должность");

            input = Console.ReadLine();

            positions = IncrementArraySize(positions);
            positions[positions.Length - 1] = input;

            Console.WriteLine("Досье успешно добавлена");
        }

        static bool CheckListForEmptiness(string[] fullNames, string[] positions, out string errorMessage)
        {
            if (fullNames.Length == 0 || positions.Length == 0)
            {
                errorMessage = "Список пуст";
                return true;
            }

            errorMessage = string.Empty;
            return false;
        }

        static string[] IncrementArraySize(string[] array)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tempArray[i] = array[i];

            return tempArray;
        }

        static string[] DecrementArraySize(string[] array)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < tempArray.Length; i++)
                tempArray[i] = array[i];

            return tempArray;
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Введите одну из команд:");
            Console.WriteLine($"{ExitCommand} - выйти из программы");
            Console.WriteLine($"{AddPositionCommand} - добавить досье");
            Console.WriteLine($"{ShowAllPositionsCommand} - вывести все досье");
            Console.WriteLine($"{DeleteLastPositionCommand} - удалить последнее досье");
            Console.WriteLine($"{SearchByLastNameCommand} - найти досье по фамилии");
            Console.WriteLine();
        }
    }
}
