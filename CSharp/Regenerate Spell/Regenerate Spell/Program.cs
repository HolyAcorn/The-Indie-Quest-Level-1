using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regenerate_Spell
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var HP = random.Next(1, 100);
            Console.WriteLine($"Warrior HP: {HP}");

            Console.WriteLine("The Regenerate Spell is cast!");
            while(HP < 50)
            {
                HP += 10;
                Console.WriteLine($"Warrior HP: {HP}");
            }
            Console.WriteLine("The Regenerate Spell is completed!");
        }
    }
}
