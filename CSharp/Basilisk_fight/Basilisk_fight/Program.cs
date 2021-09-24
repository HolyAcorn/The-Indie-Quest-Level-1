using System;
using System.Collections.Generic;

namespace Basilisk_fight
{
    class Program
    {
        static List<string> pcNames = new List<string> { "Jazlyn", "Theron", "Dayana", "Rolando" };
        static List<string> enemyTypes = new List<string> { "Orc", "Mage", "Troll" };
        static List<int> enemyHPs = new List<int> { 15, 40, 84 };
        static int conSave = 0;
        static List<int> dcValue = new List<int> { 12, 20, 18 };
        static Random random = new Random();
        static void Main(string[] args)
        {





            Console.Write("A party of warriors {0}", string.Join(", ", pcNames));
            Console.Write(" descends into the dungeon.");
            Console.WriteLine();
            while(enemyTypes.Count > 0 && pcNames.Count > 0)
            {
                SimulateBattle(pcNames, enemyTypes[0], enemyHPs[0], dcValue[0]);

            }
            if (pcNames.Count > 0)
            {
                Console.WriteLine("The players leave the dungeon, the surviving members are {0}.", string.Join(", ", pcNames));
            }
            else
            {
                Console.WriteLine("The heroes all die in the dungeon.");
            }

        }
        static void SimulateBattle(List<string> pcNames, string enemy, int enemyTotalHP, int savingThrowDC)
        {

            int hitTarget = 0;
            
            int greatsword = 0;
            
            Console.WriteLine($"A {enemy} with {enemyTotalHP} HP appears!");

            while (enemyTotalHP > 0)
            {
                foreach (string name in pcNames)
                {
                    greatsword = DiceRoll(2,6);
                    enemyTotalHP -= greatsword;
                    if (enemyTotalHP < 0 || enemyTotalHP == 0)
                    {
                        enemyTotalHP = 0;
                        Console.WriteLine($"{name} hits the {enemy} for {greatsword}. The {enemy} has {enemyTotalHP} HP left.");
                        break;
                    }
                    Console.WriteLine($"{name} hits the {enemy} for {greatsword}. The {enemy} has {enemyTotalHP} HP left.");

                }
                hitTarget = random.Next(0, pcNames.Count);
                conSave = DiceRoll(1, 20, 5);
                Console.WriteLine($"The {enemy} attacks {pcNames[hitTarget]}. They roll a constituion save with DC {savingThrowDC} and rolls {conSave}");
                if (conSave < savingThrowDC)
                {
                    Console.WriteLine($"{pcNames[hitTarget]} fails their check and is killed. :c");
                    Console.WriteLine();
                    pcNames.RemoveAt(hitTarget);
                    if (pcNames.Count == 0)
                    {
                        Console.WriteLine("Game over. :c");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"{pcNames[hitTarget]} succeeds their saving throw.");
                    Console.WriteLine();
                }
            }
            if (enemyTotalHP == 0)
            {
                Console.WriteLine("The enemy collapses and the heroes celebrate their victory! :D");
                enemyHPs.RemoveAt(0);
                enemyTypes.RemoveAt(0);
                dcValue.RemoveAt(0);
                Console.WriteLine();
            }
        }
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            int result = 0;
            for (int i = 0; i < numberOfRolls; i++) 
            {
                result += random.Next(1, diceSides + 1);
            }
            result += fixedBonus;
            return result;
        }
    }
}
