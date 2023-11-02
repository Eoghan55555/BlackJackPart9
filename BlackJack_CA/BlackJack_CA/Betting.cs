using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CA
{
    internal class Betting
    {
        public enum Result { BlackJack, Win, Draw, Loss } //Enum for each scenario in BlackJack
        
        private int _bettingChips;
        private int _betValue;
        private string _name;
        
        public int BetValue 
        { 
            get;
            set; 
        }
        public int BettingChips
        {
      
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Betting()
        {

        }
        
        
        public void BetWinning(Result match)
        {
            if (match == Result.BlackJack) //Scenario when player gets BlackJack
            {
                int blackjack = (int)(_betValue * 2.5); // Bet = 100 -> Winnings = 250 (Profit = 150)
                Console.WriteLine("BlackJack!\n");

                _bettingChips = _bettingChips + blackjack;
                Console.WriteLine($"You won {blackjack}\nTotal is now {_bettingChips}");
            }
            else if (match == Result.Win) //Scenario when player wins
            {
                double winning = _betValue * 2; // Bet = 100 -> Winnings = 200 (Profit = 100)
                Console.WriteLine("Win!\n");

                _bettingChips = (int)(_bettingChips + winning);
                Console.WriteLine($"You won {winning} chip(s)\nTotal is now {_bettingChips}");
            }
            else if (match == Result.Draw) //Scenario when player draws
            {
                double draw = _betValue; // Bet = 100 -> Winnings = 100 (Profit = 0)
                _bettingChips = (int)(_bettingChips + draw);

                Console.WriteLine("Draw!\n");
                Console.WriteLine($"No chips lost you still have {_bettingChips} chips left");
            }
            else if (match == Result.Loss) //Scenario when player loses or busts
            {
                double losing = _betValue; // Bet = 100 -> Winnings = 0 (Profit = -100)
                Console.WriteLine("Loss!\n");

                Console.WriteLine($"You lost {losing} chip(s)\nTotal is now {_bettingChips}");
            }
        }
        public string GetName() //Gets the username of the player
        {
            Console.Write("Enter Name: ");
            _name = Console.ReadLine();
            if (string.IsNullOrEmpty(_name))
            {
                while (string.IsNullOrEmpty(_name))
                {
                    Console.WriteLine("\nError\n");
                    Console.Write("Please Re-Enter Name: ");
                    _name = Console.ReadLine();
                }
            }
            return _name;
        }
        public int GetTotalBettingChips() //Gets the total amount of chips
        {
            Console.Write("Enter the amount of betting chips you want to have: ");
            string chipsconverter = Console.ReadLine();
            if (string.IsNullOrEmpty(chipsconverter)|| !int.TryParse(chipsconverter, out _bettingChips) || int.Parse(chipsconverter) < 0)
            {
                while (string.IsNullOrEmpty(chipsconverter) || !int.TryParse(chipsconverter, out _bettingChips) || int.Parse(chipsconverter) < 0)
                {
                    Console.WriteLine("\nError\n");
                    Console.Write("Re-Enter the amount of betting chips you want to have: ");
                    chipsconverter = Console.ReadLine();
                }

            }
            return _bettingChips;
        }
        public double BetCalc() //Calculate how much a user wants to bet
        {
            Console.Write("Enter Bet Amount: ");
            string chipsconverter = Console.ReadLine();
            if (string.IsNullOrEmpty(chipsconverter) || !int.TryParse(chipsconverter, out _betValue) || int.Parse(chipsconverter) < 0) //Validate input
            {
                while (string.IsNullOrEmpty(chipsconverter) || !int.TryParse(chipsconverter, out _betValue) || int.Parse(chipsconverter) < 0)
                {
                    Console.WriteLine("\nError\n");
                    Console.Write("Re-Enter Bet Amount: ");
                    chipsconverter = Console.ReadLine();
                }

            }

            if (_betValue >= _bettingChips) //To avoid going into negative chips
            {
                _betValue = _bettingChips; //Essentially going all in
                _bettingChips = 0;
            }
            else if (_betValue < _bettingChips) //When a nromal bet is (less than the amount they have)
            {
                _bettingChips = _bettingChips - _betValue;
            }
            return _bettingChips;
        }
        public bool GameOver()
        {
            bool gameover = false;
            if (_bettingChips == 0) //If player is out of chips
            {
                gameover = true;
                Console.WriteLine("Out of Chips.\nGame Over!");
            }

            else
            {
                Console.Write("\nContinue Y/N: ");
                string input = Console.ReadLine();
                string higher_Input = input.ToUpper();
                if (higher_Input != "Y" || higher_Input != "YES" || higher_Input != "N" || higher_Input != "NO") //Or if player chooses to leave
                {
                    while (higher_Input != "Y" && higher_Input != "YES" && higher_Input != "N" && higher_Input != "NO")
                    {
                        Console.WriteLine("Error Please Try Again\n");
                        Console.Write("\nContinue Y/N: ");
                        input = Console.ReadLine();
                        higher_Input = input.ToUpper();
                    }
                }
                if (higher_Input == "N" || higher_Input == "NO")
                {
                    gameover = true;
                }
            }
            return gameover;
        }

    }
}
