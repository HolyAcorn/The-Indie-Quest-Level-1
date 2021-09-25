using System;
using System.IO;
using System.Threading;

namespace BowlingValley
{
    class Program
    {
        static void Main(string[] args)
        {


            string scoreFile = "ScoreFrame.txt";
            string[] scoreFrame = File.ReadAllLines(scoreFile);

            Random random = new Random();
            int totalFrames = 10;

            int[] pointsGained = new int[totalFrames];
            int[] frameScores = new int[totalFrames];

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
                    }
                    else
                    {
                        knockedPins[i] = new int[] { firstRoll, secondRoll };
                    }


                }
                else
                {
                    int firstRoll = random.Next(0, 11);
                    int secondRoll = random.Next(0, 11 - firstRoll);
                    if (firstRoll == 10)
                    {
                        knockedPins[i] = new int[] { firstRoll };
                    }
                    else
                    {
                        knockedPins[i] = new int[] { firstRoll, secondRoll };
                    }
                }

            }

            Console.WriteLine($"SIMULATING {totalFrames} BOWLING FRAMES ");
            Thread.Sleep(25);
            Console.Write(".");
            Thread.Sleep(25);
            Console.Write(".");
            Thread.Sleep(25);
            Console.Write(".");
            Console.WriteLine("DONE!");
            Console.WriteLine();
            Console.WriteLine("Results:");

            for (int frame = 0; frame < knockedPins.Length; frame++)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"FRAME {frame}");
                int spareCounter = 0;
                int rollCounter = 0;
                foreach (int roll in knockedPins[frame])
                {
                    rollCounter++;
                    char display = ' ';

                    if (roll == 0)
                    {
                        display = '-';
                    }
                    else
                    {
                        display = roll.ToString()[0];
                        pointsGained[frame] += roll;
                    }
                    spareCounter += roll;
                    if (spareCounter == 10)
                    {
                        display = '/';
                        pointsGained[frame] += roll;
                        if (frame < knockedPins.Length)
                        {
                            pointsGained[frame - 1] += roll;
                        }
                    }
                    if (roll == 10)
                    {
                        display = 'X';
                        pointsGained[frame] += roll;
                        if (frame < knockedPins.Length)
                        {
                            frameScores[frame + 1] += roll;
                            if (frame +1 < knockedPins.Length)
                            {
                                frameScores[frame + 2] += roll;
                            }
                            
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Roll {rollCounter}: {display}");

                }
                if (frame != 0)
                {
                    frameScores[frame] += frameScores[frame - 1];
                }
                frameScores[frame] += pointsGained[frame];
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Knocked pins = {spareCounter}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Points Gained = {pointsGained[frame]}");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Score = {frameScores[frame]}");
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


        }
    }
}
