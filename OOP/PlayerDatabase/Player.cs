using System;

namespace PlayerDatabase
{
    public class Player
    {
        public uint Id { get; }
        public string Name { get; }
        public int Level { get; }
        public bool IsBanned { get; private set; }

        public Player(uint id, string name, int level)
        {
            Id = id;
            Name = name;

            if(level < 0)
                Level = 0;
            else
                Level = level;

            IsBanned = false;
        }

        public void Ban() => IsBanned = true;

        public void UnBan() => IsBanned = false;

        public void ShowInformation() => Console.WriteLine($"id - {Id}, имя - {Name}, уровень - {Level}, бан - {IsBanned}");
    }
}
