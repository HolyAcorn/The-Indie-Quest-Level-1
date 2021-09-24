using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace MonsterManual
{
    enum ArmorType
    {
        Unspecified,
        Natural,
        Leather,
        StuddedLeather,
        Hide,
        ChainShirt,
        ChainMail,
        ScaleMail,
        Plate,
        Other
    }
    enum ArmorCategory
    {
        None,
        Light,
        Medium,
        Heavy
    }
    class ArmorTypeEntry
    {
        public string Name;
        public ArmorInformation Type = new ArmorInformation();
        public ArmorCategory Category;
        public int Weight;
    }
    class ArmorInformation
    {
        public int Class;
        public ArmorType Type;
    }

    class Monster
    {
        public string Name;
        public string Description;
        public string Alignment;
        public string HitPoints;
        public ArmorTypeEntry Armor = new ArmorTypeEntry();
    }
    class Program
    {

        //Text strings
        //Welcoming the player
        static string welcome1 = "Welcome traveler...";
        static string welcome2 = " You seek to learn from the ancient texts of the Monster Manual, hm?";
        static string welcome3 = "Go on then!";
        static string welcome4 = "Tell me what you are searching for:";
        //Ask players what they would like to use to search for.
        static string armorOrMonster = " First I need to know if you wish to search for (n)ames or for (a)rmor?";
        //When multiple Results are found
        static string foundMultipleResults1 = "There are many creatures that go by that name.";
        static string foundMultipleResults2 = "Creatures that fit that name are:";
        //When only 1 result is found.
        static string foundOneResult1 = "I have found your the creature you are looking for.";
        static string foundOneResult2 = "Here is all I know of it:";
        //When no results are found.
        static string noFoundResult = "There are no monsters that carry such a name...";
        // When player needs to make a choice.
        static string makeChoice = "Please, tell me which you would like to hear more about:";
        // When the player chose an incorrect option.
        static string incorrectOption = "That is not one of the options I gave you you arrogant traveler!";
        // Asking the player to try again.
        static string tryAgainText = "Would you like to try again?";
        //Before outputing final selection (Only when multiple options were found previously.)
        static string finalSelection = "Here is all I know on the monster you selected:";

        static List<Monster> monsterManual = new List<Monster> { };

        static ConsoleColor inputColor = ConsoleColor.DarkRed;
        static ConsoleColor textColor = ConsoleColor.DarkCyan;
        static ConsoleColor optionsColor = ConsoleColor.DarkGray;
        static ConsoleColor resultColor = ConsoleColor.DarkGreen;

        static int charTimer = 25; // should be 25
        static int longCharTimer = 75; // should be 75
        static int extraLongCharTimer = 125; // should be 125

        static int rowTime = 1000; // should be 1000
        static int rowLongTime = 1500; // should be 1500
        static int rowExtraLongTime = 3000; // should be 3000

        static int optionCharTimer = 2; //Should be 2
        static int optionRowTimer = 10; //Should be 10

        static void Main(string[] args)
        {
            _DebugDisableTextCrawl(); //TODO: TURN OFF WHEN BUILDING! ONLY FOR DEBUG!

            // Title Generation
            string[] title = new string[] { @"   __________________________________________________________________________",
            @" / \                             					     \.",
            @"|   |                            					     |.",
            @" \_ |                            					     |.",
            @"    |              ___  ________ _   _ _____ _____ ___________ 		     |.",
            @"    |   	   |  \/  |  _  | \ | /  ___|_   _|  ___| ___ \		     |.",
            @"    |   	   | .  . | | | |  \| \ `--.  | | | |__ | |_/ /		     |.",
            @"    |   	   | |\/| | | | | . ` |`--. \ | | |  __||    / 		     |.",
            @"    |   	   | |  | \ \_/ / |\  /\__/ / | | | |___| |\ \ 		     |.",
            @"    |   	   \_|  ___\____\___\______/_ \__ \____/__| \_|		     |.",
            @"    |   	        |  \/  |/ _ \| \ | | | | |/ _ \| |     		     |.",
            @"    |   	        | .  . / /_\ \  \| | | | / /_\ \ |     		     |.",
            @"    |   	        | |\/| |  _  | . ` | | | |  _  | |     		     |.",
            @"    |   	        | |  | | | | | |\  | |_| | | | | |____ 		     |.",
            @"    |   	        \_|  |_|_| |_|_| \_/\___/\_| |_|_____/               |.",
            @"    |                            					     |.",
            @"    |                                                                        |.",
            @"    |   _____________________________________________________________________|___",
            @"    |  /                                                                     	/.",
            @"    \_/________________________________________________________________________/."};

            int titleCharTimer = 1; // should be 1
            int titleRowTimer = 75; // Should be 75
            int rowCounter = 0;
            foreach (string row in title)
            {
                int charCounter = 0;

                foreach (char c in row)
                {

                    if (charCounter > 7 && charCounter < 62 && rowCounter < 15 && rowCounter > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write(c);
                    Thread.Sleep(titleCharTimer);
                    charCounter++;
                }
                Console.WriteLine();
                Thread.Sleep(titleRowTimer);
                rowCounter++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(rowExtraLongTime);

            string manualFilePath = "MonsterManual.txt";
            string monsterFileString = File.ReadAllText(manualFilePath);
            string[] monsterFile = File.ReadAllLines(manualFilePath);

            int entryLineLength = 7;

            string armorFilePath = "ArmorTypes.txt";
            string[] armorFileString = File.ReadAllLines(armorFilePath);
            var armorTypeEntries = new Dictionary<ArmorType, ArmorTypeEntry>();
            string armorTypeText = "Plate";
            ArmorType armorType = Enum.Parse<ArmorType>(armorTypeText);
            foreach (string line in armorFileString)
            {
                string[] lineArray = line.Split(',');
            }
            

            string descRegex = @"\D+\n(\D+[)]?[^Armor]?), (.*)";
            string hitRegex = @"oints: (\d+) [(]?(\d+d?\d+ ?[+]?[-]? ?(?:\d+)?)?";
            string monsterRegex = @"((.*)\n.*\n.*\n*\n.*\n.*\n.*)\n\n?";
            string armorRegex = @"Armor Class: (\d+) [(]?(\w+ ?\w+)?";
            MatchCollection monsterMatch = Regex.Matches(monsterFileString, monsterRegex);
            MatchCollection descMatch = Regex.Matches(monsterFileString, descRegex);
            MatchCollection hitMatch = Regex.Matches(monsterFileString, hitRegex);
            MatchCollection armorMatch = Regex.Matches(monsterFileString, armorRegex);



            //Setup the Monster List
            for (int m = 0; m < monsterFile.Length / entryLineLength + 1; m++)
            {
                Monster monster = new Monster();
                monster.Name = monsterMatch[m].Groups[2].Value;
                monster.Description = descMatch[m].Groups[1].Value;
                monster.Alignment = descMatch[m].Groups[2].Value;
                monster.HitPoints = hitMatch[m].Groups[2].Value;
                monster.Armor.Type.Class = Convert.ToInt32(armorMatch[m].Groups[1].Value);
                if (armorMatch[m].Groups[2].Success)
                {

                    switch (armorMatch[m].Groups[2].Value)
                    {
                        case "Natural Armor":
                        case "natural armor":
                            monster.Armor.Type.Type = ArmorType.Natural;
                            monster.Armor.Name = "Natural Armor";
                            break;
                        case "Leather Armor":
                        case "leather armor":
                            monster.Armor.Type.Type = ArmorType.Leather;
                            monster.Armor.Category = ArmorCategory.Light;
                            monster.Armor.Name = "Leather";
                            monster.Armor.Weight = 10;
                            break;
                        case "Studded Leather":
                        case "studded leather":
                            monster.Armor.Type.Type = ArmorType.StuddedLeather;
                            monster.Armor.Category = ArmorCategory.Light;
                            monster.Armor.Name = "Studded Leather";
                            monster.Armor.Weight = 13;
                            break;
                        case "Hide Armor":
                        case "hide armor":
                            monster.Armor.Type.Type = ArmorType.Hide;
                            monster.Armor.Category = ArmorCategory.Medium;
                            monster.Armor.Name = "Hide";
                            monster.Armor.Weight = 12;
                            break;
                        case "Chain Shirt":
                        case "chain shirt":
                            monster.Armor.Type.Type = ArmorType.ChainShirt;
                            monster.Armor.Category = ArmorCategory.Light;
                            monster.Armor.Name = "Leather Shirt";
                            monster.Armor.Weight = 20;
                            break;
                        case "Chain Mail":
                        case "chain mail":
                            monster.Armor.Type.Type = ArmorType.ChainMail;
                            monster.Armor.Category = ArmorCategory.Heavy;
                            monster.Armor.Name = "Chain Mail";
                            monster.Armor.Weight = 55;
                            break;
                        case "Scale Mail":
                        case "scale mail":
                            monster.Armor.Type.Type = ArmorType.ScaleMail;
                            monster.Armor.Category = ArmorCategory.Light;
                            monster.Armor.Name = "Leather";
                            monster.Armor.Weight = 10;
                            break;

                        case "Plate":
                        case "plate":
                        case "Breastplate":
                            monster.Armor.Type.Type = ArmorType.Plate;
                            monster.Armor.Category = ArmorCategory.Heavy;
                            monster.Armor.Name = "Plate";
                            monster.Armor.Weight = 65;
                            break;
                        default:
                            monster.Armor.Name = "None";
                            monster.Armor.Type.Type = ArmorType.Other;
                            monster.Armor.Category = ArmorCategory.None;
                            break;

                    }
                }
                else
                {
                    monster.Armor.Type.Type = ArmorType.Unspecified;
                }
                monsterManual.Add(monster);
            }

            //Welcome generation & Query Input
            Console.ForegroundColor = textColor;




            foreach (char c in welcome1)
            {
                Console.Write(c);
                Thread.Sleep(charTimer);
            }
            Thread.Sleep(rowTime);
            foreach (char c in welcome2)
            {
                Console.Write(c);
                Thread.Sleep(charTimer);
            }
            Thread.Sleep(rowLongTime);
            Console.WriteLine();
            foreach (char c in welcome3)
            {
                Console.Write(c);
                Thread.Sleep(charTimer);
            }
            Thread.Sleep(rowLongTime);

            bool searchByArmor = false;
            foreach (char c in armorOrMonster)
            {
                Console.Write(c);
                Thread.Sleep(charTimer);
            }
            Console.WriteLine();
            Console.ForegroundColor = inputColor;
            string aomInput = Console.ReadLine();
            Console.ForegroundColor = textColor;
            if (aomInput == "n" || aomInput == "N")
            {
                searchByArmor = false;
            }
            else if (aomInput == "a" || aomInput == "A")
            {
                searchByArmor = true;
            }
            else if (aomInput == "")
            {
                new ArgumentException(incorrectOption);
            }
            {
                new ArgumentException(incorrectOption);
            }

            bool repeatQuery = true;
            while (repeatQuery)
            {

                List<Monster> queriedMonsters = new List<Monster> { };
                if (searchByArmor)
                {
                    string[] armorTypeNames = Enum.GetNames(typeof(ArmorType));
                    int armorTypeCounter = 1;
                    foreach (string name in armorTypeNames)
                    {
                        Console.ForegroundColor = optionsColor;
                        string option = $"{armorTypeCounter}: {name}";
                        foreach (char c in option)
                        {

                            Console.Write(c);
                            Thread.Sleep(optionCharTimer);
                        }
                        Thread.Sleep(optionRowTimer);
                        Console.WriteLine();
                        armorTypeCounter++;
                    }

                    TakeOption(queriedMonsters, searchByArmor, armorTypeNames);



                }
                else
                {
                    Console.ForegroundColor = textColor;
                    Thread.Sleep(rowTime);
                    foreach (char c in welcome4)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Console.ForegroundColor = inputColor;
                    string inputQuery = Console.ReadLine();
                    Console.ForegroundColor = textColor;
                    string lowerInputQuery = inputQuery.ToLower();
                    foreach (Monster monster1 in monsterManual)
                    {
                        string lowerCaseName = monster1.Name.ToLower();
                        if (lowerCaseName.Contains(lowerInputQuery))
                        {
                            queriedMonsters.Add(monster1);
                        }
                    }
                }

                if (queriedMonsters.Count > 0)
                {
                    repeatQuery = false;
                    string resultName = $"Name: ";
                    string resultDesc = $"Description: ";
                    string resultAlign = $"Alignment: ";
                    string resultHit = $"Hitpoints: ";
                    if (queriedMonsters.Count > 1)
                    {
                        foreach (char c in foundMultipleResults1)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        foreach (char c in foundMultipleResults2)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        Console.ForegroundColor = optionsColor;
                        for (int i = 0; i < queriedMonsters.Count; i++)
                        {
                            string tempMonster = $"{i + 1}: {queriedMonsters[i].Name}";
                            foreach (char c in tempMonster)
                            {
                                Console.Write(c);
                                Thread.Sleep(optionCharTimer);
                            }
                            Console.WriteLine();
                            Thread.Sleep(optionRowTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        Console.ForegroundColor = textColor;
                        foreach (char c in makeChoice)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        TakeOption(queriedMonsters);





                    }
                    else
                    {
                        foreach (char c in foundOneResult1)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        foreach (char c in foundOneResult2)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        Console.WriteLine();
                        SelectOption(queriedMonsters);


                    }



                }
                else
                {
                    Console.WriteLine();
                    foreach (char c in noFoundResult)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Thread.Sleep(rowTime);
                    Console.WriteLine();
                    foreach (char c in tryAgainText)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Thread.Sleep(rowTime);
                    Console.WriteLine();
                }
            }

        }

        static void SelectOption(List<Monster> queriedMonsters, int selection = 1)
        {
            selection--;
            if (selection <= queriedMonsters.Count)
            {
                Monster selectedMonster = queriedMonsters[selection];


                string[] finalResult = new string[] { $"Name: {queriedMonsters[selection].Name}",
                    $"Description: {queriedMonsters[selection].Description}",
                    $"Alignment: {queriedMonsters[selection].Alignment}",
                    $"Hitpoints: {queriedMonsters[selection].HitPoints}",
                    $"Armor Class: {queriedMonsters[selection].Armor.Type.Class}",
                    $"Armor Type: {queriedMonsters[selection].Armor.Name}",
                    $"Armor Category: {queriedMonsters[selection].Armor.Category}",
                    $"Armor Weight: {queriedMonsters[selection].Armor.Weight}"};
                Console.ForegroundColor = resultColor;
                Console.WriteLine();
                foreach (string item in finalResult)
                {

                    foreach (char c in item)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Thread.Sleep(rowTime);
                    Console.WriteLine();
                }
            }
        }

        static List<Monster> SelectArmorType(List<Monster> monsterList, ArmorType armorType)
        {
            return monsterList;
        }

        static void TakeOption(List<Monster> queriedMonsters, bool searchByArmor = false, string[] armorTypes = null)
        {
            bool tryAgain = true;
            while (tryAgain)
            {
                Console.ForegroundColor = inputColor;
                string inputOption = Console.ReadLine();
                Console.ForegroundColor = textColor;
                int selectedOption = 0;
                try
                {
                    selectedOption = Convert.ToInt32(inputOption);
                    if (searchByArmor)
                    {
                        if (selectedOption > armorTypes.Length)
                        {
                            throw new ArgumentException(incorrectOption);
                        }
                        tryAgain = false;
                        foreach (Monster monster1 in monsterManual)
                        {

                            if (monster1.Armor.Type.Type.ToString() == armorTypes[selectedOption - 1])
                            {
                                queriedMonsters.Add(monster1);
                            }
                        }
                    }
                    else
                    {
                        if (selectedOption > queriedMonsters.Count)
                        {
                            throw new ArgumentException(incorrectOption);
                        }
                        tryAgain = false;
                        foreach (char c in finalSelection)
                        {
                            Console.Write(c);
                            Thread.Sleep(charTimer);
                        }
                        Thread.Sleep(rowTime);
                        SelectOption(queriedMonsters, selectedOption);
                    }


                    
                }
                catch (Exception e)
                {

                    new ArgumentException(incorrectOption);
                    Console.WriteLine();
                    foreach (char c in e.Message)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);

                    }
                    Thread.Sleep(rowTime);
                    Console.WriteLine();
                    foreach (char c in tryAgainText)
                    {
                        Console.Write(c);
                        Thread.Sleep(charTimer);
                    }
                    Console.WriteLine();
                }
            }
        }

        static void _DebugDisableTextCrawl()
        {
            charTimer = 0;
            longCharTimer = 0;
            extraLongCharTimer = 0;

            rowTime = 0;
            rowLongTime = 0;
            rowExtraLongTime = 0;

            optionCharTimer = 0;
            optionRowTimer = 0;
        }
    }
}
