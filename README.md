# TraditionalPokerHands
This will return the value of the Winner/s with Players details

# How to use the EvaluatesPokerHands library
1. Create new project in visual studio
2. Add referrence then add this library(.dll)
3. In your program, add header like below:
'''
using EvaluatesPokerHands;
'''
4. Declare Poker.PlayerwithCards list and then, Add Players with their 5 cards like this
'''
List<Poker.PlayerwithCards> PlayersWithCards = new List<Poker.PlayerwithCards>();
PlayersWithCards.Add(new Poker.PlayerwithCards() { Player = "Jose", Cards = new string[] { "AC", "3C", "4S", "9C", "TC" } });
PlayersWithCards.Add(new Poker.PlayerwithCards() { Player = "Jun", Cards = new string[] { "AH", "3S", "4D", "9D", "TS" }});
PlayersWithCards.Add(new Poker.PlayerwithCards() { Player = "Jose Jr", Cards = new string[] { "QD", "3H", "4H", "9S", "TD" }});
'''
Note: This .dll only set 5 cards only for each player

5. Declare Poker to call the function CheckWinner.
'''
Poker poker = new Poker();
string result = poker.CheckWinner(PlayersWithCards);
'''
The return value of result is displaying the winner's and All Players details like stated below:
'''
The winners are: Jose , Jun
Jose:AC, TC, 9C, 4S, 3C (High Card)
Jun:AH, TS, 9D, 4D, 3S (High Card)
Jose Jr:QD, TD, 9S, 4H, 3H (High Card)
''' 
  
