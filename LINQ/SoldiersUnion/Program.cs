using System;
using System.Collections.Generic;
using System.Linq;

namespace SoldiersUnion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Soldier> firstSoldiersSquad = new List<Soldier>()
            {
                new Soldier("Павел", 10),
                new Soldier("Блинчик", 15),
                new Soldier("Бобр", 13),
            };

            List<Soldier> secondSoldiersSquad = new List<Soldier>()
            {
                new Soldier("Егор", 11),
                new Soldier("Иван", 22),
                new Soldier("Филипп", 24),
                new Soldier("Михаил", 32),
            };

            char symbolForSiltering = 'Б';

            var soldiers = firstSoldiersSquad
                .Where(soldier => soldier.Name.ToUpper().StartsWith(symbolForSiltering));

            secondSoldiersSquad = secondSoldiersSquad
                .Union(soldiers)
                .ToList();

            firstSoldiersSquad = firstSoldiersSquad
                .Except(soldiers)
                .ToList();

            Console.WriteLine("Первая команда");
            foreach (var soldier in firstSoldiersSquad)
                Console.WriteLine(soldier.Name);

            Console.WriteLine("\nВторая команда");
            foreach (var soldier in secondSoldiersSquad)
                Console.WriteLine(soldier.Name);
        }
    }

    public class Soldier
    {
        public Soldier(string name, int serviceLifeInMonth)
        {
            Name = name;
            ServiceLifeInMonth = serviceLifeInMonth;
        }

        public string Name { get; }
        public int ServiceLifeInMonth { get; }
    }
}
