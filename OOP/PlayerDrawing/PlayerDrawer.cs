using System;

namespace PlayerDrawing
{
    public class PlayerDrawer
    {
        private char _playerSymbol;

        public PlayerDrawer() => _playerSymbol = '@';
        public PlayerDrawer(char playerSymbol) => _playerSymbol = playerSymbol;

        public void Draw(Player player)
        {
            Console.SetCursorPosition(player.XPosition, player.YPosition);
            Console.WriteLine(_playerSymbol);
        }
    }
}
