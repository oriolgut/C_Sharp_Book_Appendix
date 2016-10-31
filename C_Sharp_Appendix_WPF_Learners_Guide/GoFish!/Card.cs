
namespace GoFishGame
{
    class Card
    {
        public string Name
        {
            get
            {
                return $"{Value} of {Suit}";
            }
        }

        public Suits Suit { get; set; }
        public Values Value { get; set; }

        public Card (Values value, Suits suit)
        {
            Value = value;
            Suit = suit;
        }

        public static string Plural(Values value)
        {
            if (value == Values.Six)
            {
                return "Sixes";
            }

            return $"{value}s";
        }
    }
}
