using System;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BowlingValley
{
    class Program
    {

        struct Pin
        {
            public int Number;
        }

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

            

            List<Pin> displayPinsOG = new List<Pin> {};

            for (int i = 0; i < totalFrames; i++)
            {
                Pin pin = new Pin();
                pin.Number = i + 1;
                displayPinsOG.Add(pin);
            }
            List<Pin> displayPins = new List<Pin> (displayPinsOG);
            

            int[] pointsGained = new int[totalFrames];
            int[] frameScores = new int[totalFrames];
            bool isStrike = false;
            bool isSpare = false;
            bool isStrike2 = false;

            int[][] knockedPins = new int[totalFrames][];

            for (int i = 0; i < knockedPins.Length; i++)
            {
                if (i == 9)
                {
                    int firstRoll = random.Next(0, 11);
                    int secondRoll = random.Next(0, 11 - firstRoll);

                    if (firstRoll == 10)
                    {
                        secondRoll = random.Next(0, 11);
                    }

                    if (firstRoll == 10 || secondRoll == 10 || secondRoll + firstRoll == 10)
                    {
                        int thirdRoll = random.Next(0, 11);
                        knockedPins[i] = new int[] { firstRoll, secondRoll, thirdRoll };

                        pointsGained[i] += thirdRoll;
                    }
                    else
                    {
                        knockedPins[i] = new int[] { firstRoll, secondRoll };


                    }
                    pointsGained[i] += firstRoll;
                    pointsGained[i] += secondRoll;


                }
                else
                {
                    int firstRoll = random.Next(0, 11);
                    int secondRoll = random.Next(0, 11 - firstRoll);
                    pointsGained[i] += firstRoll;
                    pointsGained[i] += secondRoll;
                    if (isStrike2)
                    {
                        if (i + 1 < knockedPins.Length)
                        {
                            frameScores[i - 2] += firstRoll;
                            pointsGained[i - 2] += firstRoll;
                        }
                        isStrike2 = false;
                    }
                    if (isStrike)
                    {
                        frameScores[i - 1] += firstRoll;
                        pointsGained[i - 1] += firstRoll;
                        isStrike2 = true;
                        isStrike = false;
                    }

                    if (isSpare)
                    {
                        frameScores[i - 1] += firstRoll;
                        pointsGained[i - 1] += firstRoll;
                        isSpare = false;
                    }
                    if (firstRoll == 10)
                    {

                        knockedPins[i] = new int[] { firstRoll };

                        if (i < knockedPins.Length)
                        {
                            isStrike = true;
                        }
                    }
                    else
                    {
                        knockedPins[i] = new int[] { firstRoll, secondRoll };
                        if (firstRoll + secondRoll == 10)
                        {

                            isSpare = true;

                        }
                    }
                }
                if (i > 0)
                {
                    frameScores[i] = pointsGained[i] + frameScores[i - 1];
                }
                else
                {
                    frameScores[i] = pointsGained[i];
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
                        Console.Write(DrawScores(scoreFrame[y+5],-1, ' '));
                    }
                    else
                    {
                        Console.Write(DrawScores(scoreFrame[y+10],-1,' '));
                    }
                }
                Console.WriteLine();
            }
            int inputCounter = 0;
            int frame = 0;
            Console.WriteLine();
            Console.ForegroundColor = numberColor;
            Console.WriteLine("Press enter to roll!");
            Console.WriteLine();
            
            while (frame < 10)
            {
                //Display Frame here
                Console.ForegroundColor = frameColor;
                Console.WriteLine(FrameDisplay(frame));
                Console.ForegroundColor = numberColor;
                displayPins = new List<Pin>(displayPinsOG);
                KnockAndDisplayPins(displayPins);
                for (int i = 0; i < 3; i++)
                {
                    
                    ConsoleKeyInfo input = Console.ReadKey();
                    
                    if (input.Key == ConsoleKey.Enter)
                    {
                        switch (i)
                        {
                            case 0:
                                for (int pin = 0; pin < knockedPins[frame][0]; pin++)
                                {
                                    int randomPin = random.Next(0,displayPins.Count);
                                    displayPins.RemoveAt(randomPin);
                                    KnockAndDisplayPins(displayPins);
                                }
                                AddNumber('?', frame, knockedPins[frame][0]);
                                break;
                            case 1:

                                if (IsStrike(knockedPins[frame][0]))
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
                                        AddNumber(scoreChar, frame-1, frameScores[frame - 1]);
                                    }
                                }
                                if (!IsStrike(knockedPins[frame][0]) && !IsSpare(knockedPins[frame][1], knockedPins[frame][0]))
                                {
                                    foreach (char scoreChar in new string(":;-"))
                                    {
                                        AddNumber(scoreChar, frame, frameScores[frame]);
                                    }
                                }
                                break;
                            case 2:
                                if (frame == totalFrames - 1)
                                {
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

            
/*            for (int frame = 0; frame < totalFrames; frame++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Frame {frame+1}");
                int rollCounter = 0;
                int knockedPinsTotal = 0;
                foreach (int roll in knockedPins[frame])
                {
                    rollCounter++;
                    bool enterNotPressed = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Press enter to roll!");
                    while (enterNotPressed)
                    {
                        ConsoleKeyInfo input = Console.ReadKey();
                        if (input.Key == ConsoleKey.Enter)
                        {
                            
                            enterNotPressed = false;
                        }
                    }
                    knockedPinsTotal += roll;
                }
                
            }

            for (int x = 0; x < 5; x++)
            {
                for (int i = 0; i < totalFrames; i++)
                {
                    if (i == 0)
                    {
                        if (knockedPins[i].Length >= 2)
                        {
                            Console.Write(DrawScores(scoreFrame[x], frameScores[i], GetChar(knockedPins[i][0]), GetChar(knockedPins[i][1], 1, knockedPins[i][0])));
                        }
                        else
                        {
                            Console.Write(DrawScores(scoreFrame[x], frameScores[i], GetChar(knockedPins[i][0])));
                        }
                    }
                    else if (i < totalFrames - 1)
                    {
                        if (knockedPins[i].Length >= 2)
                        {
                            Console.Write(DrawScores(scoreFrame[x + 5], frameScores[i], GetChar(knockedPins[i][0]), GetChar(knockedPins[i][1], 1, knockedPins[i][0])));
                        }
                        else
                        {
                            Console.Write(DrawScores(scoreFrame[x + 5], frameScores[i], GetChar(knockedPins[i][0])));
                        }
                    }
                    else
                    {
                        if (knockedPins[i].Length > 2)
                        {
                            if (knockedPins[i][0] + knockedPins[i][1] == 10)
                            {
                                Console.Write(DrawScores(scoreFrame[x + 10], frameScores[i], GetChar(knockedPins[i][0]), GetChar(knockedPins[i][1], 1, knockedPins[i][0]), GetChar(knockedPins[i][2], 2, 0)));
                            }
                            else
                            {
                                Console.Write(DrawScores(scoreFrame[x + 10], frameScores[i], GetChar(knockedPins[i][0]), GetChar(knockedPins[i][1], 1, knockedPins[i][0]), GetChar(knockedPins[i][2], 2, knockedPins[i][1])));
                            }
                            
                        }
                        else
                        {
                            Console.Write(DrawScores(scoreFrame[x + 10], frameScores[i], GetChar(knockedPins[i][0]), GetChar(knockedPins[i][1], 1, knockedPins[i][0])));
                        }
                        
                    }
                }
                Console.WriteLine();
                
            }
            Console.ForegroundColor = ConsoleColor.White;*/
        }

        /*  foreach (string frame in scoreFrame)
          {
              string newFrame = frame;

              for (int i = 1; i < totalFrames; i++)
              {
                  if (scoreFrame[1] == frame)
                  {

                      int score1 = random.Next(0, 11);
                      char char1Score = score1.ToString()[0];
                      if (score1 == 10)
                      {
                          newFrame = frame.Replace('?', 'X');
                          newFrame = newFrame.Replace('!', ' ');
                      }
                      else
                      {
                          int score2 = random.Next(0, 11 - score1);
                          char char2Score = score2.ToString()[0];

                          if ((score2 == 9 && score1 == 1) || (score1 == 9 && score2 == 1) || (score1 + score2 == 10))
                          {
                              newFrame = frame.Replace('?', char1Score);
                              newFrame = newFrame.Replace('!', '/');
                          }
                          else
                          {
                              newFrame = frame.Replace('?', char1Score);
                              newFrame = newFrame.Replace('!', char2Score);
                          }
                      }
                  }
                  if (i > 1)
                  {
                      Console.Write(newFrame.Substring(1));
                  }
                  else
                  {
                      Console.Write(newFrame);
                  }

              }
              Console.WriteLine();
          }*/




        static string DrawScores(string line, int frameScore, char firstRoll, char secondRoll = ' ', char thirdRoll = ' ')
        {
            string newLine = line.Replace('?', firstRoll);
            
            if (firstRoll == 'X')
            {
                if (line.Contains('#'))
                {
                    newLine = newLine.Replace('!',secondRoll);
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

        static char GetChar(int roll, int rollCounter = 0, int spareCounter = 0)
        {
            rollCounter++;
            char display = ' ';
            spareCounter += roll;
            if (roll == 0)
            {
                display = '-';
            }
            else
            {
                display = roll.ToString()[0];

            }

            if (spareCounter == 10)
            {
                display = '/';

            }
            if (roll == 10)
            {
                display = 'X';

            }
            return display;
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
                    Console.SetCursorPosition(6 * 9+ 7, displayYStart);
                    break;
                case ':':
                    if (number > 99)
                    {
                        display = number.ToString()[0];

                    }
                    Console.SetCursorPosition(6 * frame + 2, displayYStart+2);
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
                    Console.SetCursorPosition(6 * frame + 3, displayYStart+2);
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
                    Console.SetCursorPosition(6 * frame + 4, displayYStart+2);
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

        static void KnockAndDisplayPins(List<Pin> knockedPins)
        {
            char pinSymbol = 'O';
            string spacing = "   ";
            string[] rows = new string[4];
            Console.SetCursorPosition(0, 7);
            for (int i = 0; i < 7; i++)
            {
                Console.Write("                    ");
                Console.WriteLine();
            }
            rows[1] = "  ";
            rows[2] = $" {spacing}";
            Console.SetCursorPosition(0, 7);
            foreach (Pin pin in knockedPins)
            {
                if (pin.Number >= 7)
                {
                    rows[0] += pinSymbol + spacing;
                }
                else if (pin.Number >= 4 && pin.Number < 7)
                {
                    rows[1] += pinSymbol + spacing;
                }
                else if (pin.Number == 2 || pin.Number == 3)
                {
                    rows[2] += pinSymbol + spacing;
                }
                else if (pin.Number == 1)
                {
                    rows[3] += spacing + spacing + pinSymbol;
                }
                
                
                
            }
            Console.ForegroundColor = pinColor;
            foreach (string row in rows)
            {
                Console.WriteLine(row);
            }
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
            finalString += $"FRAME {frame+1}";
            return finalString;
        }
    }
}
