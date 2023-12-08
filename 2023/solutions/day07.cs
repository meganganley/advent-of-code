using System.Drawing;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace solutions
{
    public class day07
    {
        public static void Run()
        {
            List<string> lines = Helper.get_input("day07_input.txt");
            //List<string> lines = Helper.get_input("day07_sample_input.txt");
            
            List<Hand> hands = new List<Hand>();

            foreach (string line in lines)
            {
                int[] hand = CreateHandArray(line.Split(" ")[0]);
                int bid = int.Parse(line.Split(" ")[1]);
                Hand h = new Hand(hand, bid);

                Console.WriteLine("Hand: ");

                foreach (int i in hand)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine("\nScore: " + h.GetScore());

                hands.Add(h);
            }

            hands.Sort();

            int totalWinnings = 0;
            int rank = 1;

            for ( int i = 0; i < hands.Count; i++)
            {
                totalWinnings += rank * hands[i].bid;
                rank++;
            }

            Console.WriteLine(totalWinnings);    // Part 1 - 251029473
            Console.WriteLine(0);    // Part 2 - 36919753

        }

        public static int[] CreateHandArray(string hand)
        {
            int[] handArray = new int[hand.Length];
            for (int i = 0; i < handArray.Length; i++)
            {
                if (char.IsNumber(hand[i]))
                {
                    handArray[i] = int.Parse(hand[i].ToString());
                }
                else if (hand[i] == 'T')
                {
                    handArray[i] = 10;
                }
                else if (hand[i] == 'J')
                {
                    handArray[i] = 11;
                }
                else if (hand[i] == 'Q')
                {
                    handArray[i] = 12;
                }
                else if (hand[i] == 'K')
                {
                    handArray[i] = 13;
                }
                else if (hand[i] == 'A')
                {
                    handArray[i] = 14;
                }
            }
            return handArray;
        }

        public static int[] GetHigherHand(Dictionary<int[], int> handScores, int[] hand1, int[] hand2)
        {
            if (handScores[hand1] > handScores[hand2])
            {
                return hand1;
            }
            if (handScores[hand1] < handScores[hand2])
            {
                return hand2;
            }
            for (int i = 0; i < hand1.Length; i++)
            {
                if (hand1[i] > hand2[i])
                {
                    return hand1;
                }

                if (hand1[i] < hand2[i])
                {
                    return hand2;
                }
            }
            return hand1;
        }
    }
        
    public class Hand : IComparable<Hand>
    {
        int[] hand;
        public int bid;

        public Hand(int[] hand, int bid)
        {
            this.hand = hand;
            this.bid = bid;
        }

        public int GetScore()
        {
            Dictionary<int, int> countChars = new Dictionary<int, int>();
            foreach (int card in hand)
            {
                if (!countChars.ContainsKey(card))
                {
                    countChars.Add(card, 1);
                }
                else
                {
                    countChars[card] += 1;
                }
            }
            var sortedDict = countChars.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            bool checkFullHouse = false;
            bool checkTwoPair = false;

            foreach (KeyValuePair<int, int> pair in sortedDict)
            {
                if (pair.Value == 5)
                {
                    return 7; // Five of a kind
                }
                else if (pair.Value == 4)
                {
                    return 6; // Four of a kind

                }
                else if (pair.Value == 3)
                {
                    checkFullHouse = true;

                }
                else if (pair.Value == 2 && checkFullHouse)
                {
                    return 5;  // Full house

                }
                else if (pair.Value == 2 && !checkFullHouse && !checkTwoPair)
                {
                    checkTwoPair = true;

                }
                else if (pair.Value == 2 && checkTwoPair)
                {
                    return 3;  // Two Pair

                }
                else if (pair.Value == 2 && !checkFullHouse && !checkTwoPair)
                {
                    return 2;  // Pair

                }
            }
            if (checkFullHouse)
            {
                return 4; // Three of a kind
            }

            if (checkTwoPair)
            {
                return 2; // Pair
            }

            return 1; // High card
        }
        public int CompareTo(Hand? obj)
        {
            if (this.GetType() != obj.GetType())
            {
                throw (new ArgumentException(
                       "Both objects being compared must be of type Square."));
            }
            else
            {
                Hand hand2 = (Hand)obj;

                if (GetScore() > hand2.GetScore())
                {
                    return 1;
                }
                if (GetScore() < hand2.GetScore())
                {
                    return -1;
                }
                for (int i = 0; i < this.hand.Length; i++)
                {
                    if (hand[i] > hand2.hand[i])
                    {
                        return 1;
                    }

                    if (hand[i] < hand2.hand[i])
                    {
                        return -1;
                    }
                }
                return 1;
            }
        }
        public override string ToString()
        {
            string printString = "";
            for (int i = 0; i < hand.Length; i ++)
            {
                printString += hand[i];
            }

            return "Hand: " + printString + " , bid = " + bid + " , score = " + GetScore() ;
        }

    }
}

