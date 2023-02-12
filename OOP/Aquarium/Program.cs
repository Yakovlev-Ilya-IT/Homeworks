using System;
using System.Collections.Generic;

namespace Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    enum AquariumCommands
    {
        Add = 1,
        Remove = 2,
        Exit = 3
    }

    public class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium()
        {
            _fishes = new List<Fish>()
            {
                new Fish("Альберт", 10),
                new Fish("Роберт", 5),
                new Fish("Алекс", 15)
            };
        }

        public void Work()
        {
            bool isWorking = true;

            while (isWorking)
            {
                ShowFishes();
                ShowMenu();

                if(IsEqualAquariumCommand(out AquariumCommands command))
                {
                    switch (command)
                    {
                        case AquariumCommands.Add:
                            AddFish();
                            break;

                        case AquariumCommands.Remove:
                            RemoveFish();
                            break;

                        case AquariumCommands.Exit:
                            isWorking = false;
                            break;

                        default:
                            throw new InvalidOperationException(nameof(command));
                    }
                }
                else
                {
                    Console.WriteLine("Неправильная команда, попробуйте еще");
                }

                Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
                Console.ReadKey(true);
                IncreaseFishesAge();
                Console.Clear();
            }
        }

        private void IncreaseFishesAge()
        {
            foreach (Fish fish in _fishes)
                fish.IncreaseAge();
        }

        private void AddFish()
        {
            Console.WriteLine("Дайте новой рыбке имя");

            string name = Console.ReadLine();

            Console.WriteLine("Задайте ее максимальный возраст");

            int age = GetFishAge();

            _fishes.Add(new Fish(name, age));

            Console.WriteLine("Рыбка успешно добавлена");
        }

        private void RemoveFish()
        {
            Console.WriteLine("Выберите рыбку для удаления по индексу");

            if (int.TryParse(Console.ReadLine(), out int fishNumber))
            {
                fishNumber -= 1;

                if (fishNumber < 0 || fishNumber >= _fishes.Count)
                {
                    Console.WriteLine("Введенный номер рыбки за пределами списка");
                    return;
                }

                _fishes.RemoveAt(fishNumber);
                Console.WriteLine("Рыбка успешно удалена");
                return;
            }

            Console.WriteLine("Введите число");
        }

        private bool IsEqualAquariumCommand(out AquariumCommands command)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (Enum.IsDefined(typeof(AquariumCommands), number))
                {
                    command = (AquariumCommands)number;
                    return true;
                }

                command = AquariumCommands.Add;
                return false;
            }

            command = AquariumCommands.Add;
            return false;
        }

        private int GetFishAge()
        {
            uint age;

            while(uint.TryParse(Console.ReadLine(), out age) == false)
                Console.WriteLine("Введите неотрицатльное число");

            return (int)age;
        }

        private void ShowFishes()
        {
            Console.WriteLine("Текущее состояние аквариума: ");

            if(_fishes.Count == 0)
            {
                Console.WriteLine("Аквариум пустой");
                return;
            }

            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _fishes[i].ShowInfo();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nВведите одну из команд:");
            Console.WriteLine($"Для добавления новой рыбки {(int)AquariumCommands.Add}");
            Console.WriteLine($"Для удаления рыбки по индексу {(int)AquariumCommands.Remove}");
            Console.WriteLine($"Для прекращения работы с аквариумом {(int)AquariumCommands.Exit}\n");
        }
    }

    public class Fish
    {
        private string _name;
        private int _maxAge;

        private int _currentAge;

        public Fish(string name, int maxAge)
        {
            _name = name;
            _maxAge = maxAge;
        }

        private bool IsDead => _currentAge >= _maxAge;

        public void IncreaseAge()
        {
            if(IsDead)
                return;

            _currentAge++;
        }

        public void ShowInfo()
        {
            Console.Write($"Имя - {_name}, возраст: {_currentAge}, максимальный возраст: {_maxAge}, состояние здоровья: ");

            if(IsDead)
                Console.WriteLine("мертвая");
            else
                Console.WriteLine("живая");
        }
    }
}
