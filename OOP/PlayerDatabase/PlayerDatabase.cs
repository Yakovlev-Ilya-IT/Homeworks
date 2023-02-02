using System.Collections.Generic;
using System;

namespace PlayerDatabase
{
    public class PlayerDatabase
    {
        private Dictionary<uint, Player> _players;

        public PlayerDatabase(List<Player> players)
        {
            _players = new Dictionary<uint, Player>();

            foreach (var player in players)
                _players.Add(player.Id, player);;
        }

        public PlayerDatabase() => _players = new Dictionary<uint, Player>();

        public bool Add(Player player)
        {
            if (_players.ContainsKey(player.Id))
            {
                Console.WriteLine("Похоже такой игрок уже добавлен в базу данных");
                return false;
            }

            _players.Add(player.Id, player);
            return true;
        }

        public bool Remove(uint id)
        {
            if (_players.ContainsKey(id))
            {
                _players.Remove(id);
                return true;
            }

            ShowNotFoundIdMessage(id);
            return false;
        }

        public bool Ban(uint id)
        {
            if (_players.ContainsKey(id))
            {
                _players[id].Ban();
                return true;
            }

            ShowNotFoundIdMessage(id);
            return false;
        }

        public bool Unban(uint id)
        {
            if (_players.ContainsKey(id))
            {
                _players[id].UnBan();
                return true;
            }

            ShowNotFoundIdMessage(id);
            return false;
        }

        public void ShowInformation()
        {
            if(_players.Count == 0)
            {
                Console.WriteLine("База данных пуста");
                return;
            }

            foreach (var item in _players)
                item.Value.ShowInformation();
        }

        private void ShowNotFoundIdMessage(uint id) => Console.WriteLine($"Игрока с id {id} нет в базе данных");
    }
}
