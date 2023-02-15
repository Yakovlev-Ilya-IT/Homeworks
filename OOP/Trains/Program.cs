using System;
using System.Collections.Generic;

namespace Trains
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrainLogistic trainLogistic = new TrainLogistic();
            trainLogistic.Work();
        }
    }

    public class TrainLogistic
    {
        private const string CreatePathPlanCommand = "1";
        private const string ExitCommand = "2";

        private List<Departure> _departures = new List<Departure>();

        public void Work()
        {
            Console.WriteLine("Это помошник для создания плана пути поезда, добро пожаловать!");

            bool isWork = true;

            while (isWork)
            {
                ShowDepartures();

                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case CreatePathPlanCommand:
                        CreatePathPlan();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует");
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private void ShowDepartures()
        {
            if(_departures.Count == 0)
            {
                Console.WriteLine("На данный момент отправлений нет");
                return;
            }

            for (int i = 0; i < _departures.Count; i++)
            {
                int departureNumber = i + 1;
                Console.Write($"{departureNumber} - ");
                _departures[i].ShowInformation();
            }
        }

        private void CreatePathPlan()
        {
            Path path = CreatePath();

            int soldTicketsCount = GetSoldTicketsCount();

            Console.WriteLine($"На этот рейс было продано {soldTicketsCount} билетов");

            Console.WriteLine("Нужно создать поезд для этих пассажиров, давайте сконфигурируем вагоны");

            Train train = CreateTrain(soldTicketsCount);

            SendTrain(train, path);
        }

        private void SendTrain(Train train, Path path)
        {
            _departures.Add(new Departure(path, train));
            Console.WriteLine("Поезд успешно отправлен!");
        }

        private Train CreateTrain(int passengers)
        {
            List<Wagon> wagons = new List<Wagon>();

            while (passengers > 0)
            {
                Console.WriteLine($"Осталось разместить {passengers} пасажиров");
                Console.WriteLine($"Выберите размер нового вагона");

                if(int.TryParse(Console.ReadLine(), out int wagonSize))
                {
                    if(wagonSize > 0)
                    {
                        wagons.Add(new Wagon(wagonSize));
                        passengers -= wagonSize;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка размер вагона долже быть положительным числом");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка, введите число");
                }
            }

            Console.WriteLine("Отлично, поезд вмещает всех пассажиров!");
            return new Train(wagons);
        }

        private int GetSoldTicketsCount()
        {
            int maxNumberOfPassengers = 400;
            int minNumberOfPassengers = 50;

            Random random = new Random();
            int soldTicketsCount = random.Next(minNumberOfPassengers, maxNumberOfPassengers);

            return soldTicketsCount;
        }

        private Path CreatePath()
        {
            Console.WriteLine("Введите начальную точку маршрута");

            string startPoint = Console.ReadLine();

            Console.WriteLine("Введите конечную точку маршрута");

            string endPoint = Console.ReadLine();

            return new Path(startPoint, endPoint);
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nВведите одну из следующих команд:");
            Console.WriteLine($"Чтобы создать новый маршрут введите - {CreatePathPlanCommand}");
            Console.WriteLine($"Что бы выйти из программы введите - {ExitCommand}\n");
        }
    }

    public class Departure
    {
        private Path _path;
        private Train _train;

        public Departure(Path path, Train train)
        {
            _path = path;
            _train = train;
        }

        public void ShowInformation()
        {
            _path.ShowInformation();
            _train.ShowInformation();
        }
    }

    public class Path
    {
        private string _startPoint;
        private string _endPoint;

        public Path(string startPoint, string endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public void ShowInformation() => Console.WriteLine($"Начало пути: {_startPoint}, конец пути: {_endPoint}");
    }

    public class Train
    {
        private List<Wagon> _wagons;

        public Train(List<Wagon> wagons) => _wagons = new List<Wagon>(wagons);

        public void ShowInformation()
        {
            Console.Write($"Количество вагонов в поезде: {_wagons.Count}, ");

            int numberOfSeats = 0;

            foreach (var wagon in _wagons)
                numberOfSeats += wagon.Size;

            Console.WriteLine($"количество мест в поезде: {numberOfSeats}");
        }
    }

    public class Wagon
    {

        public Wagon(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }
}
