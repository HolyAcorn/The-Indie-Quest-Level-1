using System;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace BowlingValley
{
    class Program
    {
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

                    if (secondRoll == 10 || secondRoll + firstRoll == 10)
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
            AddScore('!', 9,knockedPins[2][0]);
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
        static void AddScore(char writer, int frame, int roll, int previousRoll = 0)
        {
            char display = ' ';
            int spareCounter = previousRoll;
            switch (writer)
            {
                case '?':
                    spareCounter += roll;
                    if (roll == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = roll.ToString()[0];

                    }
                    if (roll == 10)
                    {
                        display = 'X';

                    }

                    Console.SetCursorPosition(6 * frame + 3, 1);

                    break;
                case '!':
                    if (spareCounter == 10)
                    {
                        display = ' ';
                    }
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
                    Console.SetCursorPosition(6 * frame + 5, 1);
                    break;
                case '#':
                    break;
                case ':':
                    break;
                case ';':
                    break;
                case '-':
                    break;
                default:
                    break;
            }
            Console.Write(display);
            Console.SetCursorPosition(0, 7);
        }
    }
}
