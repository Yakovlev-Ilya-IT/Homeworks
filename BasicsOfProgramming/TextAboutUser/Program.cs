using System;

namespace TextAboutUser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет, как тебя зовут?");
            string name = Console.ReadLine();
            Console.WriteLine("Какая твоя любимая игра?");
            string favoriteGame = Console.ReadLine();
            Console.WriteLine("Сколько часов ты в нее наиграл?");
            int amountOfHoursPlayed = int.Parse(Console.ReadLine());

            Console.WriteLine($"Привет {name}, я тоже люблю играть в {favoriteGame} и я рад что ты наиграл {amountOfHoursPlayed} часов в эту замечательную игру!");
        }
    }
}
