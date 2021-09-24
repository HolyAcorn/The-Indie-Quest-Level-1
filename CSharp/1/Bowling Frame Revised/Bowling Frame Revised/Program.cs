using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Frame_Revised
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int totalPins = 10;
            int firstRoll = random.Next(0, totalPins + 1);
            int secondRoll = random.Next(0, totalPins + 1 - firstRoll);
            string firstString = firstRoll.ToString();
            string secondString = secondRoll.ToString();
            int knockedPins = 0;
            firstString = firstString.Replace("10", "X");
            firstString = firstString.Replace("0", "-");
            secondString = secondString.Replace("0", "-");

            Console.WriteLine($"First roll: {firstString}");
            knockedPins += firstRoll;
            totalPins -= firstRoll;
            if (knockedPins != 10)
            {
                if (secondRoll == totalPins)
                {
                    Console.WriteLine("Second roll: /");
                }
                else
                {
                    Console.WriteLine($"Second roll: {secondString}", secondString.Replace("10", "X"), secondString.Replace("0", " - "));
                }
                knockedPins += secondRoll;
            }
            Console.WriteLine($"Knocked pins: {knockedPins}");
        }
    }
}
