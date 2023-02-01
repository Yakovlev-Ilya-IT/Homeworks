namespace PlayerDatabase
{
    public class PlayerCreator
    {
        private uint _currentId = 0;

        public Player Create(string name, int level) => new Player(_currentId++, name, level);
    }
}
