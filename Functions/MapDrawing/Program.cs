using System;

namespace MapDrawing
{
    internal class Program
    {
        private const ConsoleKey ExitCommand = ConsoleKey.Spacebar;
        private const char Wall = '#';
        private const char Empty = ' ';
        private const char Player = '@';

        static void Main(string[] args)
        {
            char[,] map =
            {
                {Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall,},
                {Wall, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Empty, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Wall, Empty, Wall, Empty, Empty, Wall,},
                {Wall, Empty, Empty, Empty, Empty, Empty, Empty, Empty, Wall, Empty, Empty, Empty, Empty, Wall,},
                {Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall, Wall,},
            };

            int xPosition = 1;
            int yPosition = 2;

            bool isProgrammRunning = true;

            while (isProgrammRunning)
            {
                DrawMap(map);

                DrawPlayer(xPosition, yPosition);

                ConsoleKeyInfo input = Console.ReadKey();

                Move(input, map, ref xPosition, ref yPosition);

                if (input.Key == ExitCommand)
                    isProgrammRunning = false;

                Console.Clear();
            }
        }

        static void DrawPlayer(int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(Player);
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Move(ConsoleKeyInfo input, char[,] map, ref int xPosition, ref int yPosition)
        {
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (map[yPosition - 1, xPosition] != Wall)
                        yPosition--;

                    break;

                case ConsoleKey.DownArrow:
                    if (map[yPosition + 1, xPosition] != Wall)
                        yPosition++;

                    break;

                case ConsoleKey.LeftArrow:
                    if (map[yPosition, xPosition - 1] != Wall)
                        xPosition--;

                    break;

                case ConsoleKey.RightArrow:
                    if (map[yPosition, xPosition + 1] != Wall)
                        xPosition++;

                    break;
            }
        }
    }
}
