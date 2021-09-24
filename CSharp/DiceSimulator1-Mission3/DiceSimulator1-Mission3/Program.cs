using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;


/*
 
  _____ _____ _____ ______    _____ ______ _   _ ______ _____         _______ ____  _____  
 |  __ \_   _/ ____|  ____|  / ____|  ____| \ | |  ____|  __ \     /\|__   __/ __ \|  __ \ 
 | |  | || || |    | |__    | |  __| |__  |  \| | |__  | |__) |   /  \  | | | |  | | |__) |
 | |  | || || |    |  __|   | | |_ |  __| | . ` |  __| |  _  /   / /\ \ | | | |  | |  _  / 
 | |__| || || |____| |____  | |__| | |____| |\  | |____| | \ \  / ____ \| | | |__| | | \ \ 
 |_____/_____\_____|______|  \_____|______|_| \_|______|_|  \_\/_/    \_\_|  \____/|_|  \_\
                                     .-------.    ______
                                    /   o   /|   /\     \
                                   /_______/o|  /o \  o  \
                                   | o     | | /   o\_____\
                                   |   o   |o/ \o   /o    /
                                   |     o |/   \ o/  o  /

                       d8b                                                                      
                       ?88                                                                      
                        88b                                                                     
                        888888b ?88   d8P                                                       
                        88P `?8bd88   88                                                        
                       d88,  d88?8(  d88                                                        
                      d88'`?88P'`?88P'?8b                                                       
                                       )88                                                      
                                      ,d8P                                                      
 ?88                       88P                                                                               
  88b                     d88                                                                                
  888888b    `?d8888b     888      ?88   d8P      d888b8b       d8888b     d8888b       88bd88b      88bd88b 
  88P `?8b    d8P' ?88    ?88      d88   88      d8P' ?88      d8P' `P    d8P' ?88      88P'  `      88P' ?8b
 d88   88P    88b  d88     88b     ?8(  d88      88b  ,88b     88b        88b  d88     d88          d88   88P
d88'   88b    `?8888P'      88b    `?88P'?8b     `?88P'`88b    `?888P'    `?8888P'    d88'         d88'   88b
                                          )88                                                                
                                         ,d8P                                                                
                                      `?888P'                                                                                                                         
                                                                                           
 
 */
namespace DiceSimulator1_Mission3
{
    class Program
    {

