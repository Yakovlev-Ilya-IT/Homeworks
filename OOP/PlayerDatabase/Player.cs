using System;

namespace PlayerDatabase
{
    public class Player
    {
        private uint _id;
        private string _name;
        private int _level;
        private bool _isBanned;

        public uint Id => _id;
        public string Name => _name;
        public int Level => _level;
        public bool IsBanned => _isBanned;

        public Player(uint id, string name, int level)
        {
            _id = id;
            _name = name;

            if(level < 0)
                _level = 0;
            else
                _level = level;

            _isBanned = false;
        }

        public void Ban() => _isBanned = true;

        public void UnBan() => _isBanned = false;

        public void ShowInformation() => Console.WriteLine($"id - {_id}, имя - {_name}, уровень - {_level}, бан - {_isBanned}");
    }
}
