using System;
using System.Collections.Generic;

namespace ShopAdministration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product("Капуста", 50),
                new Product("Масло", 150),
                new Product("Зубная паста", 200),
                new Product("Пельмени", 180),
                new Product("Мандарины", 100)
            };

            Shop shop = new Shop(products);

            int startClientNumber = 15;
            int maxClientMoney = 800;
            int minClientMoney = 200;
            Random random = new Random();

            for (int i = 0; i < startClientNumber; i++)
                shop.AddClient(new Client(random.Next(minClientMoney, maxClientMoney), random));

            shop.Work();
        }
    }

    public interface IAvailableProducts
    {
        IReadOnlyList<Product> Products { get; }
    }

    public class Shop: IAvailableProducts
    {
        private Queue<Client> _clients;
        private List<Product> _products;

        private int _money;

        public Shop(List<Product> products)
        {
            _clients = new Queue<Client>();
            _products = new List<Product>(products);
        }

        public IReadOnlyList<Product> Products => _products;

        public void AddClient(Client client)
        {
            client.PutProducts(this);
            _clients.Enqueue(client);
        }

        public void Work()
        {
            while (_clients.Count > 0)
            {
                Client newClient = _clients.Dequeue();
                Console.WriteLine($"Сегодня ваш магазин заработал {_money} руб., осталось {_clients.Count} клиентов");
                Console.WriteLine($"На кассу пришел новый клиент, нажмите любую кнопку что бы пробить его корзину товаров");

                Console.ReadKey(true);

                while (newClient.IsSolvency() == false)
                {
                    Product removedProduct = newClient.RemoveRandomProduct();
                    Console.WriteLine($"У покупателя не хватило денег и он выкинул {removedProduct.Name} ценой {removedProduct.Price} руб. из корзины");
                }

                int paidMoney = newClient.Pay();
                _money += paidMoney;

                Console.WriteLine($"Покупка на сумму {paidMoney} успешно совершена");
                Console.WriteLine("Нажмите любую кнопку, что бы перейти к следующему клиенту");

                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }

    public class Client
    {
        private int _money;
        private int _moneyToPay;

        private Random _random;

        private ProductBasket _productBasket { get; }

        public Client(int money, Random random)
        {
            _money = money;
            _productBasket = new ProductBasket();
            _random = random;
        }

        public void PutProducts(IAvailableProducts products)
        {
            for (int i = 0; i < _productBasket.Capacity; i++)
            {
                int selectedProduct = _random.Next(0, products.Products.Count);
                _productBasket.Add(products.Products[selectedProduct]);
            }
        }

        public Product RemoveRandomProduct() => _productBasket.Remove(_random);

        public bool IsSolvency()
        {
            _moneyToPay = _productBasket.GetCost();

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

    public class ProductBasket
    {
        private List<Product> _products = new List<Product>();

        public int Capacity => 10;

        public void Add(Product product) => _products.Add(product);

        public Product Remove(Random random)
        {
            int productIndex = random.Next(0, _products.Count);
            Product product = _products[productIndex];
            _products.RemoveAt(productIndex);
            return product;
        }

        public int GetCost()
        {
            int cost = 0;

            foreach (Product product in _products)
                cost += product.Price;

            return cost;
        }
    }

    public class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }
    }
}
