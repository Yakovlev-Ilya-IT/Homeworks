using System;

namespace PlayerDrawing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int xPosition = 3;
            int yPosition = 3;
            char playerSymbol = '%';

            Player player = new Player(xPosition, yPosition);

            PlayerDrawer drawer = new PlayerDrawer(playerSymbol);
            drawer.Draw(player);
        }
    }
}
