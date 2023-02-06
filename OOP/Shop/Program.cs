using System;
using System.Collections.Generic;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            items.Add(new Sword("Меч Артура", 1000));
            items.Add(new Sword("Деревянный меч", 100));
            items.Add(new Sword("Деревянный меч", 100));
            items.Add(new Apple("Золотое яблоко", 50));
            items.Add(new Arrow("Обычная стрела", 10));
            items.Add(new Arrow("Магическая стрела", 100));

            List<Cell> traderCells = new List<Cell>();

            for (int i = 0; i < items.Count; i++)
            {
                traderCells.Add(new Cell(items[i], items[i].MaxInStack));
            }

            Trader trader = new Trader(traderCells, 100);

            List<Cell> playerCells = new List<Cell>();

            for (int i = 0; i < 4; i++)
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
        private Item _item;

        private int _amount;
        private int _amountToTake;

        public Cell() { }

        public Cell(Item item, int amount)
        {
            _item =  item;
            _amount = amount;
        }

        public bool IsEmpty => _item == null;
        private bool IsNotFull => _amount + 1 <= _item?.MaxInStack;

        public bool CheckAmountToTake(int amount)
        {
            if (_amount >= amount)
            {
                _amountToTake = amount;
                return true;
            }

            _amountToTake = 0;
            return false;
        }

        public Item Take(out int amount)
        {
            _amount -= _amountToTake;

            Item takenItem = _item;
            amount = _amountToTake;

            if (_amount <= 0)
                _item = null;

            return takenItem;
        }

        public void ShowInformation()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Ячейка пуста");
                return;
            }

            _item.ShowInformation();
            Console.WriteLine($"Количество: {_amount} шт.");
        }

        public bool TryPut(Item item)
        {
            if (IsEmpty)
            {
                _item = item;
                _amount = 1;
                return true;
            }

            if(CheckTypeMatch(item) == false)
                return false;

            if (IsNotFull)
            {
                _amount++;
                return true;
            }

            return false;
        }

        public bool CheckPossibleToPut(Item item, int amount, out int possiblePutCount)
        {
            if(IsEmpty == false)
            {
                if (CheckTypeMatch(item) == false)
                {
                    possiblePutCount = 0;
                    return false;
                }
            }   

            int remainingPlace = item.MaxInStack - amount - _amount;

            if (remainingPlace >= 0)
            {
                possiblePutCount = amount;
                return true;
            }
            else
            {
                possiblePutCount = amount - Math.Abs(remainingPlace);

                if(possiblePutCount <= 0)
                    return false;

                return true;
            }
        }

        private bool CheckTypeMatch(Item item) => _item.Equals(item);
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
                Item takenItem = _cells[cellNumber].Take(out int amount);

                if (buyer.CheckSolvency(takenItem, amount))
                {
                    if (buyer.TryPut(takenItem, amount))
                    {
                        _money += buyer.Pay();

                        Console.WriteLine("Поздравляю с покупкой!");
                        return;
                    }

                    ReturnItemsToCell(_cells[cellNumber], takenItem, amount);

                    Console.WriteLine("Похоже у вас не хватает места в рюкзаке");
                    return;
                }

                ReturnItemsToCell(_cells[cellNumber], takenItem, amount);

                Console.WriteLine("Похоже у вас не хватает денег");
                return;
            }

            Console.WriteLine("К сожалению в этой ячейке нет такого количества товара");
        }

        private void ReturnItemsToCell(Cell cell, Item item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if(cell.TryPut(item) == false)
                    throw new InvalidOperationException();
            }
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

        public bool TryPut(Item item, int amount)
        {
            if (CheckInventoryCapacity(item, amount) == false)
                return false;

            foreach (Cell cell in _cells)
            {
                for (int i = amount; i >= 0; i--)
                {
                    if (cell.TryPut(item))
                    {
                        amount--;

                        if (amount == 0)
                            return true;
                    }
                }
            }

            throw new InvalidOperationException();
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

        public bool CheckSolvency(Item item, int amount)
        {
            _moneyToPay = item.Price * amount;

            if (_money >= _moneyToPay)
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

        private bool CheckInventoryCapacity(Item item, int amount)
        {
            foreach (Cell cell in _cells)
            {
                if (cell.CheckPossibleToPut(item, amount, out int possiblePutCount))
                {
                    amount -= possiblePutCount;

                    if (amount == 0)
                        return true;
                }
            }

            return false;
        }
    }

    public interface IBuyer
    {
        bool TryPut(Item item, int amount);
        bool CheckSolvency(Item item, int amount);
        int Pay();
        void ShowBalance();
        void ShowInventory();
    }
}
