using System;

namespace AlgorithimDesign_3
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int x = 0; x < 100; x++)
            {
                Console.WriteLine("Please input a whole number:");
                string input = Console.ReadLine();
                int inNumber = Convert.ToInt32(input);
                Console.WriteLine(OrdinalNumber(input, inNumber));
            }
            
        }

        static string OrdinalNumber(string text, int number)
        {
            int lastDigit = number % 10;
            string output = number.ToString();
            if (number > 10)
            {
                int secondDigit = number / 10 % 10;
                if (secondDigit == 1)
                {
                    output += "th";
                    return output;
                }
            
            }
            if (lastDigit == 1)
            {
                output += "st";
            }
            else if (lastDigit == 2)
            {
                output += "nd";
            }
            else if (lastDigit == 3)
            {
                output += "rd";
            }
            else
            {
                output += "th";
            }
            return output;
            

           
        }
    }
}
