using System;
using System.IO;

namespace Files_Mission1
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerNamePath = "player-name.txt";
            if (File.Exists(playerNamePath))
            {
                
                string readText = File.ReadAllText(playerNamePath);
                Console.WriteLine($"Look bro, I know your name is {readText}, but I'm just gonna call you bitch, bitch!");
                
            }
            else
            {
                Console.WriteLine("Yo bitch, litsen here, if you gonna join our little CrimeClub®, then you're gonna have to tell us your name!");
                string input = Console.ReadLine();
                File.WriteAllText(playerNamePath, input);
                Console.WriteLine("Welcome to the CrimeClub® " + input + "!");
            }
        }
    }
}
