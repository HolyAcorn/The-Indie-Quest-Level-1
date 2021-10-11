using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Media;
using System.Linq;

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

        public bool IsRanged;

        public int MinRangedAttack;
        public int MaxRangedAttack;
        public int Ammo;

        public bool IsFriendly;

        public int[] UnitLocation;
        public int[] AmountLocation;
    }

    public struct Cell
    {
        public int Row;
        public char Column;
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

        static string intro2File = "Intro2.txt";
        static string[] intro2Array = File.ReadAllLines(intro2File);

        static SoundPlayer musicPlayer = new SoundPlayer(@"Sound\ChampionsTheme.wav");
        static SoundPlayer selectPlayer = new SoundPlayer(@"Sound\Select 1.wav");
        static SoundPlayer cancelPlayer = new SoundPlayer(@"Sound\Cancel 1.wav");

        static ConsoleColor mainTextColor = ConsoleColor.DarkRed;
        static Grid mainGrid = new Grid();

        //Grid Colors
        static ConsoleColor gridColor = ConsoleColor.White;
        static ConsoleColor unitFriendlyColor = ConsoleColor.Cyan;
        static ConsoleColor unitEnemyColor = ConsoleColor.Red;
        static ConsoleColor inputColor = ConsoleColor.Magenta;

        //Unit creation
        static List<Unit> friendlyUnits = new List<Unit>
            {
                CreateUnit(1,1,"Pikeman", 1, 3, 5, 4, 10, 1, true, false),
                CreateUnit(1,5,"Swordsman", 6, 9, 12, 5, 35, 2, true, false),
                CreateUnit(1,2,"Archer", 2, 3, 3, 4, 10, 3, true, true, 5, 8, 12)
            };
        static List<Unit> enemyUnits = new List<Unit>
            {
            CreateUnit(10,1,"Pikeman", 1, 3, 5, 3, 10, 1, false, false),
            CreateUnit(10,5,"Swordsman", 6, 9, 12, 2, 35, 2, false, false),
            CreateUnit(10,2,"Archer", 2, 3, 3, 3, 10, 3, false, true, 5, 8, 12)
            };


        static void Main(string[] args)
        {
            selectPlayer.Load();
            cancelPlayer.Load();
            //StartScreen();
            
            GenerateGrid();
            Console.Clear();
            DrawGrid();
            _DebugMove(friendlyUnits[0],0, friendlyUnits[0].UnitLocation);
            TakeCommandInput();
            
            //StartScreen();
            Console.ReadKey();
        }

        static void StartScreen()
        {
            
            musicPlayer.Load();
            
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
                PlaySelectionSound(1);
                Console.Clear();
                Thread.Sleep(1000);
                IntroText();
            }
            else if (input.Key == ConsoleKey.Q)
            {
                PlaySelectionSound(0);
                Thread.Sleep(100);
                Environment.Exit(0);
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
            Console.ReadKey();
            PlaySelectionSound(1);
            Console.Clear();

            WriteCool(intro2Array, 50, 500);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
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
                    mainGrid.Cells[x, y].Row = y+1;
                    mainGrid.Cells[x, y].Column = Convert.ToChar(x + 65);

                }
            }


            // First Row
            mainGrid.Lines[0] = "     +---------+";
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

                    mainGrid.Lines[y + 5] = "     +---------+";
                    mainGrid.Lines[y + 1] = "     |         |";
                    mainGrid.Lines[y + 2] = $"   {yCounter+1} |         |";
                    mainGrid.Lines[y + 3] = "     |         |";
                    mainGrid.Lines[y + 4] = "     |         |";
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
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = gridColor;
            Console.Write("     ");
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


        static Unit CreateUnit(int x, int y, string name, int minAttack, int maxAttack, int soak, int haste, int hp, int priority, bool isFriendly, bool isRanged, int minRangedAttack = 0, int maxRangedAttack = 0, int ammo = 0)
        {
            Unit unit = new Unit();
            unit.Name = name;
            unit.Symbol = name[0];
            switch (name)
            {
                case "Pikeman":
                    unit.Amount = random.Next(29,36);
                    break;
                case "Archer":
                    unit.Amount = random.Next(19,26);
                    break;
                case "Swordsman":
                    unit.Amount = random.Next(9,14);
                    break;
                default:
                    break;
            }

            unit.MinAttack = minAttack;
            unit.MaxAttack = maxAttack;
            unit.Soak = soak;
            unit.Haste = haste;
            unit.HP = hp;
            unit.Priority = priority;
            unit.IsFriendly = isFriendly;
            unit.IsRanged = isRanged;
            if (isRanged)
            {
                unit.MaxRangedAttack = maxRangedAttack;
                unit.MinRangedAttack = minRangedAttack;
                unit.Ammo = ammo;
            }

            unit.AmountLocation = new int[] { x, y };
            unit.UnitLocation = new int[] { (x), y};
            return unit;
        }
        static void _DebugMove(Unit unit, int unitIndex, int[] endLocation, int[] startLocation = null)
        {
            // Set color
            if (unit.IsFriendly)
            {
                Console.ForegroundColor = unitFriendlyColor;

            }
            else
            {
                Console.ForegroundColor = unitEnemyColor;
            }

            //Remove current place
            if (startLocation != null)
            {
                Console.SetCursorPosition(startLocation[0]*10, startLocation[1]*5);
                Console.Write("  ");
                Console.SetCursorPosition(startLocation[0]*10, startLocation[1]*5+1);
                Console.Write("  ");
            }

            //Write New Place
            Console.SetCursorPosition(endLocation[0]*10, endLocation[1]*5);
            Console.Write(unit.Symbol);
            Console.SetCursorPosition(endLocation[0]*10, endLocation[1]*5+1);
            Console.Write(unit.Amount);
            
            unit.UnitLocation = endLocation;
            friendlyUnits[0] = unit;

            //Reset CursorPosition
            Console.SetCursorPosition(0, height * 6);
        }

        static void PlaySelectionSound(int selection)
        {
            switch (selection)
            {
                case 0:
                    cancelPlayer.Play();
                    break;
                case 1:
                    selectPlayer.Play();
                    break;
                default:
                    break;
            }
        }

        static void TakeCommandInput()
        {
            
            bool shouldInput = true;
            while (true)
            {
                var input = Console.ReadLine();
                if (input.StartsWith("move") || input.StartsWith("Move"))
                {
                    string[] moveInput = input.Split(' ');
                    if (moveInput.Length < 3 && moveInput.Length > 1)
                    {
                        int moveColumnNumber = 0;
                        switch (moveInput[1].ToCharArray()[0])
                        {
                            case 'A':
                            case 'a':
                                moveColumnNumber = 1;
                                break;
                            case 'B':
                            case 'b':
                                moveColumnNumber = 2;
                                break;
                            case 'C':
                            case 'c':
                                moveColumnNumber = 3;
                                break;
                            case 'D':
                            case 'd':
                                moveColumnNumber = 4;
                                break;
                            case 'E':
                            case 'e':
                                moveColumnNumber = 5;
                                break;
                            case 'F':
                            case 'f':
                                moveColumnNumber = 6;
                                break;
                            case 'G':
                            case 'g':
                                moveColumnNumber = 7;
                                break;
                            case 'H':
                            case 'h':
                                moveColumnNumber = 8;
                                break;
                            case 'I':
                            case 'i':
                                moveColumnNumber = 9;
                                break;
                            case 'J':
                            case 'j':
                                moveColumnNumber = 10;
                                break;

                            default:
                                break;
                        }
                        char moveColumn = moveInput[1].ToCharArray()[0];
                        int moveRow = int.Parse(moveInput[1].ToCharArray()[1].ToString());
                        _DebugMove(friendlyUnits[0],0, new int[] { moveColumnNumber, moveRow }, friendlyUnits[0].UnitLocation);
                        DeleteCurrentLine();

                    }
                    else
                    {
                        IncorrectInput();
                    }
                }
                else
                {
                    IncorrectInput();
                }

            }
        }
        static void IncorrectInput()
        {
            Console.WriteLine("Please input a correct input.");
            Console.SetCursorPosition(0,Console.CursorTop-2);
            Thread.Sleep(2000);
            Console.WriteLine("                               ");
            Console.WriteLine("                               ");
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        static void DeleteCurrentLine()
        {
            Console.SetCursorPosition(0,Console.CursorTop);
            Console.Write("                                                            ");
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}