using System;
using System.Collections.Generic;
using System.Linq;

namespace CannedMeat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CannedMeat> cannedMeats = new List<CannedMeat>()
            {
                new CannedMeat("Коровка", 2020, 5),
                new CannedMeat("Коровка", 2023, 5),
                new CannedMeat("Коровка", 2019, 5),
                new CannedMeat("Теленок", 2020, 7),
                new CannedMeat("Теленок", 2019, 7),
                new CannedMeat("Барашка", 2023, 9),
                new CannedMeat("Барашка", 2020, 9)
            };

            int currentYear = 2028;

            var expiredCannedMeats = cannedMeats
                .Where(cannedMeat => cannedMeat.ProdactionYear + cannedMeat.ExpirationDate < currentYear);

            Console.WriteLine("Просрочка:");
            foreach (var cannedMeat in expiredCannedMeats)
                Console.WriteLine($"{cannedMeat.Name}, год производства {cannedMeat.ProdactionYear}");
        }
    }

    public class CannedMeat
    {
        public CannedMeat(string name, int prodactionYear, int expirationDate)
        {
            Name = name;
            ProdactionYear = prodactionYear;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }
        public int ProdactionYear { get; }
        public int ExpirationDate { get; }
    }
}
