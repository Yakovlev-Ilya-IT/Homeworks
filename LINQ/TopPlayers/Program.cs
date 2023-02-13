using System;
using System.Collections.Generic;
using System.Linq;

namespace TopPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>()
            {
                new Player("Nleyn", 20, 50),
                new Player("Vanai", 50, 100),
                new Player("Bozica", 70, 200),
                new Player("Zarady", 10, 155),
                new Player("West", 5, 21),
                new Player("Ughamala", 1, 225),
                new Player("Voraziya", 100, 333),
                new Player("Zelic", 35, 322),
                new Player("Nyakoto", 32, 228),
                new Player("Habelle", 45, 111)
            };

            int showPlayersNumber = 3;

            var topByLevelPlayers = players
                .OrderByDescending(player => player.Level)
                .Take(showPlayersNumber);

            var topByPowerPlayers = players
                .OrderByDescending(player => player.Power)
                .Take(showPlayersNumber);

            Console.WriteLine("Лучшие игроки по уровню");

            foreach (var player in topByLevelPlayers)
                Console.WriteLine(player.Name + " - " + player.Level);

            Console.WriteLine("Лучшие игроки по силе");

            foreach (var player in topByPowerPlayers)
                Console.WriteLine(player.Name + " - " + player.Power);
        }
    }

    public class Player
    {
        public Player(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public string Name { get; }
        public int Level { get; }
        public int Power { get; }
    }
}
