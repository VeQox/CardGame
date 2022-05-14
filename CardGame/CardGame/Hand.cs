namespace CardGame
{
    public class Hand
    {
        public List<Card> Cards { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }

        public override string ToString()
        {
            string returnString = "|";
            foreach (Card card in Cards)
            {
                returnString += $" {card} |";
            }
            return returnString;
        }
    }
}
