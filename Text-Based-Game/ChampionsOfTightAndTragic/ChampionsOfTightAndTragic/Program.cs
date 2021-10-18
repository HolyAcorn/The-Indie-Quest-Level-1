using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Media;
using System.Linq;

namespace TextBased_Test2
{
    class Unit
    {
        public string Name;
        public string Symbol;
        public ConsoleColor Color;

        public int MinAttack;
        public int MaxAttack;
        public int Defense;

        public int Haste;
        public int MaxHP;
        public int HP;
        public int Amount;

        public int Priority;

        public bool IsRanged;

        public int MinRangedAttack;
        public int MaxRangedAttack;
        public int Ammo;

        public bool IsFriendly;

        public bool CanBeAttacked = false;

        public int[] UnitLocation;
        public Cell Cell = new Cell();

        public override string ToString() => Name;

    }
    class Neighbor
    {
        public Cell Cell;
        public int Distance;

        public override string ToString() => new String("Neighbor: " + Cell.Column + (Cell.Row + 1).ToString());

    }
    class Cell
    {
        public int Row;
        public char Column;
        public int ColumnNumber;
        public bool IsEmpty = true;
        public bool IsReachable = false;
        public bool IsReachableRanged = false;

        public Unit Unit;
        public List<Neighbor> Neighbors = new List<Neighbor>();
        public List<Path> ShortestPath = new List<Path>();

        public override string ToString() => new String(new char[] { Column }) + (Row + 1).ToString();
    }
    class Path
    {
        public Cell Cell;
        public int Distance;
        public List<Cell> StopCells = new List<Cell>();
    }

    class Grid
    {
        public Cell[,] Cells;
        public string[] Lines;
    }

    class Turn
    {
        public List<Unit> UnitsTurn = new List<Unit>();
        public int TurnNumber = 0;
        public bool PlayerTurn;
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
        static Turn mainTurn = new Turn();
        static bool keepPlaying = true;
        static bool multiPlayer = false;

        //Grid Colors
        static ConsoleColor gridColor = ConsoleColor.White;
        static ConsoleColor unitFriendlyColor = ConsoleColor.Cyan;
        static ConsoleColor unitEnemyColor = ConsoleColor.Red;
        static ConsoleColor inputColor = ConsoleColor.Magenta;
        static ConsoleColor moveColor = ConsoleColor.DarkYellow;


        //Unit creation
        /*
         CreateUnit takes in these arguments = x, y, name, MinAattack, MaxAttack, Defense, Haste, HP , Priority, IsFriendly, IsRanged,
         MinRangeAttack, MaxRangeAttack, Ammo

         Creates all the units and stores them in a list of friendly units and enemy units.
         */
        static List<Unit> friendlyUnits = new List<Unit>
            {
                CreateUnit(0,0,"Pikeman", 2, 4, 3, 3, 10, 1, true, false),
                CreateUnit(0,4,"Swordsman", 6, 9, 6, 2, 35, 2, true, false),
                CreateUnit(0,1,"Archer", 2, 3, 1, 3, 10, 3, true, true, 5, 7, 10)
            };
        static List<Unit> enemyUnits = new List<Unit>
            {
                CreateUnit(8,4,"Pikeman",2, 4, 3, 3, 10, 1, false, false),
                CreateUnit(8,1,"Swordsman", 6, 9, 6, 2, 35, 2, false, false),
                CreateUnit(8,3,"Archer", 2, 3, 1, 3, 10, 3, false, true, 5, 7, 10)
            };

