using System;
using System.Collections.Generic;

namespace CharacterGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> abilityScores = new List<int> { };
            int totalScores = 6;
            for (int x = 0; x < totalScores; ++x) 
            {
                List<int> diceGeneration = new List<int> {0,0,0,0 };
                Random random = new Random();
                int finalStat = 0;
                string rollText = "You roll ";
                for (int dice = 0; dice < diceGeneration.Count; ++dice)
                {
                    int singleDice = random.Next(1, 7);
                    diceGeneration[dice] = singleDice;
                    rollText = rollText + diceGeneration[dice];
                    if (dice == 3)
                    {
                        rollText = rollText + ". ";
                    }
                    else
                    {
                        rollText = rollText + ", ";
                    }

                }
                diceGeneration.Sort();
                diceGeneration.RemoveAt(0);
                
                foreach (var i in diceGeneration)
                {
                    finalStat += i;
                }
                rollText = rollText + "The ability score is " + finalStat;
                Console.WriteLine(rollText);
                abilityScores.Add(finalStat);
                
            }
            abilityScores.Sort();
            string stringscores = string.Join(", ", abilityScores);
            Console.WriteLine($"Your available ability scores are: {stringscores}");
        }
    }
}
