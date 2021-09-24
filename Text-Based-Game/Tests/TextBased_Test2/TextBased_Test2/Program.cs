using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace TextBased_Test2
{
    class Program
    {
        static string filepath = "Title3.txt";
        static string[] title = File.ReadAllLines(filepath);
        static ConsoleColor[,] titleColorData = new ConsoleColor[title.Length, title[1].Length];
        static char[,] titleCharData = new char[title.Length, title[1].Length];
        static void Main(string[] args)
        {


            ConsoleColor borderColor = ConsoleColor.DarkYellow;
            ConsoleColor bgColor = ConsoleColor.DarkGray;
            ConsoleColor titleTextColor = ConsoleColor.Cyan;



            for (int i = 0; i < 6; i++)
            {
                Console.ForegroundColor = borderColor;

                int cIndex = 0;
                foreach (char c in title[i])
                {
                    Console.Write(c);
                    Thread.Sleep(0);
                    cIndex++;
                }
                Thread.Sleep(25);
                Console.WriteLine();
            }
            int edgeCounter = 1;
            for (int i = 6; i < 22; i++)
            {


                int cIndex = 1;
                foreach (char c in title[i])
                {


                    if (edgeCounter >= 2)
                    {
                        if (cIndex < 9)
                        {
                            Console.ForegroundColor = borderColor;
                            titleColorData[i, cIndex - 1] = borderColor;

                        }
                        else
                        {
                            if (c == 's' || c == '=')
                            {
                                Console.ForegroundColor = bgColor;
                            }
                            else
                            {
                                Console.ForegroundColor = titleTextColor;
                            }

                            if (edgeCounter >= 3)
                            {
                                if (cIndex > 102)
                                {
                                    Console.ForegroundColor = borderColor;
                                    edgeCounter = -1;
                                }

                            }
                            else
                            {
                                if (cIndex > 102)
                                {
                                    Console.ForegroundColor = borderColor;
                                }
                            }
                        }


                    }
                    else
                    {
                        if (cIndex < 8 || cIndex > 103)
                        {
                            Console.ForegroundColor = borderColor;
                        }
                        else
                        {
                            if (c == 's' || c == '=')
                            {
                                Console.ForegroundColor = bgColor;
                                title[i].Replace('s', '|');
                                title[i].Replace('=', '_');
                            }
                            else
                            {
                                Console.ForegroundColor = titleTextColor;
                            }
                        }
                    }


                    if (c == 's' || c == '=')
                    {
                        string tempStr = title[i];
                        tempStr = title[i].Replace('s', '|');
                        tempStr = tempStr.Replace('=', '_');
                        char tempC = tempStr.ToCharArray()[cIndex - 1];
                        Console.Write(tempC);
                    }
                    else
                    {
                        Console.Write(c);
                    }

                    Thread.Sleep(0);
                    cIndex++;
                }
                edgeCounter++;
                Thread.Sleep(25);
                Console.WriteLine();
            }
            for (int i = 22; i < 28; i++)
            {
                Console.ForegroundColor = borderColor;

                int cIndex = 0;
                foreach (char c in title[i])
                {
                    Console.Write(c);
                    Thread.Sleep(0);
                    cIndex++;
                }
                Thread.Sleep(25);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
