using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionaries_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var cities = new Dictionary<string, string> {
                {"Afghanistan", "Kabul" },
                {"Albania", "Tirana" },
                {"Algeria", "Algiers" },
                {"American Samoa", "Pago Pago" },
                {"Andorra", "Andorra la Vella" }
            };
            Random random = new Random();
            int cityNumber = random.Next(0, cities.Count);
            string correctKey = cities.Keys[cityNumber];
            string correctValue = cities.Values.ElementAt(cityNumber);
            Console.WriteLine($"What is the capital city of {correctKey}");
            string input = Console.ReadLine();
            if (input == correctValue)
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Oooh, sorry, the correct answer is {correctValue}");
            }
        }
    }
}
