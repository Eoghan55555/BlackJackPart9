using System.Security.Cryptography.X509Certificates;

namespace BlackJack_CA
{
    internal class Program
    {
        static Card Player1 = new Card(); //Player 1 for card class
        static Betting _Player1 = new Betting(); //Player for betting class
        
        static Card Dealer = new Card(); //Dealer for card class
        
        static int p_num_ofcards = 0;//Count the num of cards a player would have
        static int d_num_ofcards = 0;//Count the num of cards a dealer would have
        
        static bool p_Ace = false;//If the player gets an Ace


        static bool d_Ace = false;//If the dealer gets an Ace


        static void Main(string[] args)
        {
            //Variables
            int[] compareValues = new int[2];

            //Start of the Game
            string name = _Player1.GetName();//Player enters a username
            double bettingChips = _Player1.GetTotalBettingChips();//Player enters the total amount of chips
            do
            {
                p_num_ofcards = 0;//Resets if the user continues
                d_num_ofcards = 0;

                Player1.DeckReshuffle();//Shuffles Deck

                _Player1.BetCalc();//Player enters how much they bet
                int[] handsValue = cardsDrawn(); //Two cards are drawn for player and dealer

                if (handsValue[0] != 21 || handsValue[1] != 21) //Game will continue if there is no BlackJack
                {
                    compareValues[0] = PlayerPlays(handsValue[0]);//Player goes first
                    compareValues[1] = DealerPlays(handsValue[1]);//Dealer then goes
                }

                GetResult(compareValues);//Then decides who wins

            } while (_Player1.GameOver() != true);//Game is in a loop until player loses all chips or chooses to end


            Console.WriteLine($"\nThank You for Playing, {name}!");
        }

        static int[] cardsDrawn()
        {
            //Variables
            string card;
            int cardValue;


            int[] handsvalue=new int[2];//Player is the value and the Dealer is the second value 


            //Player goes first
            while (p_num_ofcards <= 1) //Gets the first two cards for the player
            {
                card = Player1.GetCard();//Gets the card as a string
                cardValue = Player1.GetCardValue(card);//Turns the string value into a number
  
                Console.WriteLine($"\nYou have drawn {card} which is worth {cardValue}");
                handsvalue[0] += cardValue;

                p_num_ofcards++;

                ConfirmPlayerAce(card);


            }
            Console.WriteLine($"Your total is {handsvalue[0]}\n");


            //Dealer goes next
            while (d_num_ofcards < 1) //Gets the first two cards for the dealer
            {
                card = Dealer.GetCard();//Gets the card as a string
                cardValue = Dealer.GetCardValue(card);//Turns the string value into a number
                
                Console.WriteLine($"Dealer has drawn {card} which is worth {cardValue}");
                handsvalue[1] += cardValue;

                d_num_ofcards++;
            }
            Console.WriteLine($"Dealer's total is {handsvalue[1]}\n");
            return handsvalue;
        }
        static int PlayerPlays(int playersHandValue)
        {
            //Variables
            string input = "";
            string lowercaseinput="";
            string card;
            int cardValue;
;
            Console.Write("\nDo you want to stay or hit (s/h):  ");
            input = Console.ReadLine();
            lowercaseinput = input.ToLower();
            if (lowercaseinput != "stay" && lowercaseinput != "s" && lowercaseinput != "hit" && lowercaseinput != "h" && string.IsNullOrEmpty(lowercaseinput)) 
            {
                while (lowercaseinput != "stay" && lowercaseinput != "s" && lowercaseinput != "hit" && lowercaseinput != "h" && string.IsNullOrEmpty(lowercaseinput)) 
                {
                    Console.WriteLine("\nError Try Again");

                    Console.Write("\nDo you want to stay or hit (s/h): ");
                    input = Console.ReadLine();
                    lowercaseinput = input.ToLower();
                }
            }
            while (lowercaseinput == "stay" || lowercaseinput == "s" || lowercaseinput == "hit" || lowercaseinput == "h")  //Covers some answers to end their turn
            {

                if (lowercaseinput == "stay" || lowercaseinput == "s")  //First Break of the loop
                {
                    break;
                }
                card = Player1.GetCard(); //Draws a card
                cardValue = Player1.GetCardValue(card); //Cards gets a value based on what card it is
                p_num_ofcards++;//The players number of cards increases

                Console.WriteLine($"You have drawn {card} which is worth {cardValue}");//Displays both type of card and its value
                
                playersHandValue+= cardValue; //adds it to a total value of the hand

                ConfirmPlayerAce(card);

                if (p_Ace == true && playersHandValue > 21)//If player has an ace and goes above 21 it decreases the hand value by 10
                {
                    playersHandValue -= 10;
                    p_Ace = false; //That Ace has been changed can't be used again to reduce the hand value
                }
                

                Console.WriteLine($"Total is now {playersHandValue}\n");//Displays the hand value

                if (playersHandValue >= 21) //Second check to see if the reached to or past 21
                {
                    break;
                }

                Console.Write("\nDo you want to stay or hit (s/h): ");
                input = Console.ReadLine();
                lowercaseinput = input.ToLower();
                if (lowercaseinput != "stay" && lowercaseinput != "s" && lowercaseinput != "hit" && lowercaseinput != "h" && string.IsNullOrEmpty(lowercaseinput))
                {
                    while (lowercaseinput != "stay" && lowercaseinput != "s" && lowercaseinput != "hit" && lowercaseinput != "h" && string.IsNullOrEmpty(lowercaseinput))
                    {
                        Console.WriteLine("\nError Try Again");

                        Console.Write("\nDo you want to stay or hit (s/h): ");
                        input = Console.ReadLine();
                        lowercaseinput = input.ToLower();
                    }
                }


            } 

            
            return playersHandValue;//returns the hand value to allow it to compare to the dealer's
        }

