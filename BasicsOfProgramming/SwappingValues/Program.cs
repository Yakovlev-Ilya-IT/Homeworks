using System;

namespace SwappingValues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "Яковлев";
            string surname = "Илья";

            Console.WriteLine("Значения до перестановки:");
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");

            string clipboard = name;
            name = surname;
            surname = clipboard;

            Console.WriteLine("Значения после перестановки:");
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");
        }
    }
}
