using System;
using System.IO;

namespace Files_Mission2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] backers = File.ReadAllLines("backers.txt");

            Console.WriteLine("Yo yo yo motherclucker. Listen here, this is the secret entrance to the CrimeClub® Hideout. If you wanna come inside say your name.");
            Console.WriteLine();
            string name = Console.ReadLine();
            string output = "";
            foreach (string backer in backers)
            {
               
                if (backer == name)
                {
                    output = "Ait, you cool, welcome back mudderfucker.";
                    break;
                }
                else
                {
                    output = "Bro, you aint in the club, gtfo or I will tell on you!";
                    
                }
            }
            Console.WriteLine(output);
        }
    }
}
