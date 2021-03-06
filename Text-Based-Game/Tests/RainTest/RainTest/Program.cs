using System;
using System.Collections.Generic;
using System.Threading;
namespace The_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var streams = new List<int>();
            var symbols = @"|";

            for (int i = 0; i < 10; i++) streams.Add(random.Next(0, 10));
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            while (true)
            {
                for (int x = 0; x < 80; x++)
                {
                    Console.Write(streams.Contains(x) ? symbols[random.Next(symbols.Length)] : ' ');
                }

                Console.WriteLine();
                Thread.Sleep(100);


                if (random.Next(0, 3) == 0)
                {
                    streams.RemoveAt(random.Next(streams.Count));
                    streams.Add(random.Next(0, 80));
                }
            }
        }
    }
}