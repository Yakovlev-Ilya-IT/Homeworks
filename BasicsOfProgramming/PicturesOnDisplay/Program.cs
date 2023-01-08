using System;

namespace PicturesOnDisplay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxPicturesInRow = 3;
            int totalPictures = 52;

            int numberOfFilledRows = totalPictures / maxPicturesInRow;
            int numberOfExtraImages = totalPictures % maxPicturesInRow;

            Console.WriteLine($"Количество заполненных рядов: {numberOfFilledRows}");
            Console.WriteLine($"Количество лишних картинок: {numberOfExtraImages}");
        }
    }
}
