using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CA
{
    internal class Card
    {
        //Lists for creating a deck in a method
        static List<string> CardSuits = new List<string>() { "Diamonds", "Hearts", "Clubs", "Spades" };
        static List<string> CardValues = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        static List<string> Deck = new List<string>();

        public int GetCardValue(string card) //Turns the string into an int so card values can be added
        {
            card.Split("of"); //card is now able to show the first character of the string

            char value = card[0]; //Single Character that is able to be converted later

            IDictionary<char, int> CardValueConverter = new Dictionary<char, int> //Converter for char so it can be an int
            {
                ['2'] = 2,
                ['3'] = 3,
                ['4'] = 4,
                ['5'] = 5,
                ['6'] = 6,
                ['7'] = 7,
                ['8'] = 8,
                ['9'] = 9,
                ['1'] = 10, //Since there is no 1 in a deck, 10 can be called as '1' when using char
                ['J'] = 10, //Jack
                ['Q'] = 10, //Queen
                ['K'] = 10, //King
                ['A'] = 11, //Ace
            };

            int cardvalue = CardValueConverter[value]; //Turns the char into an int from the dictionary
            return cardvalue;
        }

        public void DeckReshuffle()
        {
            foreach (string cardsuit in CardSuits) //4 suits
            {
                foreach (string cardvalue in CardValues)//13 types of cards
                {
                    string card = cardvalue + " of " + cardsuit; //adds 'of' inbetween the two values to make a sentence when drawing a card e.g. 'Queen of Spades'
                    
                    
                    Deck.Add(card); //Adds the card that came from the suit list and card value list into the deck list to have 52 cards
                }
            }
        }
        public string GetCard()//Method of drawing a card from the deck
        {
            Random random = new Random();
            int randomIndex = random.Next(0, Deck.Count); //Takes a random number from the list

            string cardDrawn = Deck[randomIndex]; //Turns the number into a card
            Deck.Remove(cardDrawn); //Removes the card out of the deck

 
            return cardDrawn; //Returns the card to be used elsewhere
        }







    }
}
