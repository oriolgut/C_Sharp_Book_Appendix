using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPFGuySerializer
{
    [DataContract]
    class Card
    {
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        [DataMember]
        public Suits Suit { get; set; }

        [DataMember]
        public Values Value { get; set; }

        private static Random r = new Random();

        public static Card RandomCard()
        {
            return new Card((Suits)r.Next(4), (Values)r.Next(1, 14));
        }

        public string Name
        {
            get
            {
                return $"{Value} of {Suit}";
            }
        }

        public override string ToString() { return Name; }
    }
}
