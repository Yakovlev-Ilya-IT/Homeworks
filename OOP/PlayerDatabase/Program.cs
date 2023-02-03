namespace PlayerDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerCreator creator = new PlayerCreator();
            PlayerDatabase database = new PlayerDatabase();

            database.Work(creator);
        }
    }
}
