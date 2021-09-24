using System;

namespace Switch_Mission1
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Console.Write("Set the price bitch: ");
            string input = Console.ReadLine();
            string[] arrayInput = input.Split(" ");
            double x = Convert.ToDouble(arrayInput[0]);
            double y = Convert.ToDouble(arrayInput[2]);

            string symbol = arrayInput[1];
            double output = 0;
            switch (symbol)
            {
                case "+":
                case "plus":
                    output = x + y;
                    break;
                case "-":
                case "minus":
                    output = x - y;
                    break;
                case "*":
                case "multiplied":
                    output = x * y;
                    break;
                case "/":
                case "Divided":
                    output = y / x;
                    break;

            }
            
            Console.WriteLine($"The shit price was set to: {output}");


        }


    }
}
