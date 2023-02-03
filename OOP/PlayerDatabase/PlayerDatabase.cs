using System.Collections.Generic;
using System;

namespace PlayerDatabase
{
    public class PlayerDatabase
    {
        private Dictionary<uint, Player> _players;

        private const string AddPlayerCommand = "add";
        private const string BanCommand = "ban";
        private const string UnbanCommand = "unban";
        private const string RemovePlayerCommand = "remove";
        private const string ShowPlayersInfoCommand = "show";
        private const string ExitCommand = "exit";

        public PlayerDatabase(List<Player> players)
        {
            _players = new Dictionary<uint, Player>();

            foreach (var player in players)
                _players.Add(player.Id, player);
        }

        public PlayerDatabase() => _players = new Dictionary<uint, Player>();

        public void Work(PlayerCreator creator)
        {
            bool isRunning = true;

            while (isRunning)
            {
                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case AddPlayerCommand:
                        Add(creator);
                        break;

                    case BanCommand:
                        Ban();
                        break;

                    case UnbanCommand:
                        Unban();
                        break;

                    case RemovePlayerCommand:
                        Remove();
                        break;

                    case ShowPlayersInfoCommand:
                        ShowInformation();
                        break;

                    case ExitCommand:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Команда не распознана, попробуйте еще");
                        break;
                }
            }
        }

        private void Add(PlayerCreator creator)
        {
            if (TryCreatePlayer(creator, out Player player) == false)
                return;

            if (_players.ContainsKey(player.Id))
            {
                Console.WriteLine("Похоже такой игрок уже добавлен в базу данных");
                return;
            }

            _players.Add(player.Id, player);
            Console.WriteLine("Игрок успешно добавлен");
        }

        private bool TryCreatePlayer(PlayerCreator creator, out Player player)
        {
            Console.WriteLine("Введите имя нового игрока");

            string name = Console.ReadLine();

            Console.WriteLine("Введите желаемый уровень");

            if (HandleLevelInput(out int level))
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

        private bool HandleLevelInput(out int level)
        {
            if (int.TryParse(Console.ReadLine(), out level))
            {
                if (level < 0)
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

        private void Remove()
        {
            if (HandleIdInput(out uint id) == false)
                return;

            if (_players.ContainsKey(id))
            {
                _players.Remove(id);
                Console.WriteLine("Игрок успешно удален");
                return;
            }

            ShowNotFoundIdMessage(id);
        }

        private void Ban()
        {
            if (HandleIdInput(out uint id) == false)
                return;

            if (_players.ContainsKey(id))
            {
                _players[id].Ban();
                Console.WriteLine("Игрок успешно забаен");
                return;
            }

            ShowNotFoundIdMessage(id);
        }

        private void Unban()
        {
            if (HandleIdInput(out uint id) == false)
                return;

            if (_players.ContainsKey(id))
            {
                _players[id].Unban();
                Console.WriteLine("Игрок успешно разбанен");
                return;
            }

            ShowNotFoundIdMessage(id);
        }

        private bool HandleIdInput(out uint id)
        {
            Console.WriteLine("Введите id игрока");

            if (uint.TryParse(Console.ReadLine(), out id))
                return true;

            Console.WriteLine("Похоже вы не ввели неотрицательное число:(");
            return false;
        }

        private void ShowInformation()
        {
            if(_players.Count == 0)
            {
                Console.WriteLine("База данных пуста");
                return;
            }

            foreach (var item in _players)
                item.Value.ShowInformation();
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

        private void ShowNotFoundIdMessage(uint id) => Console.WriteLine($"Игрока с id {id} нет в базе данных");
    }
}
