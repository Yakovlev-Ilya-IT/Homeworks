using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Prisoner> prisoners = new List<Prisoner>()
            {
                new Prisoner("Козлов Сергей Константинович", Crimes.Antigovernment),
                new Prisoner("Тарасов Игорь Эльдарович", Crimes.Antigovernment),
                new Prisoner("Виноградов Андрей Михаилович", Crimes.Terrorist),
                new Prisoner("Молчанов Аркадий Львович", Crimes.Terrorist),
                new Prisoner("Лихачёв Флор Федорович", Crimes.Antigovernment),
                new Prisoner("Красильников Вилен Георгиевич", Crimes.Other)
            };

            Console.WriteLine("Список до амнистии:");

            foreach (var prisoner in prisoners)
                Console.WriteLine($"{prisoner.FullName}");

            var filteringPrisoners = prisoners
                .Where(prisoner => prisoner.Crime != Crimes.Antigovernment);

            Console.WriteLine("\nСписок после амнистии:");
            foreach (var prisoner in filteringPrisoners)
                Console.WriteLine($"{prisoner.FullName}");
        }
    }

    public enum Crimes
    {
        Antigovernment = 0,
        Terrorist,
        Other
    }

    public class Prisoner
    {
        public Prisoner(string fullName, Crimes crime)
        {
            FullName = fullName;
            Crime = crime;
        }

        public string FullName { get; }
        public Crimes Crime { get; }
    }
}
