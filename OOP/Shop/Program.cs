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
        public string Name { get; }
        public int Price { get; }
        
        public abstract int MaxStack { get; }    

        public Item(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public override bool Equals(object other)
        {
            Item item = other as Item;
            if (item == null)
                return false;

            if (ReferenceEquals(this, item))
                return true;

            return Name.ToUpper() == item.Name.ToUpper() && Price == item.Price;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Price);
    }

    public class Sword : Item
    {
        public override int MaxStack => 1;

        public Sword(string name, int price) : base(name, price)
        {
        }
    }

    public class Apple : Item
    {
        public override int MaxStack => 64;

        public Apple(string name, int price) : base(name, price)
        {
        }
    }

    public class Arrow : Item
    {
        public override int MaxStack => 16;

        public Arrow(string name, int price) : base(name, price)
        {
        }
    }

    public class Cell
    {
        private Item _item;
        private int _amount; 

        public bool TryAdd(Item item, int amount)
        {
            if(CheckAddToStackPossibility(item, amount) == false)
                return false;

            if(_item == null)
            {
                _item = item;
                _amount = amount;

                return true;
            }

            if (_item.Equals(item))
            {
                _amount += amount;

                return true;
            }

            return false;
        }

        private bool CheckAddToStackPossibility(Item item, int amount) => _amount + amount < item.MaxStack;
    }

    public class Trader
    {
        private List<Item> _items; 

        public Trader(List<Item> items)
        {
            _items = new List<Item>(items);
        }

        public 
    }
}