        static int DealerPlays(int dealersHandValue)
        {
            while (dealersHandValue < 17)
            {
                string card = Dealer.GetCard();//Gets the card as a string
                int cardValue = Dealer.GetCardValue(card);//Turns the string value into a number

                d_num_ofcards++;

                ConfirmDealerAce(card);


                Console.WriteLine($"\nDealer has drawn {card} which is worth {cardValue}");//Displays both type of card and its value

                dealersHandValue += cardValue;//adds it to a total value of the hand

                if (d_Ace == true && dealersHandValue > 21)//If player has an ace and goes above 21 it decreases the hand value by 10
                {
                    dealersHandValue -= 10;
                    Console.WriteLine($"Hand is now {dealersHandValue} since there is an Ace");
                    d_Ace= false; //That Ace has been changed can't be used again to reduce the hand value
                }

                Console.WriteLine($"Dealer's Total is now {dealersHandValue}\n");//Displays the hand value
            }
            return dealersHandValue;//returns the hand value to allow it to compare to the player's
        }

        static void GetResult(int[] handsValue)//0 is the player and 1 is the dealer
        {
            //Loss Scenarios
            if (handsValue[0] > 21) //Player Busts
            {
                _Player1.BetWinning(Betting.Result.Loss);
            }
            else if (handsValue[0] < handsValue[1] && handsValue[1] <= 21) //Dealer is below 22 and has more than the player
            {
                _Player1.BetWinning(Betting.Result.Loss);
            }
            else if (handsValue[1] == 21 && d_num_ofcards == 2 && handsValue[0] < handsValue[2])//Dealer Gets BlackJack and player does not
            {
                _Player1.BetWinning(Betting.Result.Loss);
            }

            //Win Scenarios
            else if (handsValue[1] > 21)  //Player is Below 22 and Dealer Busts
            {
                _Player1.BetWinning(Betting.Result.Win);
            }
            else if ((handsValue[0] > handsValue[1]) && handsValue[0] == 21 && p_num_ofcards != 2) //Player has more than Dealer and not BlackJack
            {
                _Player1.BetWinning(Betting.Result.Win);
            }
            else if (handsValue[0] > handsValue[1] && handsValue[0] != 21)
            {
                _Player1.BetWinning(Betting.Result.Win);
            }
            //Player gets BlackJack
            else if (handsValue[0] == 21 && p_num_ofcards == 2)  //Player gets a natural 21
            {
                _Player1.BetWinning(Betting.Result.BlackJack);
            }

            //Draw Scenarios
            else if (handsValue[0] == handsValue[1]) //Player and Dealer has the same amount
            {
                _Player1.BetWinning(Betting.Result.Draw);
            }


        }

        static void ConfirmPlayerAce(string card)//if a player gets an ace
        {
            card.Split("of");
            char confirm_Ace = card[0];
            if (confirm_Ace == 'A')
            {
                p_Ace = true;
            }

        }

        static void ConfirmDealerAce(string card)
        {
            card.Split("of");
            char confirm_Ace = card[0];
            if (confirm_Ace == 'A')
            {
                d_Ace = true;
            }

        }
    }
}