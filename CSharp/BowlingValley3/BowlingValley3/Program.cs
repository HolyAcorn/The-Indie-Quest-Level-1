using System;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BowlingValley
{
    class Program
    {



        static ConsoleColor displayColor = ConsoleColor.DarkGray;
        static ConsoleColor numberColor = ConsoleColor.Cyan;
        static ConsoleColor frameColor = ConsoleColor.Magenta;
        static ConsoleColor pinColor = ConsoleColor.White;

        static void Main(string[] args)
        {


            string scoreFile = "ScoreFrame.txt";
            string[] scoreFrame = File.ReadAllLines(scoreFile);
            string scoreFrameString = File.ReadAllText(scoreFile);
            string frame1Regex = @"(┌.*\n.*([?]).(!)\n.*\n.*(:)?(;)(-)\s+.*\n)";
            string frame2Regex = @"(┬.*\n.*([?]).(!)\n.*\n.*(:)(;)(-)\s+.*\n)";
            string frame3Regex = @"(┬.*\n.*([?]).(!).(#).\n.*\n.*(:)(;)(-)\s+.*\n.*)";

            Match frame1Match = Regex.Match(scoreFrameString, frame1Regex);
            Match frame2Match = Regex.Match(scoreFrameString, frame2Regex);
            Match frame3Match = Regex.Match(scoreFrameString, frame3Regex);

            Random random = new Random();
            int totalFrames = 10;



            List<int> displayPinsOG = new List<int> {7,8,9,10,4,5,6,2,3,1};

            List<int> displayPins = new List<int>(displayPinsOG);


            int[] pointsGained = new int[totalFrames];
            int[] frameScores = new int[totalFrames];
            bool isStrike = false;
            bool isSpare = false;
            bool isStrike2 = false;

            int[][] knockedPins = new int[totalFrames][];

            for (int i = 0; i < knockedPins.Length; i++)
            {
                


            }


            Console.ForegroundColor = displayColor;
            for (int y = 0; y < 5; y++)
            {
                for (int i = 0; i < totalFrames; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(DrawScores(scoreFrame[y], -1, ' '));
                    }
                    else if (i < 9)
                    {
                        Console.Write(DrawScores(scoreFrame[y + 5], -1, ' '));
                    }
                    else
                    {
                        Console.Write(DrawScores(scoreFrame[y + 10], -1, ' '));
                    }
                }
                Console.WriteLine();
            }
            int inputCounter = 0;
            int frame = 0;
            Console.WriteLine();
            Console.WriteLine("1 2 3 4 5 6 7");
            Console.ForegroundColor = numberColor;



            while (frame < 10)
            {

                Console.ForegroundColor = frameColor;
                Console.WriteLine(FrameDisplay(frame));
                Console.ForegroundColor = numberColor;
                displayPins = new List<int>(displayPinsOG);
                KnockAndDisplayPins(displayPins);
                bool takeInput = true;
                for (int i = 0; i < 3; i++)
                {
                    int input = 0;
                    ConsoleKeyInfo enterInput = new ConsoleKeyInfo();

                    if (takeInput)
                    {
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine("Enter where to roll the ball (1-7):");
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine("Press enter to continue:                ");
                        enterInput = Console.ReadKey();
                    }


                    if (enterInput.Key == ConsoleKey.Enter || (input < 8 && input > 0))
                    {

                        int firstRoll = GetKnockAngle(input, displayPins);

                        switch (i)
                        {
                            case 0:
                                // for (int pin = 0; pin < knockedPins[frame][0]; pin++)
                                //{
                                int hitPin = -1;
                                foreach (int pin in displayPins)
                                {
                                    if (pin == firstRoll)
                                    {
                                        hitPin = displayPins.IndexOf(pin);
                                    }
                                }
                                    displayPins.RemoveAt(hitPin);
                                    KnockAndDisplayPins(displayPins);
                                //}
                                AddNumber('?', frame, hitPin);
                                takeInput = true;
                                break;
                            case 1:
                                hitPin = -1;
                                foreach (int pin in displayPins)
                                {
                                    if (pin == firstRoll)
                                    {
                                        hitPin = displayPins.IndexOf(pin);
                                    }
                                }
                                if (!IsPinStanding(firstRoll,displayPins))
                                {
                                    hitPin = 0;
                                }
                                displayPins.Remove(hitPin);
                                KnockAndDisplayPins(displayPins);
                                //}
                                AddNumber('!', frame, hitPin);
                                takeInput = true;
                                /*if (IsStrike(knockedPins[frame][0]))
                                {
                                    AddNumber('!', frame, 0, knockedPins[frame][0]);
                                }
                                else
                                {
                                    for (int pin = 0; pin < knockedPins[frame][1]; pin++)
                                    {
                                        int randomPin = random.Next(0, displayPins.Count);
                                        displayPins.RemoveAt(randomPin);
                                        KnockAndDisplayPins(displayPins);
                                    }
                                    AddNumber('!', frame, knockedPins[frame][1], knockedPins[frame][0]);
                                }

                                if (frame != 0 && (IsStrike(knockedPins[frame - 1][0]) || IsSpare(knockedPins[frame - 1][1], knockedPins[frame - 1][0])))
                                {
                                    foreach (char scoreChar in new string(":;-"))
                                    {
                                        AddNumber(scoreChar, frame - 1, frameScores[frame - 1]);
                                    }
                                }
                                if (!IsStrike(knockedPins[frame][0]) && !IsSpare(knockedPins[frame][1], knockedPins[frame][0]))
                                {
                                    foreach (char scoreChar in new string(":;-"))
                                    {
                                        AddNumber(scoreChar, frame, frameScores[frame]);
                                    }
                                }
                                if (frame > 8)
                                {
                                    takeInput = true;
                                }
                                else
                                {
                                    takeInput = false;
                                }*/
                                break;
                            case 2:
                                if (frame == totalFrames - 1)
                                {
                                    displayPins = new List<int>(displayPinsOG);
                                    KnockAndDisplayPins(displayPins);
                                    if (IsStrike(knockedPins[frame][0]))
                                    {

                                        AddNumber('#', frame, knockedPins[frame][2]);
                                    }

                                    else
                                    {
                                        if (IsSpare(knockedPins[frame][0], knockedPins[frame][1]))
                                        {
                                            for (int pin = 0; pin < knockedPins[frame][2]; pin++)
                                            {
                                                int randomPin = random.Next(0, displayPins.Count);
                                                displayPins.RemoveAt(randomPin);
                                                KnockAndDisplayPins(displayPins);
                                            }

                                            AddNumber('#', frame, knockedPins[frame][2]);
                                        }

                                    }
                                    foreach (char scoreChar in new string(":;-"))
                                    {
                                        AddNumber(scoreChar, frame, frameScores[frame]);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                frame++;
            }
            Console.WriteLine();
        }

        static string DrawScores(string line, int frameScore, char firstRoll, char secondRoll = ' ', char thirdRoll = ' ')
        {
            string newLine = line.Replace('?', firstRoll);

            if (firstRoll == 'X')
            {
                if (line.Contains('#'))
                {
                    newLine = newLine.Replace('!', secondRoll);
                }
                else
                {
                    newLine = newLine.Replace('!', ' ');
                }


            }

            else
            {
                newLine = newLine.Replace('!', secondRoll);
            }
            if (line.Contains('#'))
            {
                if (secondRoll == 'X' || secondRoll == '/' || firstRoll == 'X')
                {
                    newLine = newLine.Replace('#', thirdRoll);
                }
                else
                {
                    newLine = newLine.Replace('#', ' ');
                }
            }
            if (frameScore == -1)
            {
                newLine = newLine.Replace(':', ' ');
                newLine = newLine.Replace(';', ' ');
                newLine = newLine.Replace('-', ' ');
            }
            else
            {
                if (frameScore > 99)
                {
                    newLine = newLine.Replace(':', frameScore.ToString()[0]);
                    newLine = newLine.Replace(';', frameScore.ToString()[1]);
                    newLine = newLine.Replace('-', frameScore.ToString()[2]);
                }
                else
                {
                    newLine = newLine.Replace(':', ' ');
                }
                if (frameScore > 9 && frameScore < 100)
                {
                    newLine = newLine.Replace(';', frameScore.ToString()[0]);
                    newLine = newLine.Replace('-', frameScore.ToString()[1]);


                }
                else
                {
                    newLine = newLine.Replace(';', ' ');
                    newLine = newLine.Replace('-', frameScore.ToString()[0]);
                }
            }

            return newLine;

        }

        static void AddNumber(char writer, int frame, int number, int previousNumber = 0)

        {
            char display = ' ';
            int spareCounter = previousNumber;
            int displayYStart = 1;
            switch (writer)
            {
                case '?':
                    spareCounter += number;
                    if (number == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = number.ToString()[0];

                    }
                    if (IsStrike(number))
                    {
                        display = 'X';

                    }

                    Console.SetCursorPosition(6 * frame + 3, displayYStart);

                    break;
                case '!':


                    if (IsStrike(previousNumber) && frame != 9)
                    {
                        display = ' ';
                    }

                    else
                    {
                        spareCounter += number;
                        if (IsSpare(number, previousNumber))
                        {
                            display = '/';

                        }
                        else
                        {
                            if (number == 0)
                            {
                                display = '-';
                            }
                            else
                            {
                                display = number.ToString()[0];
                            }
                        }
                    }


                    Console.SetCursorPosition(6 * frame + 5, displayYStart);
                    break;
                case '#':
                    if (IsStrike(previousNumber))
                    {
                        display = ' ';
                    }
                    spareCounter += number;
                    if (number == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = number.ToString()[0];
                    }
                    if (IsSpare(number, previousNumber))
                    {
                        display = '/';
                    }
                    Console.SetCursorPosition(6 * 9 + 7, displayYStart);
                    break;
                case ':':
                    if (number > 99)
                    {
                        display = number.ToString()[0];

                    }
                    Console.SetCursorPosition(6 * frame + 2, displayYStart + 2);
                    break;
                case ';':
                    if (number > 9)
                    {
                        if (number < 99)
                        {
                            display = number.ToString()[0];
                        }
                        else
                        {
                            display = number.ToString()[1];
                        }
                    }
                    Console.SetCursorPosition(6 * frame + 3, displayYStart + 2);
                    break;
                case '-':
                    if (number > 9)
                    {
                        if (number > 99)
                        {
                            display = number.ToString()[2];
                        }
                        else
                        {
                            display = number.ToString()[1];
                        }
                    }
                    else
                    {
                        display = number.ToString()[0];
                    }
                    Console.SetCursorPosition(6 * frame + 4, displayYStart + 2);
                    break;
                default:
                    break;
            }
            Console.ForegroundColor = numberColor;
            Console.Write(display);
            Console.SetCursorPosition(0, 7);
        }

        static bool IsSpare(int number1, int number2)
        {
            bool isSpare = false;
            if (number1 + number2 == 10)
            {
                isSpare = true;
            }
            return isSpare;
        }

        static bool IsStrike(int number)
        {
            bool isStrike = false;
            if (number == 10)
            {
                isStrike = true;
            }
            return isStrike;
        }

        static void KnockAndDisplayPins(List<int> knockedPins)
        {
            char pinSymbol = 'O';
            string spacing = "   ";
            string[] rows = new string[] { $" {spacing} {spacing} {spacing} ", $"   {spacing} {spacing} ", $" {spacing} {spacing} ", $"{spacing}{spacing} " };
            Console.SetCursorPosition(0, 7);
            for (int i = 0; i < 7; i++)
            {
                Console.Write("                    ");
                Console.WriteLine();
            }
            foreach (string row in rows)
            {
                Console.WriteLine(row);
            }

            Console.SetCursorPosition(0, 7);
            foreach (int pin in knockedPins)
            {
                int left = 0;
                int top = 7;
                switch (pin)
                {
                    case 7:
                        break;
                    case 8:
                        left = 4;
                        break;
                    case 9:
                        left = 8;
                        break;
                    case 10:
                        left = 12;
                        break;
                    case 4:
                        top = 8;
                        left = 2;
                        break;
                    case 5:
                        top = 8;
                        left = 6;
                        break;
                    case 6:
                        top = 8;
                        left = 10;
                        break;
                    case 2:
                        top = 9;
                        left = 4;
                        break;
                    case 3:
                        top = 9;
                        left = 8;
                        break;
                    case 1:
                        top = 10;
                        left = 6;
                        break;
                }
                Console.SetCursorPosition(left, top);
                Console.Write(pinSymbol);



            }
            Console.ForegroundColor = pinColor;
            
            //asd
            Console.ForegroundColor = numberColor;

        }
        static string FrameDisplay(int frame)
        {
            Console.SetCursorPosition(0, 5);
            string spacing = "      ";
            string finalString = "";
            for (int i = 0; i < frame; i++)
            {
                finalString += spacing;
            }
            finalString += $"FRAME {frame + 1}";
            return finalString;
        }

        static int GetKnockAngle(int input, List<int> standingPins)
        {
            int choice = 0;
            switch (input)
            {
                case 1:
                    if (IsPinStanding(7, standingPins))
                    {
                        choice = 7;
                    }
                    else
                    {
                        choice = 0;
                    }
                    break;
                case 2:
                    if (IsPinStanding(4, standingPins))
                    {
                        choice = 4;
                    }
                    else
                    {
                        choice = 0;
                    }
                    break;
                case 3:
                    if (IsPinStanding(2, standingPins))
                    {
                        choice = 2;
                    }
                    else
                    {
                        if (IsPinStanding(8, standingPins))
                        {
                            choice = 8;
                        }
                        else
                        {
                            choice = 0;
                        }
                    }
                    break;
                case 4:
                    if (IsPinStanding(1, standingPins))
                    {
                        choice = 1;
                    }
                    else
                    {
                        if (IsPinStanding(5, standingPins))
                        {
                            choice = 5;
                        }
                        else
                        {
                            choice = 0;
                        }
                    }
                    break;
                case 5:
                    if (IsPinStanding(3, standingPins))
                    {
                        choice = 3;
                    }
                    else
                    {
                        if (IsPinStanding(9, standingPins))
                        {
                            choice = 9;
                        }
                        else
                        {
                            choice = 0;
                        }
                    }
                    break;
                case 6:
                    if (IsPinStanding(6, standingPins))
                    {
                        choice = 6;
                    }
                    else
                    {
                        choice = 0;
                    }
                    break;
                case 7:
                    if (IsPinStanding(10, standingPins))
                    {
                        choice = 10;
                    }
                    else
                    {
                        choice = 0;
                    }
                    break;

            }
            return choice;

            

        }
        static bool IsPinStanding(int input, List<int> standingPins)
        {
            bool isTrue = false;
            foreach (int pin in standingPins)
            {
                if (pin == input)
                {
                    isTrue = true;
                    break;
                }
            }
            return isTrue;
        }
    }
}

/* 
 if (frame == 9)
                        {
                            int firstRoll = GetKnockAngle(input, displayPins);
                            int secondRoll = GetKnockAngle(input, displayPins);

                            if (firstRoll == 10)
                            {
                                secondRoll = random.Next(0, 11);
                            }

                            if (firstRoll == 10 || secondRoll == 10 || secondRoll + firstRoll == 10)
                            {
                                int thirdRoll = random.Next(0, 11);
                                knockedPins[frame] = new int[] { firstRoll, secondRoll, thirdRoll };

                                pointsGained[frame] += thirdRoll;
                            }
                            else
                            {
                                knockedPins[frame] = new int[] { firstRoll, secondRoll };


                            }
                            pointsGained[frame] += firstRoll;
                            pointsGained[frame] += secondRoll;


                        }
                        else
                        {
                            int firstRoll = GetKnockAngle(input, displayPins);
                            int secondRoll = GetKnockAngle(input, displayPins);
                            pointsGained[frame] += firstRoll;
                            pointsGained[frame] += secondRoll;
                            if (isStrike2)
                            {
                                if (frame + 1 < knockedPins.Length)
                                {
                                    frameScores[frame - 2] += firstRoll;
                                    pointsGained[frame - 2] += firstRoll;
                                }
                                isStrike2 = false;
                            }
                            if (isStrike)
                            {
                                frameScores[frame - 1] += firstRoll;
                                pointsGained[frame - 1] += firstRoll;
                                isStrike2 = true;
                                isStrike = false;
                            }

                            if (isSpare)
                            {
                                frameScores[frame - 1] += firstRoll;
                                pointsGained[frame - 1] += firstRoll;
                                isSpare = false;
                            }
                            if (firstRoll == 10)
                            {

                                knockedPins[frame] = new int[] { firstRoll };

                                if (frame < knockedPins.Length)
                                {
                                    isStrike = true;
                                }
                            }
                            else
                            {
                                knockedPins[frame] = new int[] { firstRoll, secondRoll };
                                if (firstRoll + secondRoll == 10)
                                {

                                    isSpare = true;

                                }
                            }
                        }
                        if (frame > 0)
                        {
                            frameScores[frame] = pointsGained[frame] + frameScores[frame - 1];
                        }
                        else
                        {
                            frameScores[frame] = pointsGained[frame];
                        }
 */