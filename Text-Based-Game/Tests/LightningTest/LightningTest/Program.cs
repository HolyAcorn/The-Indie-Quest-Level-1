using System;
using System.Threading;
using System.IO;
using System.Media;

namespace LightningTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string lightningFile = "Lightning.txt";
            string[] lightning = File.ReadAllLines(lightningFile);

            SoundPlayer lightningSFX = new SoundPlayer("LightningStrike2.wav");
            lightningSFX.Load();
            lightningSFX.Play();

            string[] introTextShort = new string[] 
            { "Ten years ago, the first hellgate was opened in the lands of Reydia by a mad mage.",
                "Two years later, Reydia w"
            }; 
            string[] introTextRest = new string[] 
            { "as nothing but ruins.",
                "Every year, more and more hellgates are opened, and the demons keep coming.",
                "Three nations still stand, Denblum, Crithain and Trestria.",
                "These three nations have banded together and searched far an wide to find the greatest men and women ",
                "to lead their armies against the hordes of hell.",
                "The Champions stand as the final hope for humanity."
            };
            WriteCool(introTextShort, 50, 500);
            //Thread.Sleep(4810);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string line in lightning)
            {
                Console.WriteLine(line);
            }
            Thread.Sleep(150);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(introTextShort[0]);
            Console.Write(introTextShort[1]);

            WriteCool(introTextRest, 50, 500);
            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(750);
                Console.Write('.');
            }
            Console.ReadKey();
        }

        static void WriteCool(string[] lines, int charSpeed, int rowSpeed)
        {
            int lineCount = 0;
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(charSpeed);
                }
                if (lineCount != lines.Length-1)
                {
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
                lineCount++;
                
                Thread.Sleep(rowSpeed);
            }
        }
    }
}
