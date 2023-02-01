using System;
using System.Collections.Generic;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> purchaseAmounts = new Queue<int>(new int[] { 10, 20, 5, 30, 45, 15 });

            int wallet = 0;

            while(purchaseAmounts.Count > 0)
            {
                int currentPurchaseAmount = purchaseAmounts.Dequeue();

                Console.WriteLine($"Вы обслужили покупателя на сумму {currentPurchaseAmount}");

                wallet += currentPurchaseAmount;

                Console.WriteLine($"Текущий счет кошелька: {wallet}");

                Console.ReadKey();

                Console.Clear();
            }

            Console.WriteLine("Вы обслужили всех покупателей, поздравляю!");
        }
    }
}
