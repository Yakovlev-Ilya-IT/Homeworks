using System;

namespace ListOfDossiers
{
    internal class Program
    {
        private const string ExitCommand = "0";
        private const string AddPositionCommand = "1";
        private const string ShowAllPositionsCommand = "2";
        private const string DeletePositionCommand = "3";
        private const string SearchByLastNameCommand = "4";

        static void Main(string[] args)
        {
            string[] fullNames = new string[0];
            string[] positions = new string[0];

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
                        AddPosition(ref fullNames, ref positions);
                        break;
                    case ShowAllPositionsCommand:
                        ShowAllPositions(fullNames, positions);
                        break;
                    case DeletePositionCommand:
                        DeletePosition(ref fullNames, ref positions);
                        break;
                    case SearchByLastNameCommand:
                        SearchByLastName(fullNames, positions);
                        break;
                    default:
                        Console.WriteLine("Неверная команда, попробуйте еще");
                        break;
                }
            }
        }

        static void SearchByLastName(string[] fullNames, string[] positions)
        {
            Console.WriteLine("Введите фамилию для поиска");

            string inputSurname = Console.ReadLine();
            bool isMatchNotFound = true;

            for (int i = 0; i < fullNames.Length; i++)
            {
                string[] splitFullName = fullNames[i].Split(' ');

                if (splitFullName[0].ToUpper() == inputSurname.ToUpper())
                {
                    Console.WriteLine($"{fullNames[i]} - {positions[i]}");
                    isMatchNotFound = false;
                }
            }

            if (isMatchNotFound)
                Console.WriteLine("Совпадения не найдены");
        }

        static void DeletePosition(ref string[] fullNames, ref string[] positions)
        {
            if (CheckListForEmptiness(fullNames, positions, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Console.WriteLine("Введите номер досье, которое хотите удалить");

            int inputIndex = int.Parse(Console.ReadLine());

            if(!CheckOutOfBounds(inputIndex - 1, fullNames, positions))
            {
                Console.WriteLine("Введенный номер за пределами списка");
            }

            DeleteArrayElementAtIndex(ref fullNames, inputIndex - 1);
            DeleteArrayElementAtIndex(ref positions, inputIndex - 1);

            Console.WriteLine("Досье успешно удалено");
        }

        private static void DeleteArrayElementAtIndex(ref string[] array, int index)
        {
            for (int i = index + 1; i < array.Length; i++)
                array[i - 1] = array[i];

            array = DecrementArraySize(array);
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

            AddElementToArray(ref fullNames);

            Console.WriteLine("Введите должность");

            AddElementToArray(ref positions);

            Console.WriteLine("Досье успешно добавлено");
        }

        static void AddElementToArray(ref string[] array)
        {
            string input = Console.ReadLine();

            array = IncrementArraySize(array);
            array[array.Length - 1] = input;
        }

        static bool CheckOutOfBounds(int index, string[] fullNames, string[] positions) => index < fullNames.Length && index < positions.Length && index >= 0;

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
            Console.WriteLine($"{DeletePositionCommand} - удалить досье по номеру");
            Console.WriteLine($"{SearchByLastNameCommand} - найти досье по фамилии");
            Console.WriteLine();
        }
    }
}
