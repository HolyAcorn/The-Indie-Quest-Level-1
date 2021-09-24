using System;
using System.Text.RegularExpressions;

namespace DiceSimulator2_Mission3
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "To use the magic potion of Dragon Breath, first roll d8. If you roll 2 or higher, you manage to open the potion.Now roll 5d4 + 5 to see how many seconds the spell will last.Finally, the damage of the flames will be 2d6 per second.";
            string diceRegex = @"(\d+)?d(\d+)[+]?(\d)?";
            MatchCollection diceMatches = Regex.Matches(input, diceRegex);
            Console.WriteLine($"{diceMatches.Count} standard dice notations present.");
            int numberOfRolls = 0;
            foreach (Match item in diceMatches)
            {
                if (item.Groups[1].Value != "")
                {
                    numberOfRolls += Convert.ToInt32(item.Groups[1].Value);
                }
                else
                {
                    numberOfRolls += 1;
                }
                
            }
            Console.WriteLine($"The player will have to perform {numberOfRolls} rolls.");
        }
    }
}
