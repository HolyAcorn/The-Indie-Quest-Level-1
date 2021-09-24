using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionaries_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var olympics = new Dictionary<string, string>()
            {
                { "2000", "Australia" },
                {"2002", "United States" },
                {"2004", "Greece" },
                { "2006", "Italy"},
                {"2008", "China" },
                {"2010", "Canada" },
                {"2012", "United Kingdom" },
                {"2014", "Russia" },
                { "2016", "Brazil" },
                {"2018", "South Korea" },
                {"2020", "Japan" },
            };
            Random random = new Random();
            int input = random.Next(0, olympics.Count);
            Console.WriteLine($"Where was the {olympics.ElementAt(input).Key}?");
            string playerGuess = Console.ReadLine();
            if(playerGuess == olympics.ElementAt(input).Value)
            {
                Console.WriteLine("Correct! :D");
            }
            else
            {
                Console.WriteLine($"Oh no, I'm sorry, the correct answer is {olympics.ElementAt(input).Value}");
            }
        }
    }
}
