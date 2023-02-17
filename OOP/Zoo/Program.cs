using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal(AnimalTypes.Лев, true, "Арррр"),
                new Animal(AnimalTypes.Обезьяна, false, "Ууа-уа-уа-а-у"),
                new Animal(AnimalTypes.Медведь, true, "РРРРР"),
                new Animal(AnimalTypes.Утка, false, "Кря-кря")
            };

            Dictionary<AnimalTypes, Cage> cages = new Dictionary<AnimalTypes, Cage>();
            Random random = new Random();
            int minAnimalsNumber = 2;
            int maxAnimalsNumber = 15;

            foreach (var animal in animals)
                cages.Add(animal.Type, new Cage(animal, random.Next(minAnimalsNumber, maxAnimalsNumber)));

            Zoo zoo = new Zoo(cages);
            zoo.Work();
        }
    }

    public class Animal
    {
        private bool _isMan;
        private string _sound;

        public Animal(AnimalTypes type, bool isMan, string sound)
        {
            Type = type;
            _isMan = isMan;
            _sound = sound;
        }

        public AnimalTypes Type { get; }

        public void ShowInformation()
        {
            Console.Write($"Животное - {Type}, ");

            if (_isMan)
                Console.Write($"пол: мужской, ");
            else
                Console.Write($"пол: женский, ");

            Console.WriteLine($"Издаваемый звук: {_sound}");
        }
    }

    public enum AnimalTypes
    {
        Лев = 0,
        Обезьяна,
        Медведь,
        Утка
    }

    public class Zoo
    {
        Dictionary<AnimalTypes, Cage> _cages;

        public Zoo(Dictionary<AnimalTypes, Cage> cages) => _cages = new Dictionary<AnimalTypes, Cage>(cages);

        public void Work()
        {
            bool isWorking = true;
            ConsoleKey exitKey = ConsoleKey.Escape;

            while (isWorking)
            {
                ShowCageSelectionMenu();

                if (IsEqualAnimalType(out AnimalTypes type))
                    _cages[type].ShowInformation();
                else
                    Console.WriteLine("Неправильная команда, попробуйте еще");

                Console.WriteLine($"Для выхода из зоопарка нажмите {exitKey}, для продолжения любую другую клавишу");
                ConsoleKeyInfo key = Console.ReadKey();

                if(key.Key == exitKey) 
                    isWorking = false;
                else
                    Console.Clear();
            }
        }

        private bool IsEqualAnimalType(out AnimalTypes type)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (Enum.IsDefined(typeof(AnimalTypes), number))
                {
                    type = (AnimalTypes)number;
                    return true;
                }

                type = AnimalTypes.Лев;
                return false;
            }

            type = AnimalTypes.Лев;
            return false;
        }

        private void ShowCageSelectionMenu()
        {
            Console.WriteLine("Выберите к каком вольеру подойти (введите соответствуюущую цифру):");
            Console.WriteLine($"Вольер со львами {(int)AnimalTypes.Лев}");
            Console.WriteLine($"Вольер с обезьянами {(int)AnimalTypes.Обезьяна}");
            Console.WriteLine($"Вольер с медведями {(int)AnimalTypes.Медведь}");
            Console.WriteLine($"Вольер с утками {(int)AnimalTypes.Утка}\n");
        }
    }



    public class Cage
    {
        private Animal _animal;
        private int _count;

        public Cage(Animal animal, int count)
        {
            _animal = animal;
            _count = count;
        }

        public AnimalTypes Type => _animal.Type;

        public void ShowInformation()
        {
            Console.WriteLine($"Добро пожаловать в наш вольер! Полная информация о животных и их количестве приведена ниже:");
            _animal.ShowInformation();
            Console.WriteLine($"Количество особей в вольере: {_count} шт.");
        }
    }
}
