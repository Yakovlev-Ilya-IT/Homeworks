using System;
using System.Collections.Generic;
using System.Linq;

namespace Soldiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Soldier> soldiers = new List<Soldier>()
            {
                new Soldier("Павел", 10, Weapons.Пистолет, Ranks.Генерал),
                new Soldier("Максим", 15, Weapons.Пистолет, Ranks.Генерал),
                new Soldier("Глеб", 13, Weapons.Автомат, Ranks.Лейтенант),
                new Soldier("Егор", 11, Weapons.Автомат, Ranks.Лейтенант),
                new Soldier("Иван", 22, Weapons.Автомат, Ranks.Лейтенант),
                new Soldier("Филипп", 24, Weapons.Пулемет, Ranks.Полковник),
                new Soldier("Михаил", 32, Weapons.Пулемет, Ranks.Полковник),
            };

            var soldiersData = soldiers
                .Select(soldier => new { Name = soldier.Name, Rank = soldier.Rank });

            foreach (var data in soldiersData)
                Console.WriteLine($"{data.Name} - {data.Rank}");
        }
    }

    public enum Weapons
    {
        Пистолет = 0,
        Автомат,
        Пулемет
    }

    public enum Ranks
    {
        Генерал = 0,
        Лейтенант,
        Полковник
    }

    public class Soldier
    {
        public Soldier(string name, int serviceLifeInMonth, Weapons weapon, Ranks rank)
        {
            Name = name;
            ServiceLifeInMonth = serviceLifeInMonth;
            Weapon = weapon;
            Rank = rank;
        }

        public string Name { get; }
        public int ServiceLifeInMonth { get; }
        public Weapons Weapon { get; }
        public Ranks Rank { get; }
    }
}
