using System;

namespace Enums_Mission
{
    class Program
    {
        enum Suits
        {
            Hearts,
            Spades,
            Diamonds,
            Clubs
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DrawAce(Suits.Spades);
        }

        static void DrawAce(Suits suit)
        {
            string line = "+───────+";
            Console.WriteLine(line);
            line = "│A      │";
            Console.WriteLine(line);
            string printingSuit = "♥";
            int numberSuit = 0;
            switch (suit)
            {
                case Suits.Hearts:
                    printingSuit = "♥";
                    break;
                case Suits.Spades:
                    printingSuit = "♠";
                    break;
                case Suits.Diamonds:
                    printingSuit = "♦";
                    break;
                case Suits.Clubs:
                    printingSuit = "♣";
                    break;
            }
            for (int i = 0; i < 3; i++)
            {
                line = "|";
                for (int x = 0; x < 7; x++)
                {
                    
                    if (x == numberSuit)
                    {
                        line += printingSuit;
                        
                    }
                    else
                    {
                        line += " ";
                    }
                 
                }
                numberSuit += 3;
                line += "|";
                Console.WriteLine(line);
            }
            
            line = "│      A│";
            Console.WriteLine(line);
            line = "+───────+";
            Console.WriteLine(line);
        }
    }
}
