using System;
using System.Collections.Generic;

namespace Lists_n_shit
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string> {"murica", "Viktor", "totoro"};
            names.Add("Belladona");
            names.Add("Nathaniel");
            names.Remove("murica");
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}");
            }
            Console.WriteLine($"The list has {names.Count} people in it.");
            Console.WriteLine($"My name is {names[0]}");
            Console.WriteLine($"I have added {names[2]} and {names[3]} to the list.");
            
        }
    }
}
