using System;
using System.Collections.Generic;

namespace ShopAdministration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Client
    {
        private List<Product> _products;

        private int _money;


    }

    public class Product
    {
        public string Name { get; }
        public int Price { get; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
