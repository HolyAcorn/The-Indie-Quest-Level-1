using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
   

namespace RegEx_Mission1
{
    class Program
    {
        static void Main(string[] args)
        {
            string monsterManual = "MonsterManual.txt";

            var readText = File.ReadAllText(monsterManual);
            var namesByAlignment = new List<string>[3, 3];
            var namesOfUnaligned = new List<string> ();
            var namesOfAnyAlignment = new List<string>();
            var namesOfSpecialCases = new List<string>();

            string[] monsters = readText.Split("\n\n");

            for (int axis1 = 0; axis1 < 3; axis1++)
            {
                for (int axis2 = 0; axis2 < 3; axis2++)
                {
                    namesByAlignment[axis1, axis2] = new List<string>();
                }
            }

            void SetAlignmentMatch2(Match alignMatch, int matchIndex, string monsterName)
            {
                switch(alignMatch.Groups[1].Value)
                {
                    case "good":
                        namesByAlignment[matchIndex, 0].Add(monsterName);
                        break;
                    case "neutral":
                        namesByAlignment[matchIndex, 1].Add(monsterName);
                        break;
                    case "evil":
                        namesByAlignment[matchIndex, 2].Add(monsterName);
                        break;
                }
            }

            foreach (var monster in monsters)
            {
                string namePattern = @"^[\w ]+";
                Match nameMatch = Regex.Match(monster,namePattern);

                string alignmentRegex1 = @"(lawful|chaotic|neutral)\s";
                string alignmentRegex2 = @"(good|neutral|evil)\n";


                Match alignMatch = Regex.Match(monster, alignmentRegex1);
                Match alignMatch2 = Regex.Match(monster, alignmentRegex2);

                    switch (alignMatch.Groups[1].Value)
                    {
                        case "lawful":
                            SetAlignmentMatch2(alignMatch2, 0, nameMatch.Value);
                            break;
                        case "neutral":
                            SetAlignmentMatch2(alignMatch2, 1, nameMatch.Value);
                            break;
                        case "chaotic":
                            SetAlignmentMatch2(alignMatch2, 2, nameMatch.Value);
                            break;
                    }

                    string unalignedRegex = @"unaligned";
                    if (Regex.IsMatch(monster, unalignedRegex))
                    {
                        namesOfUnaligned.Add(nameMatch.Value);
                    }
                    string anyAlignRegex = @"any alignment";
                    if (Regex.IsMatch(monster, anyAlignRegex))
                    {
                        namesOfAnyAlignment.Add(nameMatch.Value);
                    }
                    string specialRegex = @"any non-.*\n";
                    Match specialMatch = Regex.Match(monster, specialRegex);
                    if (specialMatch.Success)
                    {
                        namesOfSpecialCases.Add(String.Join(" - ", nameMatch.Value, specialMatch.Value));
                    }
            
                

                /*if (Regex.IsMatch(monster, alignmentMatch1))
                {
                    if (alignMatch.Value == "lawful")
                    {
                        
                    }
                }*/

                /* Mission 3
                string dicePattern = @"\d{2}d";

                Console.WriteLine($"{nameMatch} - 10+ dice rolls: {Regex.IsMatch(monster, dicePattern)}");*/



                /*if (Regex.IsMatch(monster, @"fly [1-4]\d\s"))
                {

                    Console.WriteLine(nameMatch.Value);
                }*/

            }
            List<string> alignText = new List<string> { "lawful good", "lawful neutral", "lawful evil", "neutral good", "true neutral", "neutral evil", "chaotic good", "chaotic neutral", "chaotic evil" };
            int a = 0;
            foreach (List<string> monsterList in namesByAlignment)
            {
                
                Console.WriteLine($"Monsters with the alignment {alignText[a]} are:");
                foreach (string monster in monsterList)
                {
                    Console.WriteLine(monster);
                }
                a++;
                Console.WriteLine();
            }
            Console.WriteLine("Monsters with any alignment are:");
            foreach (string monster in namesOfAnyAlignment)
            {
                Console.WriteLine(monster);
            }
            Console.WriteLine();
            Console.WriteLine("Monsters that are unaligned are:");
            foreach (string monster in namesOfUnaligned)
            {
                Console.WriteLine(monster);
            }
            Console.WriteLine();
            Console.WriteLine("Special cases are:");
            foreach (string monster in namesOfSpecialCases)
            {
                Console.Write(monster);
            }
        }
    }
}

