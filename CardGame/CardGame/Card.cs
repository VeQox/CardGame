namespace CardGame
{
    public class Card : IComparable<Card>
    {
        public int Value { get; set; }
        public enum CardType { Clover = 0, Spade = 1, Diamond = 2, Heart = 3 }
        public CardType Type { get; set; }

        public Card(CardType type, int value)
        {
            Type = type;
            Value = value;
        }

        public int CompareTo(Card? other)
        {
            if (other == null) return 0;
            if (Type == other.Type)
                return Value > other.Value ? 1 : -1;
            if (Type < other.Type) return -1;
            if (Type > other.Type) return 1;
            return 0;
        }
        public override string ToString() => $"{TypeString()} {ValueString()}";
        public string TypeString() => GetPlaceHolder(); //$" ___ \n| {GetPlaceHolder()} |\n| {Value} |\n ͞͞͞ ";
        public string ValueString() => Value < 10 ? " " + Value.ToString() : Value == 10 ? "10" : Value == 11 ? " J" : Value == 12 ? " D" : Value == 13 ? " K" : " A";
        private string GetPlaceHolder() => Type == CardType.Clover ? "♣" : Type == CardType.Spade ? "♠" : Type == CardType.Diamond ? "♦" : "♥";
        public static bool operator >(Card a, Card b) => a.CompareTo(b) == 1;
        public static bool operator <(Card a, Card b) => a.CompareTo(b) == -1;
    }
}
