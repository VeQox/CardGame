namespace CardGame
{
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            CardGame game = new(2);
            game.Start();
        }
    }
}