        private static int SortTurnOrder(Unit unit1, Unit unit2)
        {
            if (unit1 == null)
            {
                if (unit2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (unit2 == null)
                {
                    return 1;
                }
                else
                {

                    int haste = unit1.Haste.CompareTo(unit2.Haste);
                    if (haste != 0)
                    {
                        return haste;
                    }
                    else
                    {
                        return unit1.Haste.CompareTo(unit2.Haste);
                    }



                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            selectPlayer.Load();
            cancelPlayer.Load();
            //StartScreen();

            TutorialScreen();

            //Initialize Turns
            mainTurn.UnitsTurn = new List<Unit>();
            for (int i = 0; i < 3; i++)
            {
                mainTurn.UnitsTurn.Add(enemyUnits[i]);
                mainTurn.UnitsTurn.Add(friendlyUnits[i]);
            }
            mainTurn.UnitsTurn.Sort(SortTurnOrder);
            mainTurn.UnitsTurn.Reverse();


            GenerateGrid();
            Console.Clear();
            DrawGrid();
            InitializeInfoBox();
            foreach (Unit unit in mainTurn.UnitsTurn)
            {
                InitializeUnits(unit);
            }

            while (keepPlaying)
            {
                WriteInCell(mainTurn.UnitsTurn[0].UnitLocation, mainTurn.UnitsTurn[0].Color, $"({mainTurn.UnitsTurn[0].Symbol})", mainTurn.UnitsTurn[0].Amount.ToString());
                DisplayInfoBox(mainTurn.UnitsTurn[0]);

                if (mainTurn.UnitsTurn[0].IsFriendly)
                {
                    mainTurn.PlayerTurn = true;
                }
                else
                {
                    mainTurn.PlayerTurn = false;
                }
                bool turnDone = false;

                if (multiPlayer)
                {

                    TakeCommandInput(mainTurn.UnitsTurn[0]);
                    turnDone = true;
                }
                else
                {
                    if (mainTurn.PlayerTurn)
                    {
                        TakeCommandInput(mainTurn.UnitsTurn[0]);
                        turnDone = true;
                    }
                    else
                    {
                        //Placeholder Multiplayer, add AI method here later
                        //TakeCommandInput(mainTurn.UnitsTurn[0]);
                        AIDecideDestination(mainTurn.UnitsTurn[0]);
                        turnDone = true;
                    }
                }

                if (turnDone)
                {
                    Unit tempUnit = mainTurn.UnitsTurn[0];
                    mainTurn.UnitsTurn.RemoveAt(0);
                    mainTurn.UnitsTurn.Add(tempUnit);
                }
            }



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

        static void TutorialScreen(bool inGame = false)
        {
            int tutCharSpeed = 0; // 50
            int tutRowSpeed = 0; // 250
            if (inGame)
            {
                tutCharSpeed = 10;
                tutRowSpeed = 100;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            string tutorialPath = "Tutorial Screen.txt";
            string[] tutorialLines = File.ReadAllLines(tutorialPath);
            string[] tutorialText = new string[] {"Champions of Tight and Tragic is a turnbased strategy game.",
                                    "You will be controlling units on a grid with the goal to defeat the enemy units.", " ",
                                    "Please input the number of what you would like to learn more about: ",
                                    "1. The grid",
                                    "2. Input",
                                    "3. Units",
                                    "4. Back"};
            WriteCool(tutorialText, 0, 0);
            bool inTutorial = true;
            while (inTutorial)
            {
                Console.Clear();

                WriteCool(tutorialText, 0, 0);
                ConsoleKeyInfo tutorialInput = Console.ReadKey();
                
                switch (tutorialInput.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.ForegroundColor = gridColor;
                        foreach (string line in tutorialLines)
                        {
                            Console.WriteLine(line);
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(0, 20);
                        WriteCool(new string[] {
                                "This is a smaller version of the grid. As you can see,",
                                "the columns are named by letters, and the rows are named by numbers.",
                                "So to find a cell you can use these two namings. So the second cell",
                                "in the third row would be B3.",
                                "Whilst the first cell in the second row would be A2, and so forth.", " ",
                                "Please press any key to continue..."}, tutCharSpeed, tutRowSpeed);
                        Console.ReadKey();
                        Console.SetCursorPosition(8, 5);
                        Console.ForegroundColor = moveColor;
                        Console.Write("(");
                        Console.ForegroundColor = unitFriendlyColor;
                        Console.Write("P");
                        Console.ForegroundColor = moveColor;
                        Console.Write(")");
                        Console.SetCursorPosition(9, 6);
                        Console.ForegroundColor = unitFriendlyColor;
                        Console.Write("24");
                        Console.SetCursorPosition(9 + 10, 5);
                        Console.ForegroundColor = moveColor;
                        Console.Write("x");
                        Console.SetCursorPosition(9 + 10, 10);
                        Console.Write("x");
                        Console.SetCursorPosition(9, 10);
                        Console.Write("x");
                        Console.SetCursorPosition(39, 15);
                        Console.ForegroundColor = unitEnemyColor;
                        Console.Write("S");
                        Console.SetCursorPosition(39, 16);
                        Console.Write("9");


                        for (int i = 21; i < 28; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            DeleteCurrentLine();
                        }
                        Console.SetCursorPosition(0, 20);
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteCool(new string[] {
                            "This is what a board can look like.",
                             "The colors Cyan and Red denote either friendly or enemy unit types respectively.",
                             "In this example, the " + '\u0022' + "P" + '\u0022' + " stands for Pikeman, and the number underneath it (24)",
                             "are how many of that unit type you have. The same thing with the " + '\u0022' + "S" + '\u0022' + ", that stands for Swordsman.",
                             "The yellow markers around the P indicates that it is that unit's turn. ",
                             "The yellow " + '\u0022' + "x" + '\u0022' + " shows which cells the unit can walk to.",
                             " ",
                             "Please press any key to continue..."}, tutCharSpeed, tutRowSpeed);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        // Input 1
                        Console.Clear();
                        WriteCool(new string[] {
                        "There are 5 inputs you can enter.",
                        " ",
                        "These are:"}, tutCharSpeed, tutRowSpeed);
                        WriteCool(new string[] {" ","Move",
                        "Attack",
                        "Hold",
                        "Info",
                        "Help" }, tutCharSpeed, tutRowSpeed);
                        Thread.Sleep(2000);
                        WriteCool(new string[] {" ", " ", "Press any key to continue..." }, tutCharSpeed, tutRowSpeed);
                        Console.ReadKey();
                        Console.Clear();

                        // Input 2
                        WriteCool(new string[] { "Move" }, tutCharSpeed, tutRowSpeed);
                        Console.WriteLine();
                        Console.WriteLine();
                        Thread.Sleep(2000);
                        WriteCool(new string[] {"During one of your unit's turns, you can move that unit to any",
                        "cell that has a yellow " + '\u0022' + "x" + '\u0022' + " in it.",
                        "You do this by typing: "}, tutCharSpeed, tutRowSpeed);
                        Console.ForegroundColor = inputColor;
                        WriteCool(new string[] { "move x" }, tutCharSpeed, tutRowSpeed);
                        Console.WriteLine();
                        Thread.Sleep(500);
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteCool(new string[] {"You would however replace the x with one of the cells you can move into. Example: ", " " }, tutCharSpeed, tutRowSpeed);
                        Console.ForegroundColor = inputColor;
                        WriteCool(new string[] {"move a2" }, tutCharSpeed, tutRowSpeed);
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteCool(new string[] {" ", " ", "Press any key to continue..." }, tutCharSpeed, tutRowSpeed);
                        Console.ReadKey();

                        // Input 3 (Move)
                        Console.Clear();
                        Console.ForegroundColor = gridColor;
                        foreach (string line in tutorialLines)
                        {
                            Console.WriteLine(line);
                        }
                        Console.SetCursorPosition(8, 5);
                        Console.ForegroundColor = moveColor;
                        Console.Write("(");
                        Console.ForegroundColor = unitFriendlyColor;
                        Console.Write("P");
                        Console.ForegroundColor = moveColor;
                        Console.Write(")");
                        Console.SetCursorPosition(9, 6);
                        Console.ForegroundColor = unitFriendlyColor;
                        Console.Write("24");
                        Console.SetCursorPosition(9 + 10, 5);
                        Console.ForegroundColor = moveColor;
                        Console.Write("x");
                        Console.SetCursorPosition(9 + 10, 10);
                        Console.Write("x");
                        Console.SetCursorPosition(9, 10);
                        Console.Write("x");
                        Console.SetCursorPosition(39, 15);
                        Console.ForegroundColor = unitEnemyColor;
                        Console.Write("S");
                        Console.SetCursorPosition(39, 16);
                        Console.Write("9");

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(0, 20);

                        WriteCool(new string[] { "Try it yourself..." }, tutCharSpeed, tutRowSpeed);
                        Console.WriteLine();
                        bool takeTutorialInput = true;
                        while (takeTutorialInput)
                        {
                            Console.ForegroundColor = inputColor;
                            string tutorialTestInput = Console.ReadLine();
                            if (tutorialTestInput.Contains("move") || tutorialTestInput.Contains("Move"))
                            {
                                string[] tutTestInputSplit = tutorialTestInput.Split(' ');
                                if (tutTestInputSplit.Length > 1 && tutTestInputSplit.Length < 3)
                                {
                                    switch (tutTestInputSplit[1])
                                    {
                                        case "B1":
                                        case "b1":
                                            TutorialClearMovable();
                                            Console.SetCursorPosition(19, 5);
                                            Console.ForegroundColor = unitFriendlyColor;
                                            Console.Write("P");
                                            Console.SetCursorPosition(19, 6);
                                            Console.Write("24");
                                            Thread.Sleep(500);
                                            takeTutorialInput = false;
                                            break;
                                        case "B2":
                                        case "b2":
                                            TutorialClearMovable();
                                            Console.SetCursorPosition(19, 10);
                                            Console.ForegroundColor = unitFriendlyColor;
                                            Console.Write("P");
                                            Console.SetCursorPosition(19, 11);
                                            Console.Write("24");
                                            Thread.Sleep(500);
                                            takeTutorialInput = false;
                                            break;
                                        case "A2":
                                        case "a2":
                                            TutorialClearMovable();
                                            Console.SetCursorPosition(9, 10);
                                            Console.ForegroundColor = unitFriendlyColor;
                                            Console.Write("P");
                                            Console.SetCursorPosition(9, 11);
                                            Console.Write("24");
                                            Thread.Sleep(500);
                                            takeTutorialInput = false;
                                            break;
                                        default:
                                            IncorrectInput("You cannot move to that cell, please choose a cell that has a yellow " + '\u0022' + "x" + '\u0022' + " in it.");
                                            break;
                                    }
                                }
                                else
                                {
                                    IncorrectInput("Hmm, that didn't work. Remember, to move you type move x. Replace the x with the cell you wish to travel to.");
                                }
                            }
                            else
                            {
                                IncorrectInput("Hmm, that didn't work. Remember, to move you type move x. Replace the x with the cell you wish to travel to.");
                            }
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        break;

                }

                Console.SetCursorPosition(0, 21);
                DeleteCurrentLine();
                Console.SetCursorPosition(0, 20);
                Console.ForegroundColor = ConsoleColor.White;
                DeleteCurrentLine();
                WriteCool(new string[] { "Great job! :D", " ", "As you can see, the turn has now passed onto the red Swordsman.",
                    "This is because every turn, your unit can only perform 1 action." }, tutCharSpeed, tutRowSpeed);
                Console.ReadKey();

                // Input 4 (Attack)
                Console.Clear();
                Console.WriteLine("Attack");
                WriteCool(new string[] {" ", "Whenever you are close enough to attack an enemy, a yellow " + '\u0022' + "#" + '\u0022' + " will appear beneath them.",
                                        "You can then choose to attack them by typing: "}, tutCharSpeed,tutRowSpeed);
                Console.ForegroundColor = inputColor;
                WriteCool(new string[] {"attack x" }, tutCharSpeed,tutRowSpeed);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                WriteCool(new string[] {"You would however replace the x with the cell where the unit you wish to attack is located. Example: " }, tutCharSpeed,tutRowSpeed);
                Console.ForegroundColor = inputColor;
                WriteCool(new string[] { "attack C4" }, tutCharSpeed, tutRowSpeed);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                WriteCool(new string[] { "Press any key to continue...." }, tutCharSpeed, tutRowSpeed);
                Console.ReadKey();
                Console.Clear();

                Console.ForegroundColor = gridColor;
                foreach (string line in tutorialLines)
                {
                    Console.WriteLine(line);
                }
                Console.SetCursorPosition(8, 5);
                Console.ForegroundColor = moveColor;
                Console.Write("(");
                Console.ForegroundColor = unitFriendlyColor;
                Console.Write("P");
                Console.ForegroundColor = moveColor;
                Console.Write(")");
                Console.SetCursorPosition(9, 6);
                Console.ForegroundColor = unitFriendlyColor;
                Console.Write("24");
                Console.SetCursorPosition(9 + 10, 5);
                Console.ForegroundColor = moveColor;
                Console.Write("x");
                Console.SetCursorPosition(9 + 10, 10);
                Console.Write("x");
                Console.SetCursorPosition(9, 10);
                Console.Write("x");
                Console.SetCursorPosition(29, 10);
                Console.ForegroundColor = unitEnemyColor;
                Console.Write("S");
                Console.SetCursorPosition(29, 11);
                Console.Write("9");
                Console.ForegroundColor = moveColor;
                Console.SetCursorPosition(29, 12);
                Console.Write("#");

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 20);
                WriteCool(new string[] {"As you can see, the red Swordsman is within range to be attacked.", "Try attacking the unit." },tutCharSpeed,tutRowSpeed);
                Console.WriteLine();
                string tutorialTestInput2 = Console.ReadLine();


            }

            static void TutorialClearMovable()
            {
                Console.SetCursorPosition(8, 5);
                Console.Write("   ");
                Console.SetCursorPosition(8, 6);
                Console.Write("   ");
                Console.SetCursorPosition(19, 5);
                Console.ForegroundColor = moveColor;
                Console.Write(" ");
                Console.SetCursorPosition(9 + 10, 10);
                Console.Write(" ");
                Console.SetCursorPosition(9, 10);
                Console.Write(" ");
                Console.SetCursorPosition(38, 15);
                Console.Write("(");
                Console.SetCursorPosition(40, 15);
                Console.Write(")");
                Console.SetCursorPosition(0, 20);
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
            //How tall one cell is
            int cellHeight = 6;

            // Initialize Grid
            mainGrid.Cells = new Cell[width, height];
            mainGrid.Lines = new string[height * cellHeight - 4];
            // Set Row & Columns
            // I create one new cell for every spot in the Maingrid Array
            //Since computer counts from 0, every row is offseted by -1 and needs to be adjusted when taking input.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    mainGrid.Cells[x, y] = new Cell();
                    mainGrid.Cells[x, y].Row = y;
                    mainGrid.Cells[x, y].Column = Convert.ToChar(x + 65);
                    mainGrid.Cells[x, y].ColumnNumber = x;
                    mainGrid.Cells[x, y].Neighbors = new List<Neighbor>();
                    mainGrid.Cells[x, y].ShortestPath = new List<Path>();
                    if (x > 0)
                    {
                        ConnectCells(mainGrid.Cells[x, y], mainGrid.Cells[x - 1, y]);
                    }
                    if (y > 0)
                    {
                        ConnectCells(mainGrid.Cells[x, y], mainGrid.Cells[x, y - 1]);
                        if (x < width - 1)
                        {
                            ConnectCells(mainGrid.Cells[x, y], mainGrid.Cells[x + 1, y - 1]);
                        }
                    }
                    if (x > 0 && y > 0)
                    {
                        ConnectCells(mainGrid.Cells[x, y], mainGrid.Cells[x - 1, y - 1]);


                    }

                }
            }
            foreach (Cell cell in mainGrid.Cells)
            {
                Dijkstra(mainGrid.Cells, cell);
            }


            // First Row
            mainGrid.Lines[0] = "     +---------+";
            for (int x = 0; x < width - 1; x++)
            {
                mainGrid.Lines[0] += "---------+";
            }

            // Remaining Grid
            int yCounter = 0;
            for (int y = 0; y <= height * cellHeight;)
            {
                if (y + height < height * cellHeight)
                {

                    mainGrid.Lines[y + 5] = "     +---------+";
                    mainGrid.Lines[y + 1] = "     |         |";
                    mainGrid.Lines[y + 2] = $"   {yCounter + 1} |         |";
                    mainGrid.Lines[y + 3] = "     |         |";
                    mainGrid.Lines[y + 4] = "     |         |";
                    for (int x = 0; x < width - 1; x++)
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
            for (int x = 0; x < width; x++)
            {
                Console.Write($"     {Convert.ToChar(x + 65)}    ");
            }
            Console.WriteLine();
            foreach (string line in mainGrid.Lines)
            {
                Console.WriteLine(line);
            }
        }

        static void InitializeInfoBox()
        {
            string infoPath = "infobox.txt";
            string[] infoLines = File.ReadAllLines(infoPath);
            Console.SetCursorPosition(96, 3);
            foreach (string line in infoLines)
            {
                Console.Write(line);
                Console.SetCursorPosition(96, Console.CursorTop + 1);
            }
        }

        static void DisplayInfoBox(Unit unit)
        {
            static void WriteDisplay(int left, int top, string text)
            {
                Console.SetCursorPosition(left, top);
                if (text == "Archer" || text == "Pikeman")
                {
                    Console.Write("          ");
                }
                else
                {
                    Console.Write("        ");
                }
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
            Console.ForegroundColor = unit.Color;
            WriteDisplay(116, 6, unit.Name);
            WriteDisplay(113, 7, $"{unit.MinAttack} - {unit.MaxAttack}");
            WriteDisplay(114, 8, $"{unit.Defense}");
            WriteDisplay(112, 9, $"{unit.Haste}");
            WriteDisplay(109, 10, $"{unit.HP} / {unit.MaxHP}");
            WriteDisplay(113, 11, $"{unit.Amount}");
            WriteDisplay(120, 12, $"{unit.MinRangedAttack} - {unit.MaxRangedAttack}");
            WriteDisplay(111, 13, $"{unit.Ammo}");

            Console.SetCursorPosition(0, 30);

        }


        static Unit CreateUnit(int x, int y, string name, int minAttack, int maxAttack, int defense, int haste, int hp, int priority, bool isFriendly, bool isRanged, int minRangedAttack = 0, int maxRangedAttack = 0, int ammo = 0)
        {
            Unit unit = new Unit();
            unit.Name = name;
            unit.Symbol = name[0].ToString();
            if (isRanged)
            {
                unit.Symbol += "r";
            }
            switch (name)
            {
                case "Pikeman":
                    unit.Amount = random.Next(29, 36);
                    break;
                case "Archer":
                    unit.Amount = random.Next(19, 26);
                    break;
                case "Swordsman":
                    unit.Amount = random.Next(9, 14);
                    break;
                default:
                    break;
            }

            unit.MinAttack = minAttack;
            unit.MaxAttack = maxAttack;
            unit.Defense = defense;
            unit.Haste = haste;
            unit.HP = hp;
            unit.MaxHP = hp;
            unit.Priority = priority;
            unit.IsFriendly = isFriendly;
            if (unit.IsFriendly)
            {
                unit.Color = unitFriendlyColor;
            }
            else
            {
                unit.Color = unitEnemyColor;
            }
            unit.IsRanged = isRanged;
            if (isRanged)
            {
                unit.MaxRangedAttack = maxRangedAttack;
                unit.MinRangedAttack = minRangedAttack;
                unit.Ammo = ammo;
            }
            unit.UnitLocation = new int[] { (x), y };
            return unit;
        }
        static void InitializeUnits(Unit unit)
        {
            int[] endLocation = unit.UnitLocation;
            // Set color
            if (unit.IsFriendly)
            {
                Console.ForegroundColor = unitFriendlyColor;

            }
            else
            {
                Console.ForegroundColor = unitEnemyColor;
            }


            //Write New Place
            WriteInCell(endLocation, unit.Color, $" {unit.Symbol.ToString()}", unit.Amount.ToString());
            unit.Cell = mainGrid.Cells[endLocation[0], endLocation[1]];
            mainGrid.Cells[endLocation[0], endLocation[1]].Unit = unit;
            SetCellNotEmpty(mainGrid.Cells[endLocation[0], endLocation[1]]);
            unit.UnitLocation = endLocation;
            if (unit.IsFriendly)
            {
                friendlyUnits[0] = unit;
            }
            else
            {
                enemyUnits[0] = unit;
            }


            //Reset CursorPosition
            Console.SetCursorPosition(0, height * 6 - 1);
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

        static void TakeCommandInput(Unit unit)
        {

            bool shouldInput = true;
            while (shouldInput)
            {

                SetMoveTiles(unit);
                Console.ForegroundColor = inputColor;
                var input = Console.ReadLine();
                if (input.StartsWith("move") || input.StartsWith("Move"))
                {
                    string[] moveInput = input.Split(' ');
                    if (moveInput.Length < 3 && moveInput.Length > 1)
                    {
                        int moveColumnNumber = ConvertColumn(moveInput[1].ToCharArray()[0]);

                        int moveRow = int.Parse(moveInput[1].ToCharArray()[1].ToString()) - 1;
                        if (mainGrid.Cells[moveColumnNumber, moveRow].IsReachable)
                        {
                            foreach (Cell cell in mainGrid.Cells)
                            {
                                if (cell.IsReachable && cell.IsEmpty)
                                {
                                    WriteInCell(new int[] { cell.ColumnNumber, cell.Row }, ConsoleColor.White);
                                }
                            }
                            _MoveByDijsktra(unit, 0, new int[] { moveColumnNumber, moveRow }, unit.UnitLocation);
                            DeleteCurrentLine();
                            shouldInput = false;

                        }
                        else
                        {
                            IncorrectInput("Your unit cannot reach that. Please select a tile with an 'x' on it");
                        }


                    }
                    else
                    {
                        IncorrectInput("Please input a correct input.");
                    }
                }
                else if (input.StartsWith("attack") || input.StartsWith("Attack"))
                {
                    string[] attackInput = input.Split(' ');
                    if (attackInput.Length < 4 && attackInput.Length > 1)
                    {
                        int moveColumnNumber = ConvertColumn(attackInput[1].ToCharArray()[0]) - 1;
                        int moveRow = int.Parse(attackInput[1].ToCharArray()[1].ToString()) - 1;
                        if (attackInput.Length == 3)
                        {
                            moveColumnNumber = ConvertColumn(attackInput[2].ToCharArray()[0]);
                            moveRow = int.Parse(attackInput[2].ToCharArray()[1].ToString()) - 1;
                        }
                        else
                        {
                            moveColumnNumber = ConvertColumn(attackInput[1].ToCharArray()[0]) - 1;
                            moveRow = int.Parse(attackInput[1].ToCharArray()[1].ToString()) - 1;
                        }
                        int attackColumnNumber = ConvertColumn(attackInput[1].ToCharArray()[0]);
                        int attackRow = int.Parse(attackInput[1].ToCharArray()[1].ToString()) - 1;

                        if (mainGrid.Cells[attackColumnNumber, attackRow].Unit != null)

                        {
                            if (mainGrid.Cells[attackColumnNumber, attackRow].IsReachable || mainGrid.Cells[attackColumnNumber, attackRow].IsReachableRanged)
                            {
                                if (mainGrid.Cells[attackColumnNumber, attackRow].Unit.IsFriendly != unit.IsFriendly)
                                {
                                    foreach (Cell cell in mainGrid.Cells)
                                    {
                                        if (cell.IsReachable && cell.IsEmpty)
                                        {
                                            WriteInCell(new int[] { cell.ColumnNumber, cell.Row }, ConsoleColor.White);
                                        }
                                    }
                                    if (!unit.IsRanged || unit.Ammo <= 0)
                                    {
                                        unit.Ammo--;
                                        _MoveByDijsktra(unit, 0, new int[] { moveColumnNumber, moveRow }, unit.UnitLocation);
                                    }

                                    WriteInCell(unit.UnitLocation, unit.Color);
                                    WriteInCell(unit.UnitLocation, unit.Color, $" {unit.Symbol.ToString()}", unit.Amount.ToString());
                                    AttackUnit(unit, mainGrid.Cells[attackColumnNumber, attackRow].Unit);
                                    shouldInput = false;

                                }
                                else
                                {
                                    IncorrectInput("You cannot attack your own troops!");
                                }
                            }
                            else
                            {
                                IncorrectInput("You cannot reach that enemy!");
                            }

                        }
                        else
                        {
                            IncorrectInput("There is no enemy in that cell!");
                        }

                    }
                    else
                    {
                        IncorrectInput("Please input a correct input.");
                    }
                }
                else if (input == "hold" || input == "Hold")
                {
                    Console.SetCursorPosition(0, 30);
                    DeleteCurrentLine();
                    Console.ForegroundColor = unit.Color;
                    Console.Write($"{unit.Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" holds.");
                    Console.SetCursorPosition(0, 30);
                    Thread.Sleep(2500);
                    DeleteCurrentLine();
                }
                else if (input.StartsWith("info") || input.StartsWith("Info"))
                {
                    string[] infoInput = input.Split(' ');
                    if (infoInput.Length < 3 && infoInput.Length > 1)
                    {
                        int infoColumnNumber = ConvertColumn(infoInput[1].ToCharArray()[0]);

                        int infoRow = int.Parse(infoInput[1].ToCharArray()[1].ToString()) - 1;
                        if (mainGrid.Cells[infoColumnNumber, infoRow].Unit != null)
                        {
                            DisplayInfoBox(mainGrid.Cells[infoColumnNumber, infoRow].Unit);
                            DeleteCurrentLine();
                            Console.ForegroundColor = inputColor;
                            Console.WriteLine("Press any key to return.");
                            Console.ReadKey();
                            DeleteCurrentLine();
                            DisplayInfoBox(unit);
                        }
                        else
                        {
                            IncorrectInput("There is no unit in that cell.");
                        }
                    }
                    else
                    {
                        IncorrectInput("Please input a correct input.");
                    }
                }
            }
        }
        static void IncorrectInput(string message)
        {
            Console.WriteLine(message);
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Thread.Sleep(2000);
            Console.WriteLine("                                                                              ");
            Console.WriteLine("                                                                              ");
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        static void DeleteCurrentLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write("                                                                              ");
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        static void ConnectCells(Cell a, Cell b)
        {
            a.Neighbors.Add(new Neighbor { Cell = b, Distance = 1 });
            b.Neighbors.Add(new Neighbor { Cell = a, Distance = 1 });
        }

        static void Dijkstra(Cell[,] grid, Cell source)
        {
            // Q = Set of Neighbors
            // u = Neighbour of Q that has the shortest distance from source
            // v = Cell in Grid
            List<Cell> Q = new List<Cell> { };
            Dictionary<Cell, int> dist = new Dictionary<Cell, int> { };
            Dictionary<Cell, Cell> prev = new Dictionary<Cell, Cell> { };
            foreach (Cell v in grid)
            {
                dist.Add(v, 99);
                prev.Add(v, null);
                Q.Add(v);
            }
            dist[source] = 0;

            while (Q.Count != 0)
            {
                Cell u = Q.OrderBy((v) => dist[v]).First();

                Q.Remove(u);
                for (int v = 0; v < u.Neighbors.Count; v++)
                {
                    Cell neighbor = u.Neighbors[v].Cell;
                    if (Q.Contains(u.Neighbors[v].Cell))
                    {
                        int alt = dist[u] + u.Neighbors[v].Distance;
                        if (alt < dist[neighbor])
                        {
                            dist[neighbor] = alt;
                            prev[neighbor] = u;
                        }
                    }
                }
            }

            foreach (Cell otherCell in grid)
            {
                if (otherCell == source) continue;

                var path = new Path { Cell = otherCell, Distance = dist[otherCell] };
                source.ShortestPath.Add(path);

                Cell stop = prev[otherCell];
                while (stop != source)
                {
                    path.StopCells.Insert(0, stop);
                    stop = prev[stop];
                }
            }
            //source.ShortestPath.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        }

        static void _MoveByDijsktra(Unit unit, int unitIndex, int[] endLocation, int[] startLocation = null)
        {
            mainGrid.Cells[startLocation[0], startLocation[1]].IsEmpty = true;
            int[] stopLocation = new int[2];
            // Set color
            if (unit.IsFriendly)
            {
                Console.ForegroundColor = unitFriendlyColor;

            }
            else
            {
                Console.ForegroundColor = unitEnemyColor;
            }


            var startGrid = mainGrid.Cells[startLocation[0], startLocation[1]];
            var endGrid = mainGrid.Cells[endLocation[0], endLocation[1]];
            int pathIndex = 0;
            //Write New Place
            for (int i = 0; i < startGrid.ShortestPath.Count; i++)
            {
                if (startGrid.ShortestPath[i].Cell == endGrid)
                {
                    if (startGrid.ShortestPath[i].StopCells.Count != 0)
                    {
                        stopLocation[1] = startGrid.ShortestPath[i].StopCells[0].Row;
                        stopLocation[0] = ConvertColumn(startGrid.ShortestPath[i].StopCells[0].Column);
                        pathIndex = i;
                    }
                    else
                    {
                        stopLocation = new int[] { endLocation[0], endLocation[1] };
                    }

                    break;
                }
            }
            for (int i = 1; i < startGrid.ShortestPath[pathIndex].StopCells.Count + 2; i++)
            {
                if (startLocation[0] == endLocation[0] && startLocation[1] == endLocation[1])
                {
                    break;
                }
                else
                {
                    MoveOneCell(i);
                }

            }
            /*Console.SetCursorPosition(endLocation[0] * 10 + 10, endLocation[1] * 5 + 5);
            Console.Write(unit.Symbol);
            Console.SetCursorPosition(endLocation[0] * 10 + 10, endLocation[1] * 5 + 1 + 5);
            Cosole.Write(unit.Amount);*/
            unit.Cell = mainGrid.Cells[endLocation[0], endLocation[1]];
            mainGrid.Cells[endLocation[0], endLocation[1]].Unit = unit;
            SetCellNotEmpty(mainGrid.Cells[endLocation[0], endLocation[1]]);
            unit.UnitLocation = endLocation;
            if (unit.IsFriendly)
            {
                friendlyUnits[0] = unit;
            }
            else
            {
                enemyUnits[0] = unit;
            }


            //Reset CursorPosition
            Console.SetCursorPosition(0, height * 6);

            void MoveOneCell(int stopIndex)
            {
                int stepTimer = 1000;
                //Remove current place
                if (startLocation != null)
                {
                    WriteInCell(startLocation, ConsoleColor.White);
                }
                WriteInCell(stopLocation, unit.Color, $" {unit.Symbol.ToString()}", unit.Amount.ToString());
                mainGrid.Cells[startLocation[0], startLocation[1]].IsEmpty = true;
                mainGrid.Cells[startLocation[0], startLocation[1]].Unit = null;
                startLocation = new int[2] { stopLocation[0], stopLocation[1] };
                if (stopIndex < startGrid.ShortestPath[pathIndex].StopCells.Count)
                {
                    stopLocation[1] = startGrid.ShortestPath[pathIndex].StopCells[stopIndex].Row;
                    stopLocation[0] = ConvertColumn(startGrid.ShortestPath[pathIndex].StopCells[stopIndex].Column);
                    Thread.Sleep(stepTimer);
                }
                else
                {
                    stopLocation = new int[2] { endLocation[0], endLocation[1] };
                    Thread.Sleep(stepTimer);
                }


            }


        }
        static int ConvertColumn(char input)
        {
            int moveColumnNumber = 0;
            switch (input)
            {
                case 'A':
                case 'a':
                    moveColumnNumber = 0;
                    break;
                case 'B':
                case 'b':
                    moveColumnNumber = 1;
                    break;
                case 'C':
                case 'c':
                    moveColumnNumber = 2;
                    break;
                case 'D':
                case 'd':
                    moveColumnNumber = 3;
                    break;
                case 'E':
                case 'e':
                    moveColumnNumber = 4;
                    break;
                case 'F':
                case 'f':
                    moveColumnNumber = 5;
                    break;
                case 'G':
                case 'g':
                    moveColumnNumber = 6;
                    break;
                case 'H':
                case 'h':
                    moveColumnNumber = 7;
                    break;
                case 'I':
                case 'i':
                    moveColumnNumber = 8;
                    break;
                case 'J':
                case 'j':
                    moveColumnNumber = 9;
                    break;

                default:
                    break;
            }
            return moveColumnNumber;
        }

        static void SetMoveTiles(Unit unit)
        {
            int haste = unit.Haste;
            int[] location = unit.UnitLocation;
            Cell cell = unit.Cell;
            // False = AI, true = Player
            bool unitTeam = unit.IsFriendly;
            Console.ForegroundColor = unit.Color;
            foreach (Cell c in mainGrid.Cells)
            {
                SetSingleEmptyTile(new int[] { c.ColumnNumber, c.Row });
                if (c.Unit != null)
                {
                    c.IsEmpty = false;
                }
                c.IsReachable = false;
                c.IsReachableRanged = false;
            }
            for (int i = 0; i < haste + 1; i++)
            {
                foreach (Neighbor neighbor in cell.Neighbors)
                {
                    int[] tiler = new int[] { neighbor.Cell.ColumnNumber, neighbor.Cell.Row };
                    if (neighbor.Cell.IsEmpty)
                    {
                        SetSingleMoveableTile(tiler);
                        mainGrid.Cells[tiler[0], tiler[1]].IsEmpty = true;
                        mainGrid.Cells[tiler[0], tiler[1]].IsReachable = true;
                    }
                    foreach (Neighbor neighborInNeighbor in neighbor.Cell.Neighbors)
                    {
                        if (neighborInNeighbor.Cell.IsEmpty)
                        {
                            tiler = new int[] { neighborInNeighbor.Cell.ColumnNumber, neighborInNeighbor.Cell.Row };
                            SetSingleMoveableTile(tiler);
                            mainGrid.Cells[tiler[0], tiler[1]].IsEmpty = true;
                            mainGrid.Cells[tiler[0], tiler[1]].IsReachable = true;
                        }
                        else if (neighborInNeighbor.Cell.Unit.IsFriendly != unitTeam)
                        {
                            tiler = new int[] { neighborInNeighbor.Cell.ColumnNumber, neighborInNeighbor.Cell.Row };
                            SetSingleEnemyTile(tiler);
                            mainGrid.Cells[tiler[0], tiler[1]].IsEmpty = false;
                            mainGrid.Cells[tiler[0], tiler[1]].IsReachable = true;

                        }
                    }
                }
            }

            if (unit.IsRanged)
            {
                foreach (Cell c in mainGrid.Cells)
                {
                    if (!c.IsEmpty && c.Unit.IsFriendly != unitTeam)
                    {
                        int[] tiler = new int[] { c.ColumnNumber, c.Row };
                        SetSingleEnemyTile(tiler);
                        mainGrid.Cells[tiler[0], tiler[1]].IsEmpty = false;
                        mainGrid.Cells[tiler[0], tiler[1]].IsReachableRanged = true;
                    }
                }
            }



            Console.SetCursorPosition(0, 29);


            static void SetSingleMoveableTile(int[] loc)
            {
                Console.SetCursorPosition(loc[0] * 10 + 10, loc[1] * 5 + 5);
                Console.ForegroundColor = moveColor;
                Console.Write("x  ");
                /*for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(loc[0] * 10 + 6, loc[1] * 5 + 4+i);
                    Console.Write("         ");
                }
                Console.BackgroundColor = ConsoleColor.Black;*/

            }
            static void SetSingleEnemyTile(int[] loc)
            {
                Console.SetCursorPosition(loc[0] * 10 + 10, loc[1] * 5 + 5 + 2);
                Console.ForegroundColor = moveColor;
                Console.Write("#  ");
            }
            static void SetSingleEmptyTile(int[] loc)
            {
                Console.SetCursorPosition(loc[0] * 10 + 10, loc[1] * 5 + 5 + 2);
                Console.Write("   ");
            }
        }

        static void SetCellNotEmpty(Cell cell)
        {
            cell.IsEmpty = false;
        }

        static void WriteInCell(int[] location, ConsoleColor color, string text1 = "    ", string text2 = "    ")
        {
            if (text1.Contains('('))
            {
                Console.SetCursorPosition(location[0] * 10 + 10 - 1, location[1] * 5 + 5);
                Console.ForegroundColor = moveColor;
                Console.Write(text1[0]);
                Console.ForegroundColor = color;
                for (int i = 1; i < text1.Length - 1; i++)
                {
                    Console.Write(text1[i]);
                }
                Console.ForegroundColor = moveColor;
                Console.Write(text1.Last());
                Console.ForegroundColor = color;
            }
            else
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(location[0] * 10 + 10 - 1, location[1] * 5 + 5);
                Console.Write(text1);
            }
            Console.SetCursorPosition(location[0] * 10 + 10, location[1] * 5 + 5 + 1);
            Console.Write(text2);
        }

        static void AttackUnit(Unit attackingUnit, Unit attackedUnit)
        {
            int defense = attackedUnit.Defense;
            int damage = 0;
            if (attackingUnit.IsRanged && attackingUnit.Ammo > 0)
            {
                damage = (random.Next(attackingUnit.MinRangedAttack, attackingUnit.MaxRangedAttack + 1) * (attackingUnit.Amount / 2)) - defense;
                attackingUnit.Ammo--;
            }
            else
            {
                damage = (random.Next(attackingUnit.MinAttack, attackingUnit.MaxAttack + 1) * (attackingUnit.Amount / 2)) - defense;
            }

            if (damage < 0)
            {
                damage = 0;
            }
            int numberOfDead = 0;
            if (attackingUnit.IsFriendly != attackedUnit.IsFriendly)
            {
                attackedUnit.HP -= (damage);
                if (attackedUnit.HP <= 0)
                {
                    for (int i = 0; attackedUnit.HP < 0; i++)
                    {
                        attackedUnit.Amount--;
                        numberOfDead += 1;
                        attackedUnit.HP += attackedUnit.MaxHP;
                        if (attackedUnit.HP > 0)
                        {
                            break;
                        }


                    }
                    if (attackedUnit.Amount <= 0)
                    {
                        //Remove unit
                        WriteInCell(attackedUnit.UnitLocation, ConsoleColor.White);
                    }
                    else
                    {
                        WriteInCell(attackedUnit.UnitLocation, attackedUnit.Color, $" {attackedUnit.Symbol}", attackedUnit.Amount.ToString() + " ");

                    }
                }
            }
            Console.SetCursorPosition(0, 30);
            DeleteCurrentLine();
            Console.ForegroundColor = attackingUnit.Color;
            Console.Write($"{attackingUnit}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" deals {damage} to ");
            Console.ForegroundColor = attackedUnit.Color;
            Console.Write($"{attackedUnit}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(". ");
            Console.ForegroundColor = attackedUnit.Color;
            Console.Write($"{attackedUnit}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" looses {numberOfDead} troops.");
            Console.SetCursorPosition(0, 30);
            Thread.Sleep(2500);
            DeleteCurrentLine();
        }

        static void AIDecideDestination(Unit unit)
        {
            SetMoveTiles(unit);
            List<Unit> playerUnits = new List<Unit>();
            foreach (Unit u in mainTurn.UnitsTurn)
            {
                if (u.IsFriendly)
                {
                    playerUnits.Add(u);
                }
            }
            playerUnits.Sort(SortPlayerList);
            playerUnits.Reverse();
            if (unit.IsRanged && unit.Amount >= 0)
            {
                bool enemyClose = false;
                foreach (Neighbor neighbor in unit.Cell.Neighbors)
                {
                    if (!neighbor.Cell.IsEmpty && neighbor.Cell.Unit.IsFriendly)
                    {

                        enemyClose = true;
                        AttackUnit(unit, neighbor.Cell.Unit);
                        break;
                    }
                }

                if (!enemyClose && playerUnits[0].Cell.IsReachableRanged)
                {
                    AttackUnit(unit, playerUnits[0]);
                }
            }
            else
            {
                bool hasWalked = false;
                foreach (Unit u in friendlyUnits)
                {
                    if (u.Cell.IsReachable)
                    {
                        _MoveByDijsktra(unit, 0, new int[] { u.UnitLocation[0] + 1, u.UnitLocation[1] }, unit.UnitLocation);
                        WriteInCell(unit.UnitLocation, unit.Color);
                        WriteInCell(unit.UnitLocation, unit.Color, $" {unit.Symbol.ToString()}", unit.Amount.ToString());
                        AttackUnit(unit, u);
                        break;
                    }
                    else
                    {
                        bool canwalkNeighbour = false;

                        foreach (Neighbor neighbor in u.Cell.Neighbors)
                        {
                            if (neighbor.Cell.IsReachable && neighbor.Cell.IsEmpty)
                            {
                                _MoveByDijsktra(unit, 0, new int[] { neighbor.Cell.ColumnNumber, neighbor.Cell.Row }, unit.UnitLocation);
                                canwalkNeighbour = true;
                                hasWalked = true;
                                break;
                            }
                            else
                            {
                                foreach (Neighbor neighborInNeighbor in neighbor.Cell.Neighbors)
                                {
                                    if (neighborInNeighbor.Cell.IsReachable && neighborInNeighbor.Cell.IsEmpty)
                                    {

                                        _MoveByDijsktra(unit, 0, new int[] { neighborInNeighbor.Cell.ColumnNumber, neighborInNeighbor.Cell.Row }, unit.UnitLocation);
                                        canwalkNeighbour = true;
                                        hasWalked = true;
                                        break;
                                    }
                                }

                            }
                            if (hasWalked)
                            {
                                break;
                            }

                        }
                        if (!canwalkNeighbour)
                        {
                            bool canMove = true;
                            for (int i = 1; i < unit.Haste; i++)
                            {
                                if (!mainGrid.Cells[unit.UnitLocation[0] - i, unit.UnitLocation[1]].IsEmpty)
                                {
                                    canMove = false;
                                    break;
                                }

                            }
                            if (canMove)
                            {
                                _MoveByDijsktra(unit, 0, new int[] { unit.UnitLocation[0] - unit.Haste + 1, unit.UnitLocation[1] }, unit.UnitLocation);
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 30);
                                DeleteCurrentLine();
                                Console.ForegroundColor = unit.Color;
                                Console.Write($"{unit.Name}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($" holds.");
                                Console.SetCursorPosition(0, 30);
                                Thread.Sleep(2500);
                                DeleteCurrentLine();
                                break;
                            }

                        }
                    }
                    if (hasWalked)
                    {
                        break;
                    }


                }
            }
            foreach (Cell cell in mainGrid.Cells)
            {
                if (cell.IsReachable && cell.IsEmpty)
                {
                    WriteInCell(new int[] { cell.ColumnNumber, cell.Row }, ConsoleColor.White);
                }
            }
            WriteInCell(unit.UnitLocation, unit.Color);
            WriteInCell(unit.UnitLocation, unit.Color, $" {unit.Symbol.ToString()}", unit.Amount.ToString());



            static int SortPlayerList(Unit unit1, Unit unit2)
            {
                if (unit1 == null)
                {
                    if (unit2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (unit2 == null)
                    {
                        return 1;
                    }
                    else
                    {

                        int priority = unit1.Priority.CompareTo(unit2.Priority);
                        if (priority != 0)
                        {
                            return priority;
                        }
                        else
                        {
                            return unit1.Priority.CompareTo(unit2.Priority);
                        }



                    }
                }
            }
        }

    }
}