        static Random random = new Random();
        static int charTimer = 10;
        static int titleTimer = 1;
        static void Main(string[] args)
        {
            string[] title = new string[]
                { @"  _____ _____ _____ ______    _____ ______ _   _ ______ _____         _______ ____  _____ ",
                @" |  __ \_   _/ ____|  ____|  / ____|  ____| \ | |  ____|  __ \     /\|__   __/ __ \|  __ \ ",
                @" | |  | || || |    | |__    | |  __| |__  |  \| | |__  | |__) |   /  \  | | | |  | | |__) |",
                @" | |  | || || |    |  __|   | | |_ |  __| | . ` |  __| |  _  /   / /\ \ | | | |  | |  _  / ",
                @" | |__| || || |____| |____  | |__| | |____| |\  | |____| | \ \  / ____ \| | | |__| | | \ \ ",
                @" |_____/_____\_____|______|  \_____|______|_| \_|______|_|  \_\/_/    \_\_|  \____/|_|  \_\",
                @"                                     .-------.    ______",
                @"                                    /   o   /|   /\     \",
                @"                                   /_______/o|  /o \  o  \",
                @"                                   | o     | | /   o\_____\",
                @"                                   |   o   |o/ \o   /o    /",
                @"                                   |     o |/   \ o/  o  /",
                @"                                   '-------'     \/____o/",
                @"                                     d8b",
                @"                                     ?88",
                @"                                      88b",
                @"                                      888888b ?88   d8P",
                @"                                      88P `?8bd88   88",
                @"                                     d88,  d88?8(  d88",
                @"                                    d88'`?88P'`?88P'?8b",
                @"                                                     )88",
                @"                                                    ,d8P",
                @"                                                 `?888P",
                @" ?88                       88P",
                @"  88b                     d88       ",
                @"  888888b    `?d8888b     888      ?88   d8P      d888b8b       d8888b     d8888b       88bd88b      88bd88b ",
                @"  88P `?8b    d8P' ?88    ?88      d88   88      d8P' ?88      d8P' `P    d8P' ?88      88P'  `      88P' ?8b",
                @" d88   88P    88b  d88     88b     ?8(  d88      88b  ,88b     88b        88b  d88     d88          d88   88P",
                @"d88'   88b    `?8888P'      88b    `?88P'?8b     `?88P'`88b    `?888P'    `?8888P'    d88'         d88'   88b",
                @"                                          )88",
                @"                                          ,d8P",
                @"                                       `?888P'"};
            Console.ForegroundColor = ConsoleColor.Cyan;
            int titleRow = 0;
            foreach (string str in title)
            {
                
                if (titleRow == 13)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Thread.Sleep(2);
                foreach (char c in str)
                {
                    
                    Console.Write(c);
                    Thread.Sleep(titleTimer);
                }
                titleRow++;
                Console.WriteLine();
            }

            string input = "";

            int finalScore = 0;

            while (finalScore == 0)
            {

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Thread.Sleep(1000);
                string enterText = "Enter desired dice roll in standard dice notation:";
                foreach (char c in enterText)
                {
                    Console.Write(c);
                    Thread.Sleep(charTimer);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
                Thread.Sleep(500);
                bool repeat = true;
                while (repeat)
                {
                    string throwingText = $"Throwing {input}";
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;


                        foreach (char c in throwingText)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        finalScore = DiceRoll(input);
                        
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        string errorMessage = e.Message;
                        Console.WriteLine();
                        
                        foreach (char c in errorMessage)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                            
                        }
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                        
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Thread.Sleep(50);
                    string finalScoreText = $"Your final score is: {finalScore}!";
                    foreach (char c in finalScoreText)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Console.WriteLine();
                    bool repeatOption = true;
                    do
                    {
                        
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine();
                        string optionsText = "Would you like to (r)epeat, enter a (n)ew roll, or (q)uit?";
                        foreach (char c in optionsText)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        string[] validOptionsInput = new string[] { "r", "n", "q" };
                        string optionsInput = Console.ReadLine();

                        if (optionsInput == "r")
                        {
                            repeat = true;
                            repeatOption = false;
                        }
                        else if (optionsInput == "n")
                        {
                            repeat = false;
                            repeatOption = false;
                            finalScore = 0;
                        }
                        else if (optionsInput == "q")
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            repeat = false;
                            repeatOption = true;
                            try
                            {
                                validOptionsInput[4] = "";
                            }
                            catch (Exception e)
                            {

                                Console.ForegroundColor = ConsoleColor.Red;
                                string errorMessage = "That option is not a valid input.";
                                Console.WriteLine();

                                foreach (char c in errorMessage)
                                {
                                    Console.Write(c);
                                    Thread.Sleep(charTimer);
                                }
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                        }
                    }
                    while (repeatOption);
                }
                

                
            }





        }


        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            int sleepTimer = 1000;
            int result = 0;
            int roll = 0;
            

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine();
            string simulateText = "Simulating";
            foreach (char c in simulateText)
            {
                Console.Write(c);
                Thread.Sleep(charTimer);
            }

            Thread.Sleep(sleepTimer);
            Console.Write(".");
            Thread.Sleep(sleepTimer);
            Console.Write(".");
            Thread.Sleep(sleepTimer);
            Console.Write(".");
            Console.WriteLine();

