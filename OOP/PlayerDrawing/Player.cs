namespace PlayerDrawing
{
    public class Player
    {
        private int _xPosition;
        private int _yPosition;

        public int XPosition => _xPosition;
        public int YPosition => _yPosition;

        public Player(int xPosition, int yPosition)
        {
            if(xPosition < 0)
                _xPosition = 0;
            else
                _xPosition = xPosition;

            if (yPosition < 0)
                _yPosition = 0;
            else
                _yPosition = yPosition;
        }
    }
}
