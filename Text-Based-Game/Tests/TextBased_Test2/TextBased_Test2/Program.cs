using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace TextBased_Test2
{
    public class Program
    {
        static string titleFile = "Title.txt";
        static string tilesFile = "Tiles.txt";
        static string[] title = File.ReadAllLines(titleFile);
        static string[] tilesAray = File.ReadAllLines(tilesFile);
        static ConsoleColor[,] titleColorData = new ConsoleColor[title.Length, title[1].Length];
        static char[,] titleCharData = new char[title.Length, title[1].Length];

        static string flagFile = "Flag.txt";
        static string[] flag = File.ReadAllLines(flagFile);
        static char[,] flagCharData = new char[flag.Length, flag[1].Length];
        static ConsoleColor[,] flagColorData = new ConsoleColor[flag.Length, flag[1].Length];

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
  
                           Console.ForegroundColor = titleTextColor;
                            

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
                            Console.ForegroundColor = titleTextColor;
                        }
                    }

                        Console.Write(c);
                    

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
