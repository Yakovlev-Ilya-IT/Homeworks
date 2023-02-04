using System;
using System.Collections.Generic;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public abstract class Item
    {
        public Item(string name, int price)
        {
            if(price < 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }

        public abstract int MaxInStack { get; }

        public void ShowInformation() => Console.WriteLine($"Название: {Name}, цена: {Price} золота");

        public override bool Equals(object other)
        {
            Item item = other as Item;
            if (item == null)
                return false;

            if (ReferenceEquals(this, item))
                return true;

            return Name == item.Name && Price == item.Price;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Price);
    }

    public class Sword : Item
    {
        public Sword(string name, int price) : base(name, price)
        {
        }

        public override int MaxInStack => 1;
    }

    public class Apple : Item
    {
        public Apple(string name, int price) : base(name, price)
        {
        }

        public override int MaxInStack => 64;
    }

    public class Arrow : Item
    {
        public Arrow(string name, int price) : base(name, price)
        {
        }

        public override int MaxInStack => 16;
    }

    public class Cell
    {
        private List<Item> _items = new List<Item>();
        private int _amountToTake;

        public int Amount => _items.Count;
        public bool IsTaken => Amount != 0;

        public bool TryPut(Item item)
        {
            if (IsTaken)
            {
                if (_items[0].Equals(item) && CheckCapacity(item))
                {
                    _items.Add(item);
                    return true;
                }

                return false;
            }

            _items.Add(item);
            return true;
        }

        public bool CheckAmountToTake(int amount)
        {
            if (Amount >= amount)
            {
                _amountToTake = amount;
                return true;
            }

            return false;
        }

        public IEnumerable<Item> Take()
        {
            List<Item> items = _items.GetRange(_items.Count - _amountToTake, _amountToTake);
            _items.RemoveRange(_items.Count - _amountToTake, _amountToTake);
            return items;
        }

        public void ShowInformation()
        {
            if (IsTaken)
            {
                _items[0].ShowInformation();
                Console.WriteLine($"Количество: {Amount} шт.");
                return;
            }

            Console.WriteLine("Ячейка пуста");
        }

        private bool CheckCapacity(Item item) => Amount + 1 <= item.MaxInStack;
    }

    public class Trader
    {
        private const string ShowStorageCommand = "1";
        private const string BuyCommand = "2";
        private const string ExitCommand = "3";

        private List<Cell> _cells;
        private int _money;

        public Trader(List<Cell> cells, int money)
        {
            if(money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            _money = money;
            _cells = new List<Cell>(cells);
        }

        public void Work(IBuyer buyer)
        {
            bool isWorking = true;

            if (isWorking)
            {
                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case ShowStorageCommand:
                        ShowStorage();
                        break;

                    case BuyCommand:
                        Buy(buyer);
                        break;

                    case ExitCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Неопознанная команда, попробуйте еще раз");
                        break;
                }
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Добрый день, странник! Чего желаешь?");
            Console.WriteLine($"{ShowStorageCommand} - показать хранилище товаров");
            Console.WriteLine($"{BuyCommand} - купить понравившийся товар");
            Console.WriteLine($"{ExitCommand} - попрощаться");
            Console.WriteLine();
        }

        public void ShowStorage()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Console.WriteLine($"{i + 1} - ");
                _cells[i].ShowInformation();
            }
        }

        public void Buy(IBuyer buyer)
        {
            Console.WriteLine("Какой товар ты хочешь взять? (введи номер)");

            if(HandleCellNumberInput(out int cellNumber) == false)
            {
                Console.WriteLine("Не удалось обработать выбор ячейки");
                return;
            }

            Console.WriteLine("Какое количество товара ты хочешь взять?");

            if (HandleItemAmountInput(out int itemAmount) == false)
            {
                Console.WriteLine("Не удалось обработать выбор количества товара");
                return;
            }

            if (_cells[cellNumber].CheckAmountToTake(itemAmount))
            {
                IEnumerable<Item> takenItems = _cells[cellNumber].Take();

                //не знаю как лучше сделать проверку денег и вместимости инвентаря покупателя
                if (buyer.CheckSolvency(takenItems) && buyer.CheckInventoryCapacity(takenItems))
                {
                    foreach(Item item in takenItems)
                    {
                        buyer.TryPut(item);
                    }

                    _money += buyer.Pay();

                    Console.WriteLine("Поздравляю с покупкой!");
                    return;
                }

                foreach (Item item in takenItems)
                {
                    _cells[cellNumber].TryPut(item);
                }

                Console.WriteLine("Похоже у вас не хватает денег или места в рюкзаке");
            }

            Console.WriteLine("К сожалению в этой ячейке нет такого количества товара");
        }

        private bool HandleCellNumberInput(out int cellNumber)
        {
            if (int.TryParse(Console.ReadLine(), out cellNumber))
            {
                cellNumber -= 1;

                if (cellNumber >= 0 && cellNumber < _cells.Count)
                    return true;

                Console.WriteLine("Похоже ты выбрал несуществующий номер ячейки, может взглянешь на мое хранилище еще раз?");
                return false;
            }

            Console.WriteLine("Извини, но надо ввести НОМЕР нужной ячейки");
            return false;
        }

        private bool HandleItemAmountInput(out int amount)
        {
            if (int.TryParse(Console.ReadLine(), out amount))
            {
                if(amount > 0)
                    return true;

                Console.WriteLine("Какое-то непонятное количество ты выбрал, попробуй еще");
            }

            Console.WriteLine("Извини, но надо ввести ЧИСЛО");
            return false;
        }
    }

    public class Player : IBuyer
    {
        private List<Cell> _cells;

        private int _money;
        private int _moneyToPay;

        public Player(List<Cell> cells, int money)
        {
            if(money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            _cells = new List<Cell>(cells);
            _money = money;
        }

        public bool TryPut(Item item)
        {
            foreach (Cell cell in _cells)
                if (cell.TryPut(item))
                    return true;

            return false;
        }

        public void ShowInventory()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Console.WriteLine($"{i + 1} - ");
                _cells[i].ShowInformation();
            }
        }

        public bool CheckInventoryCapacity(IEnumerable<Item> items)
        {
            throw new NotImplementedException();
        }

        public bool CheckSolvency(IEnumerable<Item> items)
        {
            _moneyToPay = 0;

            foreach (Item item in items)
                _moneyToPay += item.Price;

            if(_money >= _moneyToPay)
            {
                return true;
            }

            _moneyToPay = 0;
            return false;
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }
    }

    public interface IBuyer
    {
        bool TryPut(Item item);
        bool CheckInventoryCapacity(IEnumerable<Item> items);
        bool CheckSolvency(IEnumerable<Item> items);
        int Pay();
        void ShowInventory();
    }
}
