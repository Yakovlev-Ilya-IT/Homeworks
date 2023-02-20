using System;
using System.Collections.Generic;

namespace CarService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            DetailsDatabase detailsDatabase = new DetailsDatabase();

            DetailsStorage detailsStorage = new DetailsStorage(detailsDatabase, random);

            CarService carService = new CarService(detailsStorage, 1200, random);

            int clientsNumber = 15;
            int maxMoney = 6000;
            int minMoney = 100;

            for (int i = 0; i < clientsNumber; i++)
            {
                int detailIndex = random.Next(0, detailsDatabase.Details.Count);
                Car car = new Car(detailsDatabase.Details[detailIndex]);
                carService.AddClient(new Client(car, random.Next(minMoney, maxMoney)));
            }

            carService.Work();
        }
    }

    public class DetailsDatabase
    {
        public DetailsDatabase()
        {
            Details = new List<Detail>()
            {
                new Detail("Лобовое стекло", 1000),
                new Detail("Магнитола", 800),
                new Detail("Двигатель", 4000),
                new Detail("Фара", 700),
                new Detail("Колесо", 1200),
                new Detail("Заднее стекло", 1000),
                new Detail("Бампер", 500)
            };
        }

        public IReadOnlyList<Detail> Details { get; }
    }

    public class PaymentReceipt
    {
        private Detail _detail;
        private int _installingCost;

        public PaymentReceipt(Detail detail, int installingCost)
        {
            _detail = detail;
            _installingCost = installingCost;
        }

        public int Cost => _detail.Cost + _installingCost;
    }

    public class Detail
    {
        public Detail(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }

        public override bool Equals(object other)
        {
            Detail detail = other as Detail;

            if (detail == null)
                return false;

            if (ReferenceEquals(this, detail))
                return true;

            return Name == detail.Name && Cost == detail.Cost;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Cost);
    }

    public class DetailsStorage
    {
        private Dictionary<Detail, int> _details;

        public DetailsStorage(DetailsDatabase detailsDatabase, Random random)
        {
            int maxDetailsNumber = 5;
            int minDetailsNumber = 1;

            _details = new Dictionary<Detail, int>();

            foreach (Detail detail in detailsDatabase.Details)
                _details.Add(detail, random.Next(minDetailsNumber, maxDetailsNumber));
        }

        public IReadOnlyDictionary<Detail, int> Details => _details;

        public bool TryGet(Detail replacementDetail, out Detail newDetail)
        {
            if (_details.ContainsKey(replacementDetail))
            {
                if(_details[replacementDetail] > 0)
                {
                    _details[replacementDetail]--;
                    newDetail = new Detail(replacementDetail.Name, replacementDetail.Cost);
                    return true;
                }

                newDetail = null;
                return false;
            }

            newDetail = null;
            return false;
        }

        public bool TryPut(Detail detail)
        {
            if (_details.ContainsKey(detail))
            {
                _details[detail]++;
                return true;
            }

            return false;
        }

        public void Show()
        {
            Console.WriteLine("Состояние склада:");
            foreach (var item in _details)
                Console.WriteLine($"{item.Key.Name} - {item.Value} шт.");
        }
    }

    public class CarService
    {
        private DetailsStorage _detailStorage;
        private Dictionary<Detail, int> _costsOfDetailsInstalling;
        private int _money;

        private Queue<Client> _clients;

        private bool _isBankrupt;

        public CarService(DetailsStorage detailStorage, int money, Random random)
        {
            _detailStorage = detailStorage;
            _money = money;

            DetermineCostsOfDetailsInstalling(random);

            _clients = new Queue<Client>();
        }

        public void AddClient(Client client) => _clients.Enqueue(client);

        public void Work()
        {
            int penaltyForUnrealizedOrder = 500;

            while (_clients.Count > 0 && _isBankrupt == false)
            {
                Console.WriteLine($"Баланс вашего сервиса: {_money} руб., осталось {_clients.Count} клиентов");
                _detailStorage.Show();
                Client newClient = _clients.Dequeue();
                Console.WriteLine($"В сервис приехал новый клиент, нажмите любую кнопку что бы посмотреть его авто");

                Console.ReadKey(true);

                Car clientCar = newClient.Car;
                Detail replacementDeatial = clientCar.ReplacementDetail;

                if(TryCalculateCostOfDetailInstalling(replacementDeatial, out PaymentReceipt receipt))
                {
                    Console.WriteLine($"Требуется замена детали: {replacementDeatial.Name}, стоимость замены с учетом работы: {receipt.Cost}");

                    if (newClient.IsSolvency(receipt))
                    {
                        if (_detailStorage.TryGet(replacementDeatial, out Detail newDetail))
                        {
                            if (clientCar.TryReplaceDetail(newDetail))
                            {
                                _money += newClient.Pay();
                                Console.WriteLine("Деталь успешно заменена, поздравляю!");
                            }
                            else
                            {
                                Console.WriteLine("Вы попытались установить на машину несоответствующую поломке деталь, атата");

                                if (_detailStorage.TryPut(newDetail))
                                    HandlePenaltyPayment(penaltyForUnrealizedOrder);
                                else
                                    throw new InvalidOperationException(nameof(newDetail));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такой детали нет в наличии, приносим свои извенения");
                            HandlePenaltyPayment(penaltyForUnrealizedOrder);
                        }
                    }
                    else
                    {
                        Console.WriteLine("У клиента не хватило денег и он ушел из сервиса");
                    }
                }
                else
                {
                    Console.WriteLine("Замена такой детали не поддерживается нашим сервисом, приносим извенения");
                    HandlePenaltyPayment(penaltyForUnrealizedOrder);
                }

                Console.WriteLine("Для продолжения нажмите любуню кнопку");
                Console.ReadKey(true);
                Console.Clear();
            }

            if (_isBankrupt)
                Console.WriteLine("Вы стали банкротом(");
            else
                Console.WriteLine("Поздравляю с успешным окончанием рабочего дня");
        }

        private void HandlePenaltyPayment(int penalty)
        {
            if (TryPayPenalty(penalty))
            {
                Console.WriteLine("Штраф успешно вылачен, не переживайте у вас еще будет возможность заработать");
            }
            else
            {
                Console.WriteLine("У вас недостаточно денег для вылпаты штрафа, поэтому мы изъяли остатки ваших денег, теперь вы банкрот");
                _isBankrupt = true;
            }
        }

        private bool TryPayPenalty(int penalty)
        {
            Console.WriteLine($"За невыполненный заказ с вашей мастерской взымается штраф в размере {penalty} руб.");

            if (_money < penalty)
            {
                _money = 0;
                return false;
            }

            _money -= penalty;
            return true;
        }

        private bool TryCalculateCostOfDetailInstalling(Detail detail, out PaymentReceipt receipt)
        {
            if(_costsOfDetailsInstalling.ContainsKey(detail) == false)
            {
                receipt = null;
                return false;
            }

            receipt = new PaymentReceipt(detail, _costsOfDetailsInstalling[detail]);
            return true;
        }

        private void DetermineCostsOfDetailsInstalling(Random random)
        {
            int maxCost = 1500;
            int minCost = 500;

            _costsOfDetailsInstalling = new Dictionary<Detail, int>();
            foreach (var item in _detailStorage.Details)
                _costsOfDetailsInstalling.Add(item.Key, random.Next(minCost, maxCost));
        }
    }

    public class Car
    {
        public Car(Detail replacementDetail) => ReplacementDetail = replacementDetail; 

        public Detail ReplacementDetail { get; private set; }

        public bool TryReplaceDetail(Detail detail)
        {
            if (ReplacementDetail.Equals(detail))
            {
                ReplacementDetail = detail;
                return true;
            }

            return false;
        }
    }

    public class Client
    {
        private int _money;
        private int _moneyToPay;

        public Client(Car car, int money)
        {
            Car = car;
            _money = money;
        }

        public Car Car { get; }

        public bool IsSolvency(PaymentReceipt receipt)
        {
            _moneyToPay = receipt.Cost;

            if (_money >= _moneyToPay)
                return true;

            _moneyToPay = 0;
            return false;
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }
    }
}
