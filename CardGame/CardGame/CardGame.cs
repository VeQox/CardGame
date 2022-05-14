namespace CardGame
{
    public class CardGame
    {
        List<Player> Players { get; set; }
        List<Card> Stack { get; set; }
        List<Card> UsedCards { get; set; }

        public CardGame(int playerCount)
        {
            Players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(i.ToString()));
            }
            Stack = InitStack();
            UsedCards = new();
        }

        public void Start()
        {
            bool reverse = false;
            int cardCount = 1;
            int currentPlayer = 0;
            int startingPlayer = 0;
            List<Card> hits = new();
            while (cardCount != 0)
            {
                GetNewHands(cardCount);

                Player current = Players[startingPlayer];
                Console.WriteLine($"Player: {current.Name}");
                Console.WriteLine(current.Hand);
                Console.Write("Call: ");
                current.Calls = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                foreach (Player player in Players)
                {
                    if(player != current)
                    {
                        Console.WriteLine($"Player: {player.Name}");
                        Console.WriteLine(player.Hand);
                        Console.Write("Call: ");
                        player.Calls = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                    }
                }

                for (int i = 0; i < cardCount; i++)
                {
                    hits.Clear();
                    Console.WriteLine($"Player: {Players[currentPlayer].Name}");
                    Console.WriteLine(Players[currentPlayer].Hand);
                    Console.Write($"Select 1 Card: ");
                    int cardIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                    Card selectedCard = Players[currentPlayer].Hand.Cards[cardIndex];
                    hits.Add(selectedCard);

                    foreach (Player player in Players)
                    {
                        if (player != Players[currentPlayer])
                        {
                            Console.WriteLine($"Player: {player.Name}");
                            Hand usableCards = player.GetUsableCards(selectedCard);
                            Console.WriteLine(usableCards);
                            Console.Write($"Select 1 Card: ");
                            Card card = usableCards.Cards[Convert.ToInt32(Console.ReadLine()) - 1];
                            hits.Add(card);
                        }
                        Console.WriteLine();
                    }

                    int winnerIndex = GetWinnerOfHit(hits);
                    Players[winnerIndex].Hits++;
                    currentPlayer = winnerIndex;
                }

                Console.Clear();

                foreach (Player player in Players)
                {
                    if (player.Calls != player.Hits)
                        player.Points += player.Calls > player.Hits ? player.Hits - player.Calls : player.Calls - player.Hits;
                    else
                        player.Points += player.Calls + 1;
                    player.Calls = 0;
                    player.Hits = 0;
                    Console.WriteLine($"Player: {player.Name} Points: {player.Points}");
                }
                Console.WriteLine();

                if (reverse) cardCount--;
                if (!reverse) cardCount++;
                if (!reverse && cardCount == 10) reverse = true;

                
                startingPlayer = SwitchPlayer(startingPlayer);
                currentPlayer = startingPlayer;
            }
            Console.Clear();
            foreach (Player player in Players)
            {
                Console.WriteLine($"Player: {player.Name} -> Points: {player.Points}");
            }
            Console.WriteLine();
            Players.Sort();
            Console.WriteLine($"Winner: {Players[0].Name} -> Points: {Players[0].Points}");
        }

        private int GetWinnerOfHit(List<Card> cards)
        {
            int index = -1;
            cards.Sort((a,b) => b.CompareTo(a));

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Hand.Cards.Contains(cards[0]))
                {
                    index = i;
                }
            }
            foreach (Card card in cards)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Players[i].Hand.Cards.Remove(card);
                }
            }
            return index;
        }

        public int SwitchPlayer(int currentPlayer)
        {
            if (currentPlayer < Players.Count - 1)
                return currentPlayer + 1;
            else
                return 0;
        }

        public void Print()
        {
            Console.WriteLine($"Playercount = {Players.Count}");
            Console.WriteLine($"Stack");
            foreach (Card card in Stack)
                Console.Write($"{card}, ");
            Console.WriteLine();
            Console.WriteLine($"UsedCards");
            foreach (Card card in UsedCards)
                Console.Write($"{card}, ");
            Console.WriteLine();
            Console.WriteLine($"PlayerHands:");
            foreach (Player player in Players)
                player.Print();
        }

        private static List<Card> InitStack()
        {
            List<Card> stack = new();
            for (int type = 0; type < 4; type++) // Type 0 to 3
            {
                for (int value = 2; value < 15; value++) // Value 2 to 14 aka A
                {
                    stack.Add(new Card((Card.CardType)type, value));
                }
            }
            return stack;
        }

        public void GetNewHands(int cardCount)
        {
            UsedCards.Clear();
            ClearPlayerHands();
            foreach (Player player in Players)
                player.Hand.Cards = GetNewHand(cardCount);
        }

        private List<Card> GetNewHand(int cardCount)
        {
            List<Card> hand = new();
            for (int i = 0; i < cardCount; i++)
                hand.Add(GetUnusedCard());
            return hand;
        }

        private Card GetUnusedCard()
        {
            Card card;
            Random random = new();
            do
            {
                card = Stack[random.Next(Stack.Count)];
            } while (UsedCards.Contains(card));
            UsedCards.Add(card);
            return card;
        }

        private void ClearPlayerHands()
        {
            foreach (Player player in Players)
                player.Hand.Cards.Clear();
        }
    }
}