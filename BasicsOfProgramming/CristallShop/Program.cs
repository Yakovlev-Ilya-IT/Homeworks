using System;

namespace CristallShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint crystalCost = 10;

            Console.WriteLine($"Привет, один кристалл в нашей лавке стоит {crystalCost} золота");
            Console.WriteLine("Сколько у тебя сейчас золота?");

            uint currentGold = uint.Parse(Console.ReadLine());

            Console.WriteLine("Отлично, сколько кристаллов ты хочешь купить?");

            uint numberOfCrystalsToBuy = uint.Parse(Console.ReadLine());

            currentGold -= numberOfCrystalsToBuy * crystalCost;

            Console.WriteLine($"Теперь у тебя {numberOfCrystalsToBuy} кристаллов и {currentGold} золота");
        }
    }
}
