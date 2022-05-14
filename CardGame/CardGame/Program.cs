namespace CardGame
{
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            CardGame game = new(3);
            game.Start();
        }
    }
}