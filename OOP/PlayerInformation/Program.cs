namespace PlayerInformation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Атрей", 1000, 4, 100);

            player.ShowInformation();
        }
    }
}
