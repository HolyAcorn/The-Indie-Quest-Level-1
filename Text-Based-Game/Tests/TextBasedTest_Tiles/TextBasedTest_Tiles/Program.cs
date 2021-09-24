using System;
using System.Collections.Generic;
using System.Threading;

namespace TextBasedTest_Tiles
{
    class Program
    {
        static void Main(string[] args)
        {



            int xLength = 10;
            int yLength = 5;
            List<string> tileset = DrawTiles(xLength, yLength);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            foreach (string line in tileset)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(0);
                }
                Thread.Sleep(10);
                Console.WriteLine();
            }
            Console.WriteLine(tileset[0]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static List<string> DrawTiles(int x, int y)
        {
            List<string> tiles = new List<string> { };
            for (int g = 0; g < y; g++)
            {
                string LineText = "+";
                for (int i = 0; i < x; i++)
                {
                    LineText += "--------+";

                }
                tiles.Add(LineText);
                LineText = "|";
                for (int t = 0; t < 2; t++)
                {
                    LineText = "|";
                    for (int u = 0; u < x; u++)
                    {

                        LineText += "        |";
                    }
                    tiles.Add(LineText);

                }
                tiles.Add(LineText);
               
            }
            return tiles;
            
        }

        static string DrawInsideTile(string unit, int hp, bool selected = false, bool isHp = false, bool isRanged = false, bool canBeAttacked = false, bool isX = false)
        {
            string output = "        |";
            if (selected)
            {
                if (isRanged)
                {
                    output = $"({unit}r) |";
                }
                else
                {
                    output = $"({unit})  |";
                }

            }
            else if (isHp)
            {
                output = hp.ToString() + "  |";
            }
            else if (canBeAttacked)
            {
                output = "x   |";
            }
            else if (isX)
            {
                output = "X   |";
            }
            else if (unit != "")
            {
                output = $"{unit}   |";
            }
            return output;
        }
    }


}
