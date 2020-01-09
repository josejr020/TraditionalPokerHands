using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluatesPokerHands
{
    public class Poker
    {
        private string[] cardValue = { "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
        private Card[] cards = new Card[5];
        private Random rand = new Random();
        private string cardValues = "AKQJT98765432";
        private string cardSuits = "SHCD";

        public class PlayerwithCards
        {
            public string Player { get; set; }
            public string[] Cards { get; set; }
        }

        public string CheckWinner(List<PlayerwithCards> playersWithCards)
        {
            string winner = "";
            int winners = 0;
            int rankValueCompare = 9;
            int[] cardsSortCompare = { 12, 12, 12, 12, 12 };
            int firstPairCardsCompare = 12;
            int secondPairCardsCompare = 12;
            string result = "";
            string playersData = "";


            foreach (PlayerwithCards playerWithCards in playersWithCards)
            {
                string rankName = "";
                int rankValue = 0;
                int[] cardsSort = null;
                int firstPairCards = 0;
                int secondPairCards = 0;
                string playerData = "";
                CheckCardsPokerRank(playerWithCards.Player, playerWithCards.Cards, ref rankName, ref cardsSort, ref firstPairCards, ref secondPairCards, ref rankValue, ref playerData);

                // compare their cards

                if (rankValue < rankValueCompare)
                {
                    rankValueCompare = rankValue;
                    winner = playerWithCards.Player;
                    firstPairCardsCompare = firstPairCards;
                    secondPairCardsCompare = secondPairCards;
                    cardsSortCompare = cardsSort;
                    winners = 1;
                }
                else if (rankValue == rankValueCompare)
                {
                    if ((firstPairCards > firstPairCardsCompare && secondPairCards > secondPairCardsCompare && secondPairCards != -1)
                        || firstPairCards > firstPairCardsCompare || (secondPairCards > secondPairCardsCompare && secondPairCards != -1))
                    {
                        rankValueCompare = rankValue;
                        winner = playerWithCards.Player;
                        firstPairCardsCompare = firstPairCards;
                        secondPairCardsCompare = secondPairCards;
                        cardsSortCompare = cardsSort;
                        winners = 1;
                    }
                    else if (firstPairCards == firstPairCardsCompare && secondPairCards == secondPairCardsCompare && secondPairCards != -1)
                    {
                        if (cardsSort.Last() < cardsSortCompare.Last())
                        {
                            rankValueCompare = rankValue;
                            winner = playerWithCards.Player;
                            firstPairCardsCompare = firstPairCards;
                            secondPairCardsCompare = secondPairCards;
                            cardsSortCompare = cardsSort;
                            winners = 1;
                        }
                        else if (cardsSort.Last() == cardsSortCompare.Last())
                        {
                            winner += " , " + playerWithCards.Player;
                            winners++;
                        }
                    }
                    else if (firstPairCards == firstPairCardsCompare && firstPairCards != -1)
                    {
                        if (cardsSort[2] < cardsSortCompare[2] ||
                            (cardsSort[2] == cardsSortCompare[2] && cardsSort[3] < cardsSortCompare[3]) ||
                            (cardsSort[2] == cardsSortCompare[2] && cardsSort[3] == cardsSortCompare[3] && cardsSort[4] < cardsSortCompare[4]))
                        {
                            rankValueCompare = rankValue;
                            winner = playerWithCards.Player;
                            firstPairCardsCompare = firstPairCards;
                            secondPairCardsCompare = secondPairCards;
                            cardsSortCompare = cardsSort;
                            winners = 1;
                        }
                        else if (Enumerable.SequenceEqual(cardsSort, cardsSortCompare))
                        {
                            winner += " , " + playerWithCards.Player;
                            winners++;
                        }
                    }
                    else if ((cardsSort.Min() < cardsSortCompare.Min()) ||
                        (cardsSort[1] == cardsSortCompare[1] && cardsSort[2] < cardsSortCompare[2]) ||
                        (cardsSort[1] == cardsSortCompare[1] && cardsSort[2] == cardsSortCompare[2] && cardsSort[3] < cardsSortCompare[3]) ||
                        (cardsSort[1] == cardsSortCompare[1] && cardsSort[2] == cardsSortCompare[2] && cardsSort[3] == cardsSortCompare[3] && cardsSort[4] < cardsSortCompare[4]))
                    {
                        rankValueCompare = rankValue;
                        winner = playerWithCards.Player;
                        firstPairCardsCompare = firstPairCards;
                        secondPairCardsCompare = secondPairCards;
                        cardsSortCompare = cardsSort;
                        winners = 1;
                    }
                    else if (Enumerable.SequenceEqual(cardsSort, cardsSortCompare))
                    {
                        winner += " , " + playerWithCards.Player;
                        winners++;
                    }
                }
                playersData += playerData + "\n";
            }

            if (winners == 1)
            {
                result = "The winner is: " + winner + "\n";
                result += playersData;
            }
            else if (winners < playersWithCards.Count())
            {
                result = "The winners are: " + winner + "\n";
                result += playersData;
            }
            else
            {
                result = "Congratulations! All Winners \n";
                result += playersData;
            }

            return result;
        }

        // check cards with its traditional poker rank
        private void CheckCardsPokerRank(string player, string[] playerCards, ref string rankName, ref int[] cardsSortByMultiple, ref int firstPairCards, ref int secondPairCards, ref int rankValue, ref string playerData)
        {
            int count = 0;
            int[] cardsSortedByMultiple = new int[5];
            foreach (string playerCard in playerCards)
            {
                int value = cardValues.IndexOf(playerCard[0]);
                int suit = cardSuits.IndexOf(playerCard[1]);
                cards[count] = new Card(value, suit);
                count++;
            }
            // sort cards by multiple           
            var sorting = from card in cards
                          group card by card.Value into g
                          orderby g.Count() descending, g.Key
                          select g;
            count = 0;
            foreach (var g in sorting)
            {
                foreach (Card c in g)
                {
                    cardsSortedByMultiple[count] = c.Value;
                    count++;
                }

            }

            cardsSortByMultiple = cardsSortedByMultiple;

            int handType = -1;
            string hand = null;

            // check for cards if  there has pairs or multiple 
            int count1 = sorting.First().Count();
            int count2 = sorting.ElementAt(1).Count();
            int firstValue = sorting.First().Key;
            int secondValue = sorting.ElementAt(1).Key;

            if (count1 == 4)
            {
                handType = 2;
                hand = "Four of a Kind";
                firstPairCards = cardValues.IndexOf(cardValue[firstValue]);
                secondPairCards = -1; // means no value
            }
            else if (count1 == 3 && count2 >= 2)
            {
                handType = 3;
                hand = "Full House";
                firstPairCards = cardValues.IndexOf(cardValue[firstValue]);
                secondPairCards = cardValues.IndexOf(cardValue[secondValue]);
            }
            else if (count1 == 3)
            {
                handType = 6;
                hand = "Three of a Kind";
                firstPairCards = cardValues.IndexOf(cardValue[firstValue]);
                secondPairCards = -1; // means null
            }
            else if (count1 == 2 && count2 == 2)
            {
                handType = 7;
                hand = "Two Pair";
                firstPairCards = cardValues.IndexOf(cardValue[firstValue]);
                secondPairCards = cardValues.IndexOf(cardValue[secondValue]);
            }
            else if (count1 == 2)
            {
                handType = 8;
                hand = "One Pair";
                firstPairCards = cardValues.IndexOf(cardValues[firstValue]);
                secondPairCards = -1; // means no value
            }
            else
            {
                handType = 9;
                hand = "High Card";
                firstPairCards = -1;
                secondPairCards = -1; // means no value
            }

            // sort cards by value
            cards = (from card in cards orderby card.Value select card).ToArray();

            // check cards for straight
            Card[] distinctCards = cards.Distinct(new CardValueComparer()).ToArray(); // this will remove cards with duplicate values

            bool straight = false;

            if (distinctCards.Length >= 5)
            {
                for (int i = 0; i < distinctCards.Length - 4; i++)
                {
                    if (distinctCards[i].Value == distinctCards[i + 4].Value - 4)
                    {
                        if (handType > 5)
                        {
                            hand = "Straight";
                            handType = 5;
                        }
                        straight = true;
                        break;
                    }
                }
            }

            // sort card by suit
            sorting = from card in cards
                      group card by card.Suit into g
                      orderby g.Count() descending
                      select g;

            // check cards for flush
            count = sorting.First().Count();

            if (count >= 5)
            {
                int index = sorting.First().First().Value;
                int suit = sorting.First().Key;

                if (handType > 4)
                {
                    handType = 4;
                    hand = "Flush";
                }

                if (straight)
                {
                    // check cards for straight flush        
                    Card[] flushCards = (from card in sorting.First() orderby card.Value select card).ToArray();

                    for (int i = 0; i < count - 4; i++)
                    {
                        if (flushCards[i].Value == flushCards[i + 4].Value - 4)
                        {
                            bool straightFlush = true;
                            int flushSuit = flushCards[i].Suit;

                            for (int j = i + 1; j < i + 5; j++)
                            {
                                if (flushSuit != flushCards[j].Suit)
                                {
                                    straightFlush = false;
                                    break;
                                }
                            }

                            if (straightFlush)
                            {
                                //check if flush is Royal Flush
                                // 0 means Ace
                                if (cardsSortedByMultiple.Min() == 0)
                                {
                                    handType = 0;
                                    hand = "Royal Flush";
                                }
                                else
                                {
                                    handType = 1;
                                    hand = "Staight Flush";
                                }

                                break;
                            }
                        }
                    }
                }
            }

            string outputData = player + ":";
            foreach (Card c in cards)
            {
                outputData += c.ToString() + ", ";
            }
            outputData = outputData.Remove(outputData.Length - 2);
            outputData += " (" + hand + ")";
            playerData = outputData;
            rankName = hand;
            rankValue = handType;

        }
    }

    struct Card
    {
        static string values = "AKQJT98765432";
        static string suits = "SHCD";

        public readonly int Value;
        public readonly int Suit;

        public Card(int value, int suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", values[Value], suits[Suit]);
        }
    }

    class CardValueComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card c1, Card c2)
        {
            return c1.Value == c2.Value;
        }

        public int GetHashCode(Card c)
        {
            return c.Value.GetHashCode();
        }
    }
}
