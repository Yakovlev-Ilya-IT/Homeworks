using System;
using System.Collections.Generic;
using System.Linq;

namespace FilteringCriminals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CriminalDatabase criminalDatabase = new CriminalDatabase();
            criminalDatabase.Work();
        }
    }

    public class CriminalDatabase
    {
        private List<Criminal> _criminals = new List<Criminal>()
            {
                new Criminal("Воробьёв Федор Миронович", false, 170, 65, "Русский"),
                new Criminal("Давыдов Любомир Альбертович", true, 180, 75, "Русский"),
                new Criminal("Казаков Глеб Валентинович", true, 160, 60, "Армянин"),
                new Criminal("Шубин Фрол Борисович", false, 170, 65, "Русский"),
                new Criminal("Силин Бенедикт Леонидович", false, 180, 65, "Японец")
            };

        public void Work()
        {
            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("\nВведите рост");
                int height = GetInputNumber();

                Console.WriteLine("\nВведите вес");
                int weight = GetInputNumber();

                Console.WriteLine("\nВведите национальность");
                string nationality = Console.ReadLine();

                var filteringCriminals = _criminals
                    .Where(criminal => criminal.Height == height)
                    .Where(criminal => criminal.Weight == weight)
                    .Where(criminal => criminal.Nationality.ToUpper() == nationality.ToUpper())
                    .Where(criminal => criminal.IsIntoCustody == false);

                if(filteringCriminals.Count() == 0)
                {
                    Console.WriteLine("\nТаких преступников не найдено");
                }
                else
                {
                    Console.WriteLine("\nСписок преступников по указанным параметрам");
                    foreach (var criminal in filteringCriminals)
                        Console.WriteLine(criminal.FullName);
                }
            }
        }

        private int GetInputNumber()
        {
            uint number = 0;

            while(uint.TryParse(Console.ReadLine(), out number) == false)
                Console.WriteLine("Введите неотрицательное число");
           
            return (int)number;
        }
    }

    public class Criminal
    {
        public Criminal(string fullName, bool isIntoCustody, int height, int weight, string nationality)
        {
            FullName = fullName;
            IsIntoCustody = isIntoCustody;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string FullName { get; }
        public bool IsIntoCustody { get; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationality { get; }
    }
}
