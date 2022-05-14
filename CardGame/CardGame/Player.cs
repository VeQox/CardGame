namespace CardGame
{
    public class Player : IComparable<Player>
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public int Points { get; set; }
        public int Hits { get; set; }
        public int Calls { get; set; }

        public Player(string name = "")
        {
            Name = name;
            Hand = new();
        }

        public Hand GetUsableCards(Card selectedCard)
        {
            Hand usableCards = new();
            usableCards.Cards = GetCardsByType(selectedCard.Type);
            if (usableCards.Cards.Count == 0)
            {
                usableCards.Cards = Hand.Cards;
            }
            return usableCards;

        }

        private List<Card> GetCardsByType(Card.CardType type)
        {
            List<Card> cards = new();
            foreach (Card card in Hand.Cards)
            {
                if (type == card.Type)
                {
                    cards.Add(card);
                }
            }
            return cards;
        }

        public void Print()
        {
            Console.Write($"Name: {Name} -> ");
            Console.Write(Hand);
            Console.WriteLine();
        }

        public int CompareTo(Player? other)
        {
            if (other == null) return 0;
            if (Points < other.Points) return 1;
            if (Points > other.Points) return -1;
            return 0;
        }
    }
}