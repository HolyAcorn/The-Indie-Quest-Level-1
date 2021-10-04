using System;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BowlingValley
{
    class Program
    {

        static Random random = new Random();

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

            
            int totalFrames = 10;



            List<int> displayPinsOG = new List<int> {7,8,9,10,4,5,6,2,3,1};

            List<int> displayPins = new List<int>(displayPinsOG);

            int[][] knockedPins = new int[totalFrames][];

            for (int i = 0; i < knockedPins.Length; i++)
            {
                if (i == 9)
                {
                    knockedPins[i] = new int[3];
                }
                else
                {
                    knockedPins[i] = new int[2];
                }


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
                DisplayPins(displayPins);
                for (int rollIndex = 0; rollIndex < 3; rollIndex++)
                {
                    int laneInput = 0;
                    ConsoleKeyInfo enterInput = new ConsoleKeyInfo();

                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Enter where to roll the ball (1-7):");
                    laneInput = Convert.ToInt32(Console.ReadLine());





                    if (laneInput < 8 && laneInput > 0)
                    {

                        knockedPins[frame][rollIndex] += KnockPins(displayPins, laneInput);
                        UpdateFrameScores(knockedPins, frame, rollIndex+1);
                        DisplayPins(displayPins);



                        switch (rollIndex)
                        {
                            case 0:
                                AddNumber('?', frame, knockedPins[frame][0]);

                                break;
                            case 1:
                                AddNumber('!', frame, knockedPins[frame][1], knockedPins[frame][0]);


                                break;
                            case 2:
                                AddNumber('#', frame, knockedPins[frame][2], knockedPins[frame][1]);
                                break;
                            default:
                                break;
                        }

                    }

                    bool firstRollStrike = IsStrike(knockedPins[frame][0]);
                    bool inLastFrame = frame == 9;
                    bool secondRollSpare = IsSpare(knockedPins[frame][0], knockedPins[frame][1]); ;
                    bool secondRollStrike = IsStrike(knockedPins[frame][1]);

                    if (firstRollStrike && rollIndex == 0 || secondRollSpare || secondRollStrike || (!inLastFrame && rollIndex == 1))
                    {
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine("Press enter to continue:                ");
                        Console.ReadKey();
                        displayPins = new List<int>(displayPinsOG);
                        DisplayPins(displayPins);
                    }

                    if (!inLastFrame && (firstRollStrike || rollIndex == 1) || inLastFrame && !firstRollStrike && !secondRollSpare && rollIndex > 0)
                    {
                        break;
                    }
                }

                frame++;
            }
            Console.WriteLine();
        }

        private static void DisplayFrameScore(int frame, int score)
        {
            for (int number = 0; number < 3; number++)
            {
                char[] pointChars = new char[] { ':', ';', '-' };

                AddNumber(pointChars[number], frame, score);
            }
        }

        private static int KnockPins(List<int> displayPins, int laneInput)
        {
            
            int knockedPinsCount = 0;
            int firstRollKnockedPinNumber = DetermineKnockedPin(laneInput, displayPins);
            if (firstRollKnockedPinNumber > 0)
            {
                knockedPinsCount++;
                displayPins.Remove(firstRollKnockedPinNumber);
                for (int i = 0; i < 2; i++)
                {
                    int chance = random.Next(100);
                    if (chance < 40)
                    {
                        laneInput--;
                    }
                    else if (chance < 80)
                    {
                        laneInput++;
                    }
                    knockedPinsCount += KnockPins(displayPins, laneInput);
                }
            }
            return knockedPinsCount;
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

        static void AddNumber(char writer, int frame, int knockedPins, int previousKnockedPins = 0)

        {
            char display = ' ';
            int spareCounter = previousKnockedPins;
            int displayYStart = 1;
            switch (writer)
            {
                case '?':
                    spareCounter += knockedPins;
                    if (knockedPins == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = knockedPins.ToString()[0];

                    }
                    if (IsStrike(knockedPins))
                    {
                        display = 'X';

                    }

                    Console.SetCursorPosition(6 * frame + 3, displayYStart);

                    break;
                case '!':


                    if (IsStrike(previousKnockedPins) && frame != 9)
                    {
                        display = ' ';
                    }

                    else
                    {
                        spareCounter += knockedPins;
                        if (IsSpare(knockedPins, previousKnockedPins))
                        {
                            display = '/';

                        }
                        else
                        {
                            if (knockedPins == 0)
                            {
                                display = '-';
                            }
                            else
                            {
                                display = knockedPins.ToString()[0];
                            }
                        }
                    }


                    Console.SetCursorPosition(6 * frame + 5, displayYStart);
                    break;
                case '#':
                    spareCounter += knockedPins;
                    if (knockedPins == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = knockedPins.ToString()[0];
                    }
                    if (IsSpare(knockedPins, previousKnockedPins))
                    {
                        display = '/';
                    }
                    if (IsStrike(knockedPins))
                    {
                        display = 'X';
                    }
                    Console.SetCursorPosition(6 * 9 + 7, displayYStart);
                    break;
                case ':':
                    if (knockedPins > 99)
                    {
                        display = knockedPins.ToString()[0];

                    }
                    Console.SetCursorPosition(6 * frame + 2, displayYStart + 2);
                    break;
                case ';':
                    if (knockedPins > 9)
                    {
                        if (knockedPins < 99)
                        {
                            display = knockedPins.ToString()[0];
                        }
                        else
                        {
                            display = knockedPins.ToString()[1];
                        }
                    }
                    Console.SetCursorPosition(6 * frame + 3, displayYStart + 2);
                    break;
                case '-':
                    if (knockedPins > 9)
                    {
                        if (knockedPins > 99)
                        {
                            display = knockedPins.ToString()[2];
                        }
                        else
                        {
                            display = knockedPins.ToString()[1];
                        }
                    }
                    else
                    {
                        display = knockedPins.ToString()[0];
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
            return number1 + number2 == 10 && number1 < 10;
        }

        static bool IsStrike(int number)
        {
            return number == 10;
        }

        static void DisplayPins(List<int> standingPins)
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
            foreach (int pin in standingPins)
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

        static int DetermineKnockedPin(int lane, List<int> standingPins)
        {
            int knockedPinNumber = 0;
            switch (lane)
            {
                case 1:
                    if (IsPinStanding(7, standingPins))
                    {
                        knockedPinNumber = 7;
                    }
                    else
                    {
                        knockedPinNumber = 0;
                    }
                    break;
                case 2:
                    if (IsPinStanding(4, standingPins))
                    {
                        knockedPinNumber = 4;
                    }
                    else
                    {
                        knockedPinNumber = 0;
                    }
                    break;
                case 3:
                    if (IsPinStanding(2, standingPins))
                    {
                        knockedPinNumber = 2;
                    }
                    else
                    {
                        if (IsPinStanding(8, standingPins))
                        {
                            knockedPinNumber = 8;
                        }
                        else
                        {
                            knockedPinNumber = 0;
                        }
                    }
                    break;
                case 4:
                    if (IsPinStanding(1, standingPins))
                    {
                        knockedPinNumber = 1;
                    }
                    else
                    {
                        if (IsPinStanding(5, standingPins))
                        {
                            knockedPinNumber = 5;
                        }
                        else
                        {
                            knockedPinNumber = 0;
                        }
                    }
                    break;
                case 5:
                    if (IsPinStanding(3, standingPins))
                    {
                        knockedPinNumber = 3;
                    }
                    else
                    {
                        if (IsPinStanding(9, standingPins))
                        {
                            knockedPinNumber = 9;
                        }
                        else
                        {
                            knockedPinNumber = 0;
                        }
                    }
                    break;
                case 6:
                    if (IsPinStanding(6, standingPins))
                    {
                        knockedPinNumber = 6;
                    }
                    else
                    {
                        knockedPinNumber = 0;
                    }
                    break;
                case 7:
                    if (IsPinStanding(10, standingPins))
                    {
                        knockedPinNumber = 10;
                    }
                    else
                    {
                        knockedPinNumber = 0;
                    }
                    break;


            }
            return knockedPinNumber;

            

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

        static void UpdateFrameScores(int[][] knockedPins, int frame, int rollsCompleted)
        {
            int[] pointsGained = new int[10];

            // Update scores
            for (int i = 0; i < 10; i++)
            {
                //Normal scores (1 point for each knocked pin)
                foreach (int knockedPin in knockedPins[i])
                {
                    pointsGained[i] += knockedPin;
                }

                // Spare scores (add first roll to previous (spare) frame)
                
                if (i > 0 &&  (frame > i || rollsCompleted > 0) && IsSpare(knockedPins[i - 1][0], knockedPins[i - 1][1]))
                {
                    pointsGained[i - 1] += knockedPins[i][0];
                }

                // First Strike score (adds first roll to previous (strike) frame)
                if (i > 0 && (frame > i || rollsCompleted > 0) && IsStrike(knockedPins[i - 1][0]))
                {
                    pointsGained[i - 1] += knockedPins[i][0];
                }

                // Second Strike score (adds second roll to previous (strike) frame,)
                if (i > 0 && (frame > i || rollsCompleted > 1) && IsStrike(knockedPins[i - 1][0]))
                {
                    pointsGained[i - 1] += knockedPins[i][1];
                }

                // (OR adds first roll to 2 frames ago if 2 strikes has happened))
                if (i > 1 && (frame > i || rollsCompleted > 0) && IsStrike(knockedPins[i - 1][0]) && IsStrike(knockedPins[i-2][0]))
                {
                    pointsGained[i - 2] += knockedPins[i][0];
                }


            }
            int frameScore = 0;

            // Write Scores
            for (int i = 0; i <= frame; i++)
            {
                frameScore += pointsGained[i];

                bool haveAllRollsEarlyFrames = i < frame ||rollsCompleted > 0 && IsStrike(knockedPins[i][0]) || rollsCompleted > 1;
                bool haveAllRollsTenthFrame = rollsCompleted == 2 && !IsStrike(knockedPins[i][0]) && !IsSpare(knockedPins[i][0], knockedPins[i][1]) || rollsCompleted == 3;
                bool haveAllRolls = i == 9 ? haveAllRollsTenthFrame : haveAllRollsEarlyFrames;

                bool waitingForSpare = IsSpare(knockedPins[i][0], knockedPins[i][1]) && i == frame;

                bool waitingForStrikeSameFrame = IsStrike(knockedPins[i][0]) && i == frame;
                bool waitingForStrikePreviousFrame = IsStrike(knockedPins[i][0]) && i == frame-1 && rollsCompleted < 2;
                bool waitingForeverStrikeFrame = i < 9 && IsStrike(knockedPins[i][0]) && i == frame - 2 && rollsCompleted < 1 && IsStrike(knockedPins[i+1][0]);

                bool waiting = waitingForSpare || waitingForeverStrikeFrame || waitingForStrikePreviousFrame || waitingForStrikeSameFrame;


                if (haveAllRolls && (!waiting && i < 9 ||  i == 9))
                {
                    DisplayFrameScore(i, frameScore);
                }
                
                
                
            }
            

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