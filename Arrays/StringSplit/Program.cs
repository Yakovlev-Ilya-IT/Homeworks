using System;

namespace StringSplit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = "Привет, я просто строка со словами, которую нужно покрамсать";

            string[] splitMessage = message.Split(' ');

            Console.WriteLine(message);

            foreach (string item in splitMessage)
                Console.WriteLine(item);
        }
    }
}
