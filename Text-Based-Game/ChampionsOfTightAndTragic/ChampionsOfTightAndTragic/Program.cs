using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Media;

namespace TextBased_Test2
{
    public struct Unit
    {
        public string Name;
        public char Symbol;

        public int MinAttack;
        public int MaxAttack;
        public int Soak;

        public int Haste;
        public int HP;
        public int Amount;

        public int Priority;

        public int RangedAttack;
        public int Ammo;

        public bool IsFriendly;
    }

    public struct Cell
    {
        public int Row;
        public char Column;
        public int[] UnitLocation;
        public int[] AmountLocation;
    }

    public struct Grid
    {
        public Cell[,] Cells;
        public string[] Lines;
    }
    public class Program
    {


        static Random random = new Random();

        static int width = 9;
        static int height = 5;

        static string titleFile = "Title.txt";
        static string[] title = File.ReadAllLines(titleFile);
        static ConsoleColor[,] titleColorData = new ConsoleColor[title.Length, title[1].Length];
        static char[,] titleCharData = new char[title.Length, title[1].Length];

        static string tilesFile = "Tiles.txt";
        static string[] tilesArray = File.ReadAllLines(tilesFile);

        static SoundPlayer musicPlayer = new SoundPlayer(@"Sound\ChampionsTheme.wav");
        static SoundPlayer selectPlayer = new SoundPlayer(@"Sound\Select 1.wav");
        static SoundPlayer cancelPlayer = new SoundPlayer(@"Sound\Cancel 1.wav");

        static ConsoleColor mainTextColor = ConsoleColor.DarkRed;
        static Grid mainGrid = new Grid();

        //Grid Colors
        static ConsoleColor gridColor = ConsoleColor.White;
        static ConsoleColor unitFriendlyColor = ConsoleColor.Cyan;
        static ConsoleColor unitEnemyColor = ConsoleColor.Red;


        static void Main(string[] args)
        {



            GenerateGrid();
            Console.ReadKey();
            DrawGrid();
            Console.ReadKey();
            StartScreen();
            Console.ReadKey();
        }

        static void StartScreen()
        {
            selectPlayer.Load();
            musicPlayer.Load();
            cancelPlayer.Load();
            musicPlayer.PlayLooping();
            ConsoleColor borderColor = ConsoleColor.DarkYellow;
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

            Console.WriteLine();
            Console.ForegroundColor = mainTextColor;
            Console.WriteLine("Would you like to (p)lay, read a (t)utorial, or (q)uit?");
            ConsoleKeyInfo input = Console.ReadKey();
            if (input.Key == ConsoleKey.P)
            {
                selectPlayer.Play();
                Console.Clear();
                Thread.Sleep(1000);
                IntroText();
            }
            else
            {
                cancelPlayer.Play();
            }

        }

        static void IntroText()
        {
            string lightningFile = "Lightning.txt";
            string[] lightning = File.ReadAllLines(lightningFile);

            SoundPlayer lightningSFX = new SoundPlayer(@"Sound\LightningStrike.wav");
            lightningSFX.Load();
            lightningSFX.Play();

            string[] introTextShort = new string[]
            { "Ten years ago, the first hellgate was opened in the lands of Reydia by a mad mage.",
                "Two years later, Reydia w"
            };
            string[] introTextRest = new string[]
            { "as nothing but ruins.",
                "Every year, more and more hellgates are opened, and the demons keep coming.",
                "Three nations still stand, Denblum, Crithain and Trestria.",
                "These three nations have banded together and searched far an wide to find the greatest men and women ",
                "to lead their armies against the hordes of hell.",
                "The Champions stand as the final hope for humanity."
            };
            WriteCool(introTextShort, 50, 500);
            //Thread.Sleep(4810);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string line in lightning)
            {
                Console.WriteLine(line);
            }
            Thread.Sleep(150);

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("                                                               ");
            }
            Thread.Sleep(75);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = mainTextColor;
            Console.WriteLine(introTextShort[0]);
            Console.Write(introTextShort[1]);

            WriteCool(introTextRest, 50, 500);
            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(750);
                Console.Write('.');
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
        }

        static void WriteCool(string[] lines, int charSpeed, int rowSpeed)
        {
            int lineCount = 0;
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(charSpeed);
                }
                if (lineCount != lines.Length - 1)
                {
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
                lineCount++;

                Thread.Sleep(rowSpeed);
            }


        }

        static void GenerateGrid()
        {
            int cellHeight = 6;

            // Initialize Grid
            mainGrid.Cells = new Cell[width, height];
            mainGrid.Lines = new string[height * cellHeight-4];
            // 
            for (int y = 1; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    
                }
            }
            // Set Row & Columns
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mainGrid.Cells[x, y].AmountLocation = new int[] { x + 8+1, y * 4 + 4 };
                    mainGrid.Cells[x, y].UnitLocation = new int[] { x + 8+1, y * 3 + 3 };
                    mainGrid.Cells[x, y].Row = y+1;
                    mainGrid.Cells[x, y].Column = Convert.ToChar(x + 65);

                }
            }


            // First Row
            mainGrid.Lines[0] = "    +---------+";
            for (int x = 0; x < width; x++)
            {
                mainGrid.Lines[0] += "---------+";
            }

            // Remaining Grid
            int yCounter = 0;
            for (int y = 0; y <= height*cellHeight;)
            {
                if (y + height < height*cellHeight)
                {

                    mainGrid.Lines[y + 5] = "    +---------+";
                    mainGrid.Lines[y + 1] = "    |         |";
                    mainGrid.Lines[y + 2] = $"  {yCounter+1} |         |";
                    mainGrid.Lines[y + 3] = "    |         |";
                    mainGrid.Lines[y + 4] = "    |         |";
                    for (int x = 0; x < width; x++)
                    {
                        mainGrid.Lines[y + 5] += "---------+";
                        mainGrid.Lines[y + 1] += "         |";
                        mainGrid.Lines[y + 2] += $"         |";
                        mainGrid.Lines[y + 3] += "         |";
                        mainGrid.Lines[y + 4] += "         |";
                    }
                    yCounter++;
                    y += 5;
                }
                else
                {
                    break;
                }
            }
           
        }

        static void DrawGrid()
        {
            Console.ForegroundColor = gridColor;
            Console.Write("   ");
            for (int x = 0; x < width+1; x++)
            {
                Console.Write($"     {Convert.ToChar(x + 65)}    ");
            }
            Console.WriteLine();
            foreach (string line in mainGrid.Lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
