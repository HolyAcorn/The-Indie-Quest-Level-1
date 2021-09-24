using System;
using System.Collections.Generic;

namespace Dictionaries_Mission_3_BossLevel
{
    class Program
    {
        static IDictionary<string, int> participants = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Who won this round?, or if you'd rather (q)uit.");
                string input = Console.ReadLine();
                if (input == "q" || input == "Q")
                {
                    Environment.Exit(0);
                }
                Console.WriteLine();
                addScore(input);
                
            }
            
        }

        static void addScore(string player)
        {
            if (participants.ContainsKey(player))
            {
                int score = participants[player];
                participants.Remove(player);
                participants.Add(player, score +1);
            }
            else
            {
                participants.Add(player, 1);
            }
            string[] players = new List<string>(participants.Keys).ToArray();
            int[] scores = new List<int>(participants.Values).ToArray();
            Array.Sort(scores, players);
            Array.Reverse(players);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RANKINGS:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var scorePlayer in players)
            {
                Console.WriteLine($"{scorePlayer}, {participants[scorePlayer]}");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            
            
        }
    }
}
