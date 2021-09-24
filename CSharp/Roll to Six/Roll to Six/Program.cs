using System;

namespace Roll_to_Six
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var dice = 0;
            var score = 0;

            while (dice != 6)
            {
                dice = random.Next(1, 7);
                Console.WriteLine($"The Player rolls: {dice}");
                score += dice;
            }
            Console.WriteLine($"Total score: {score}");
        }
    }
}
