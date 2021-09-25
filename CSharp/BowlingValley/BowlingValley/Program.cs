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

            foreach (string frame in scoreFrame)
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

                            if ((score2 == 9 && score1 == 1) || (score1 == 9 && score2 == 1) ||(score1 + score2 == 10))
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
            }


        }
    }
}