            for (int i = 0; i < numberOfRolls; i++)
            {
                List<string> diceAscii = new List<string> { };
                roll = random.Next(1, diceSides + 1);
                bool drawDice = true;
                switch (diceSides)
                {
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        diceAscii.Add(@"  /\.");
                        diceAscii.Add($" /{roll} \\'.");
                        diceAscii.Add(@"/____\/");
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        diceAscii.Add(@" _____");
                        diceAscii.Add(@"|     |\");
                        diceAscii.Add($"|  {roll}  | |");
                        diceAscii.Add(@"|_____| |");
                        diceAscii.Add(@" \_____\|");
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        diceAscii.Add(@"    .");
                        diceAscii.Add(@"  ./ \.");
                        diceAscii.Add($".'/ {roll} \\'.");
                        diceAscii.Add(@"|/_____\|");
                        diceAscii.Add(@"|\     /|");
                        diceAscii.Add(@"'.\   /.'");
                        diceAscii.Add(@"  '\ /'");
                        diceAscii.Add(@"    '");
                        break;
                    case 10:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        diceAscii.Add(@"    .");
                        diceAscii.Add(@"  ./ \.");
                        if (roll < 10)
                        {
                            diceAscii.Add($".'/ {roll} \\'.");
                        }
                        else
                        {
                            diceAscii.Add($".'/ {roll}\\'.");
                        }
                        diceAscii.Add(@"|/\___/\|");
                        diceAscii.Add(@" '. | .'");
                        diceAscii.Add(@"   '|'");
                        break;

                    case 12:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        diceAscii.Add(@"     _._");
                        diceAscii.Add(@"  .-'.|.'-.");
                        diceAscii.Add(@" /_.'   '._\");
                        if (roll < 10)
                        {
                            diceAscii.Add($"|  \\  {roll}  /  |");
                        }
                        else
                        {
                            diceAscii.Add($"|  \\  {roll} /  |");
                        }
                        diceAscii.Add(@" \  \___/  /");
                        diceAscii.Add(@"  \ /   \ /");
                        diceAscii.Add(@"   '-._.-'");
                        break;
                    case 20:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        diceAscii.Add(@"     ____");
                        diceAscii.Add(@" _-='_|__'=-_");
                        diceAscii.Add(@"|\    /\    /|");
                        if (roll < 10)
                        {
                            diceAscii.Add($"| \\  / {roll}\\  / | ");
                        }
                        else
                        {
                            diceAscii.Add($"| \\  /{roll}\\  / | ");
                        }
                        diceAscii.Add(@"|__\/____\/__|");
                        diceAscii.Add(@"'-_ '.   .'_-'");
                        diceAscii.Add(@"   '-.\/.-'");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        drawDice = false;
                        break;


                }
                Thread.Sleep(sleepTimer);
                string order = "";
                switch (i)
                {
                    case 0:
                        order = "st";
                        break;
                    case 1:
                        order = "nd";
                        break;
                    case 3:
                        order = "rd";
                        break;
                    default:
                        order = "th";
                        break;
                }
                
                Console.WriteLine();
                string rollText = $"{i + 1}{order} roll is: {roll}";
                if (!drawDice)
                {
                    foreach (char c in rollText)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                }
                else
                {
                    foreach (string str in diceAscii)
                    {
                        foreach (char c in str)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Console.WriteLine();
                        Thread.Sleep(50);
                    }
                }
                Console.WriteLine();
                result += roll;
            }
            result += fixedBonus;
            return result;
        }

        /*  static int DiceRoll(string diceNotation)
           {
               string diceRegex = @"(\d+)?d(\d+)[+]?(\d)?";
               Match diceMatch = Regex.Match(diceNotation, diceRegex);

               bool isValid;

               int numberOfRolls = 0;
               int diceSides;
               int fixedBonus = 0;
               int finalScore = 0;
               int tryCounter = 1;

               try
               {
                   tryCounter = 1;
                   numberOfRolls = Int32.Parse(diceMatch.Groups[1].Value);
                   tryCounter++;
                   diceSides = Int32.Parse(diceMatch.Groups[2].Value);
                   tryCounter += 2;
                   if (diceMatch.Groups[4].Success)
                   {
                       fixedBonus = Int32.Parse(diceMatch.Groups[4].Value);
                   }
                   isValid = true;
               }
               catch (Exception)
               {
                   isValid = false;
                   errorMessage = diceMatch.Groups[tryCounter].Value + " is not an integer";
               }
               if (isValid)
               {
                   if (diceMatch.Success)
                   {
                       if (diceMatch.Groups[1].Value != "")
                       {
                           numberOfRolls = Convert.ToInt32(diceMatch.Groups[1].Value);

                       }
                       else
                       {
                           numberOfRolls = 1;
                       }
                       diceSides = Convert.ToInt32(diceMatch.Groups[2].Value);
                       if (diceMatch.Groups[4].Success)
                       {
                           if (diceMatch.Groups[3].Value == "+")
                           {
                               fixedBonus = Convert.ToInt32(diceMatch.Groups[4].Value);
                           }
                           else if (diceMatch.Groups[3].Value == "-")
                           {
                               fixedBonus = -Convert.ToInt32(diceMatch.Groups[4].Value);
                           }

                       }
                       else
                       {
                           fixedBonus = 0;
                       }
                       finalScore = DiceRoll(numberOfRolls, diceSides, fixedBonus);

                   }
                   else
                   {
                       finalScore = 0;
                   }
               }
               else
               {
                   finalScore = 0;
               }

               return finalScore;
           }*/

        static int DiceRoll(string diceNotation)
        {

            char[] modifier = new char[] { 'd', '+', '-' };
            string[] splitDice = diceNotation.Split(modifier);

            int numberOfRolls = 0;
            int diceSides = 0;
            int fixedBonus = 0;
            int finalScore = 0;
            if (!diceNotation.Contains('d'))
            {
                throw new ArgumentException("You did not follow the correct notation, try again: ");
            }
            if (splitDice[0] != "")
            {
                if (splitDice[0].Contains('*') || splitDice[0].Contains('/'))
                {
                    throw new ArgumentException($"You cannot use a divide or multiply sign. Try again:");
                }
                try
                {

                    numberOfRolls = Convert.ToInt32(splitDice[0]);

                }
                catch (Exception e)
                {

                    throw new ArgumentException($"{splitDice[0]} is not an integer. Try again:");
                }
                if (numberOfRolls == 0)
                {
                    throw new ArgumentException($"You cannot throw 0 dice! Try again:");
                }
            }
            else
            {
                if (diceNotation.StartsWith('-'))
                {
                    throw new ArgumentException($"You can only use positive numbers! ({diceNotation[0]}{splitDice[1]}) is not a positive number. Try again:");
                }
                numberOfRolls = 1;
            }
            if (splitDice[1] != "")
            {
                if (splitDice[1].Contains('*') || splitDice[1].Contains('/'))
                {
                    throw new ArgumentException($"You cannot use a divide or multiply sign. Try again:");
                }
                try
                {

                    diceSides = Convert.ToInt32(splitDice[1]);

                }
                catch (Exception e)
                {
                    throw new ArgumentException($"{splitDice[1]} is not an integer. Try again:");
                }
                if (diceSides == 0)
                {
                    throw new ArgumentException($"A dice cannot have 0 sides! Try again:");
                }
            }



            if (Convert.ToInt32(splitDice[1]) > 0)
            {
                diceSides = Convert.ToInt32(splitDice[1]);
                if (splitDice[0] == "")
                {
                    numberOfRolls = 1;
                }
                else
                {
                    numberOfRolls = Convert.ToInt32(splitDice[0]);
                }
                if (splitDice.Length > 1)
                {
                    foreach (char c in diceNotation)
                    {
                        if (c == '+')
                        {
                            fixedBonus = Convert.ToInt32(splitDice[2]);
                        }
                        else if (c == '-')
                        {
                            fixedBonus = -Convert.ToInt32(splitDice[2]);
                        }
                    }
                }
                else
                {
                    fixedBonus = 0;
                }

            }


            finalScore = DiceRoll(numberOfRolls, diceSides, fixedBonus);



            return finalScore;
        }
        /*static int DiceRoll(string diceNotation)
        {

            char[] modifier = new char[] { 'd', '+', '-' };
            string[] splitDice = diceNotation.Split(modifier);
            char[] diceChar = diceNotation.ToCharArray();

            int numberOfRolls = 0;
            int diceSides = 0;
            bool inputValid = false;
            int fixedBonus = 0;
            int finalScore = 0;
            int tryCounter = 0;
            try
            {
                tryCounter = 0;
                if (splitDice[0] != "")
                {
                    numberOfRolls = Int32.Parse(splitDice[0]);
                    if (numberOfRolls < 0)
                    {
                        throw new IndexOutOfRangeException($"The number of dice ({splitDice[0]}) has to be positive");

                    }
                }
                
                tryCounter++;
                diceSides = Int32.Parse(splitDice[1]);
                tryCounter += 2;
                if (splitDice.Length == 3)
                {
                    fixedBonus = Int32.Parse(splitDice[2]);
                }
                inputValid = true;
            }
            catch (Exception)
            {
                inputValid = false;
                errorMessage = splitDice[tryCounter] + " is not an integer";
            }

            if (inputValid)
            {
                if (Convert.ToInt32(splitDice[1]) > 0)
                {
                    diceSides = Convert.ToInt32(splitDice[1]);
                    if (splitDice[0] == "")
                    {
                        numberOfRolls = 1;
                    }
                    else
                    {
                        numberOfRolls = Convert.ToInt32(splitDice[0]);
                    }
                    if (splitDice.Length > 1)
                    {
                        foreach (char c in diceChar)
                        {
                            if (c == '+')
                            {
                                fixedBonus = Convert.ToInt32(splitDice[2]);
                            }
                            else if (c == '-')
                            {
                                fixedBonus = -Convert.ToInt32(splitDice[2]);
                            }
                        }
                    }
                    else
                    {
                        fixedBonus = 0;
                    }

                }


                finalScore = DiceRoll(numberOfRolls, diceSides, fixedBonus);
            }


            return finalScore;
        }*/

        static bool IsStandardDiceNotation(string text)
        {
            char[] diceChar = text.ToCharArray();
            bool isTrue = false;
            foreach (var c in diceChar)
            {
                if (c == 'd')
                {
                    isTrue = true;
                }
            }
            return isTrue;
        }



    }
}
