using System;

namespace PlayerDatabase
{
    internal class Program
    {
        private const string AddPlayerCommand = "add";
        private const string BanCommand = "ban";
        private const string UnbanCommand = "unban";
        private const string RemovePlayerCommand = "remove";
        private const string ShowPlayersInfoCommand = "show";
        private const string ExitCommand = "exit";

        static void Main(string[] args)
        {
            PlayerCreator creator = new PlayerCreator();
            PlayerDatabase database = new PlayerDatabase();

            bool isProgramRunning = true;

            while (isProgramRunning)
            {
                ShowMenu();

                string input = Console.ReadLine();  

                switch (input)
                {
                    case AddPlayerCommand:
                        Add(creator, database);
                        break;

                    case BanCommand:
                        Ban(database);
                        break;

                    case UnbanCommand:
                        Unban(database);
                        break;

                    case RemovePlayerCommand:
                        Remove(database);
                        break;

                    case ShowPlayersInfoCommand:
                        database.ShowInformation();
                        break;

                    case ExitCommand:
                        isProgramRunning = false;
                        break;

                    default:
                        Console.WriteLine("Команда не распознана, попробуйте еще");
                        break;
                }
            }
        }

        private static void Remove(PlayerDatabase database)
        {
            if (HandleIdInput(out uint id))
            {
                if (database.Remove(id))
                {
                    Console.WriteLine("Игрок успешно удален");
                    return;
                }
            }

            Console.WriteLine("Удалить игрока не удалось");
        }

        private static void Unban(PlayerDatabase database)
        {
            if (HandleIdInput(out uint id))
            {
                if (database.Unban(id))
                {
                    Console.WriteLine("Игрок успешно разбанен");
                    return;
                }
            }

            Console.WriteLine("Разбанить игрока не удалось");
        }

        private static void Ban(PlayerDatabase database)
        {
            if (HandleIdInput(out uint id))
            {
                if (database.Ban(id))
                {
                    Console.WriteLine("Игрок успешно забаен");
                    return;
                }
            }

            Console.WriteLine("Забанить игрока не удалось");
        }

        private static void Add(PlayerCreator creator, PlayerDatabase database)
        {
            if(TryCreatePlayer(creator, out Player player))
            {
                if (database.Add(player))
                {
                    Console.WriteLine("Игрок успешно добавлен");
                    return;
                }
            }

            Console.WriteLine("Новый игрок не добавлен в базу данных");
        }

        private static bool TryCreatePlayer(PlayerCreator creator, out Player player)
        {
            Console.WriteLine("Введите имя нового игрока");

            string name = Console.ReadLine();

            Console.WriteLine("Введите желаемый уровень");

            if(HandleLevelInput(out int level))
            {
                player = creator.Create(name, level);
                return true;
            }
            else
            {
                Console.WriteLine("Игрока не получилось создать");
                player = null;
                return false;
            }
        }

        private static bool HandleIdInput(out uint id)
        {
            Console.WriteLine("Введите id игрока");

            if (uint.TryParse(Console.ReadLine(), out id))
                return true;

            Console.WriteLine("Похоже вы не ввели неотрицательное число:(");
            return false;
        }

        private static bool HandleLevelInput(out int level)
        {
            if (int.TryParse(Console.ReadLine(), out level))
            {
                if(level < 0)
                {
                    Console.WriteLine("Уровень не может быть отрицательным");
                    return false;
                }

                return true;
            }
            else
            {
                Console.WriteLine("Похоже вы ввели не число:(");
                return false;
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Это меню работы с базой данных, выберите нужное действие:");
            Console.WriteLine($"{AddPlayerCommand} - для добавления нового игрока в базу данных");
            Console.WriteLine($"{BanCommand} - для бана игрока по id");
            Console.WriteLine($"{UnbanCommand} - для разбана игрока по id");
            Console.WriteLine($"{RemovePlayerCommand} - для удаления игрока по id");
            Console.WriteLine($"{ShowPlayersInfoCommand} - для просмотра информации об игроках");
            Console.WriteLine($"{ExitCommand} - для выхода");
            Console.WriteLine();
        }
    }
}
