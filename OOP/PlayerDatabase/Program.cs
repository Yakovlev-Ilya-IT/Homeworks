using System;
using System.Collections.Generic;

namespace PlayerDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerCreator creator = new PlayerCreator();

            List<Player> players = new List<Player>();
            players.Add(creator.Create("Wizard", 5));
            players.Add(creator.Create("Hopfild", 10));
            players.Add(creator.Create("Bro", 2));

            PlayerDatabase database = new PlayerDatabase(players);

            Console.WriteLine("Изначальное состояние базы данных:");
            database.ShowInformation();

            Console.WriteLine("\nДобавили одного игрока и забанили двоих");
            database.Add(creator.Create("lololo", 100));
            database.Ban(1);
            database.Ban(2);
            database.ShowInformation();

            Console.WriteLine("\nУдалили одного игрока и разбанили одного");
            database.Remove(0);
            database.UnBan(1);
            database.ShowInformation();
        }
    }
}
