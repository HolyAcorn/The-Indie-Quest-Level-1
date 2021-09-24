using System;

namespace Generate_Characters_and_Monsters
{
    class Program
    {
        static void Main(string[] args)
        {
            var dicecount = 2;
            var rolls = 0;
            var random = new Random();
            var dice = 0;
            var strength = 0;
            var hitPoints = 0;
            var cubeCount = 0;
            var totalHitPoints = 0;

            while (rolls < dicecount)
            {
                dice = random.Next(1, 7);
                strength += dice;
                rolls++;
            }
            Console.WriteLine($"A character with strength {strength} was created.");
            
            dicecount = 8;
            while (cubeCount < 100)
            {
                rolls = 0;
                while (rolls < dicecount)
                {
                    dice = random.Next(1, 11);
                    hitPoints = dice;
                    rolls++;
                }
                hitPoints += 40;
                totalHitPoints += hitPoints;
                cubeCount++;
            }
                
            Console.WriteLine($"A gelatinous cube with {hitPoints} HP appears!");
            Console.WriteLine($"Dear gods, an army of 100 cubes descends upon us with a total of {totalHitPoints} HP. We are doomed!");
        }
    }
}
