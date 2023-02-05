using System;
using System.Collections.Generic;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Cell> traderCells = new List<Cell>();
            List<Item> items = new List<Item>();

            for (int i = 0; i < 5; i++)
            {
                traderCells.Add(new Cell());
            }

            items.Add(new Sword("Меч Артура", 1000));
            items.Add(new Sword("Деревянный меч", 100));
            items.Add(new Sword("Деревянный меч", 100));
            items.Add(new Apple("Золотое яблоко", 50));
            items.Add(new Arrow("Обычная стрела", 10));
            items.Add(new Arrow("Магическая стрела", 100));

            for (int i = 0; i < traderCells.Count; i++)
            {
                for (int j = 0; j < items[i].MaxInStack; j++)
                {
                    traderCells[i].TryPut(items[i]);
                }
            }

            Trader trader = new Trader(traderCells, 100);

            List<Cell> playerCells = new List<Cell>();

            for (int i = 0; i < 3; i++)
            {
                playerCells.Add(new Cell());
            }

            Player player = new Player(playerCells, 1500);

            trader.Work(player);
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

        public bool CheckPossibleToPut(List<Item> items, out int possiblePutCount)
        {
            int tempCapacity = Amount;
            possiblePutCount = 0;

            foreach (Item item in items)
            {
                if (IsTaken)
                {
                    if (_items[0].Equals(item) == false)
                        return false;
                }
                else
                {
                    if (items[0].Equals(item) == false)
                        return false;
                }

                if (tempCapacity + 1 <= item.MaxInStack)
                {
                    possiblePutCount++;
                    tempCapacity++;
                }
            }

            return true;
        }

        private bool CheckCapacity(Item item) => Amount + 1 <= item.MaxInStack;
    }

    public class Trader
    {
        private const string ShowStorageCommand = "1";
        private const string BuyCommand = "2";
        private const string ShowBuyerInventoryCommand = "3";
        private const string ExitCommand = "4";

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

            while (isWorking)
            {
                ShowBalances(buyer);
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

                    case ShowBuyerInventoryCommand:
                        buyer.ShowInventory();
                        break;

                    default:
                        Console.WriteLine("Неопознанная команда, попробуйте еще раз");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Нажми любую клавишу, что бы продолжить:)");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        public void ShowBalances(IBuyer buyer)
        {
            Console.WriteLine();
            Console.WriteLine($"Деньги продовца: {_money} золота");
            Console.Write("Деньги покупателя: ");
            buyer.ShowBalance();
            Console.WriteLine();
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Добрый день, странник! Чего желаешь?");
            Console.WriteLine($"{ShowStorageCommand} - показать хранилище товаров");
            Console.WriteLine($"{BuyCommand} - купить понравившийся товар");
            Console.WriteLine($"{ShowBuyerInventoryCommand} - посмотреть свой инвентарь");
            Console.WriteLine($"{ExitCommand} - попрощаться");
            Console.WriteLine();
        }

        public void ShowStorage()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Console.Write($"{i + 1} - ");
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

                if (buyer.CheckSolvency(takenItems))
                {
                    if (buyer.TryPut(takenItems))
                    {
                        _money += buyer.Pay();

                        Console.WriteLine("Поздравляю с покупкой!");
                        return;
                    }
                    else
                    {
                        foreach (Item item in takenItems)
                        {
                            _cells[cellNumber].TryPut(item);
                        }

                        Console.WriteLine("Похоже у вас не хватает места в рюкзаке");
                        return;
                    }
                }

                //foreach (Item item in takenItems)
                //{
                //    _cells[cellNumber].TryPut(item);
                //}

                Console.WriteLine("Похоже у вас не хватает денег");
                return;
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

        public bool TryPut(IEnumerable<Item> items)
        {
            List<Item> checkedItems = new List<Item>(items);

            if (CheckInventoryCapacity(checkedItems))
            {
                foreach (Cell cell in _cells)
                {
                    for (int j = checkedItems.Count - 1; j >= 0; j--)
                    {
                        if (cell.TryPut(checkedItems[j]))
                        {
                            checkedItems.RemoveAt(j);

                            if (checkedItems.Count == 0)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public void ShowInventory()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _cells[i].ShowInformation();
            }
        }

        public void ShowBalance() => Console.WriteLine($"{_money} золота");

        private bool CheckInventoryCapacity(List<Item> items)
        {
            int numberOfPlacedItems = 0;

            foreach (Cell cell in _cells)
            {
                if (cell.CheckPossibleToPut(items, out int possiblePutCount))
                {
                    numberOfPlacedItems += possiblePutCount;

                    if(numberOfPlacedItems == items.Count)
                        return true;
                }
            }

            return false;
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
        bool TryPut(IEnumerable<Item> items);
        bool CheckSolvency(IEnumerable<Item> items);
        void ShowBalance();
        int Pay();
        void ShowInventory();
    }
}
