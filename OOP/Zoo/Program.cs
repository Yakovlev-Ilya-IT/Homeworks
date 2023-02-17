using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<AnimalTypes, Cage> cages = new Dictionary<AnimalTypes, Cage>();

            Random random = new Random();
            int minAnimalsNumber = 2;
            int maxAnimalsNumber = 15;

            for (int i = 0; i < Enum.GetValues(typeof(AnimalTypes)).Length; i++)
            {
                AnimalTypes animalType = (AnimalTypes)i;
                cages.Add(animalType, new Cage(animalType, random.Next(minAnimalsNumber, maxAnimalsNumber)));
            }

            Zoo zoo = new Zoo(cages);
            zoo.Work();
        }
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
            }

            type = AnimalTypes.Lion;
            return false;
        }

        private void ShowCageSelectionMenu()
        {
            Console.WriteLine("Выберите к каком вольеру подойти (введите соответствуюущую цифру):");
            Console.WriteLine($"Вольер со львами {(int)AnimalTypes.Lion}");
            Console.WriteLine($"Вольер с обезьянами {(int)AnimalTypes.Monkey}");
            Console.WriteLine($"Вольер с медведями {(int)AnimalTypes.Bear}");
            Console.WriteLine($"Вольер с утками {(int)AnimalTypes.Duck}\n");
        }
    }

    public class Cage
    {
        private List<Animal> _animals;

        public Cage(AnimalTypes type, int count)
        {
            _animals = new List<Animal>();
            AnimalFactory factory = new AnimalFactory();

            for (int i = 0; i < count; i++)
                _animals.Add(factory.Get(type, i % 2 == 0));
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Добро пожаловать в наш вольер! Полная информация о животных и их количестве приведена ниже:");

            foreach (var animal in _animals)
                animal.ShowInformation();

            Console.WriteLine($"Количество особей в вольере: {_animals.Count} шт.");
        }
    }

    public class AnimalFactory
    {
        public Animal Get(AnimalTypes type, bool isMan)
        {
            switch (type)
            {
                case AnimalTypes.Lion:
                    return new Lion(isMan);

                case AnimalTypes.Monkey:
                    return new Monkey(isMan);

                case AnimalTypes.Bear:
                    return new Bear(isMan);

                case AnimalTypes.Duck:
                    return new Duck(isMan);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }

    public abstract class Animal
    {
        private bool _isMan;

        public Animal(bool isMan)
        {
            _isMan = isMan;
        }

        protected abstract string Name { get; }
        protected abstract string Sound { get; }

        public void ShowInformation()
        {
            Console.Write($"Животное - {Name}, ");

            if (_isMan)
                Console.Write($"пол: мужской, ");
            else
                Console.Write($"пол: женский, ");

            Console.WriteLine($"Издаваемый звук: {Sound}");
        }
    }

    public class Lion : Animal
    {
        public Lion(bool isMan) : base(isMan)
        {
        }

        protected override string Name => "Лев";

        protected override string Sound => "Арррр";
    }

    public class Monkey : Animal
    {
        public Monkey(bool isMan) : base(isMan)
        {
        }

        protected override string Name => "Обезьяна";

        protected override string Sound => "Ууа-уа-уа-а-у";
    }

    public class Bear : Animal
    {
        public Bear(bool isMan) : base(isMan)
        {
        }

        protected override string Name => "Медведь";

        protected override string Sound => "РРРРррр";
    }

    public class Duck : Animal
    {
        public Duck(bool isMan) : base(isMan)
        {
        }

        protected override string Name => "Утка";

        protected override string Sound => "Кря - кря";
    }

    public enum AnimalTypes
    {
        Lion = 0,
        Monkey,
        Bear,
        Duck
    }
}
