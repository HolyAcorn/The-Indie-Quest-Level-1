using System;
using System.Collections.Generic;

namespace AlgorithmDesign2_Mission2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> partyMembers = new List<string> { "Viktor", "Matej", "Max", "Elsa" };

            Console.WriteLine("The original list is:");

            Console.WriteLine(string.Join(",", partyMembers));
            Console.WriteLine();

            WriteAllPermutations(partyMembers);


            /*List<string> names = new List<string> { "James", "Ben", "Allie", };
            Console.WriteLine("Signed-up Participants: ");
            foreach (string name in names)
            {
                Console.Write($"{name}, ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Permutations:");
            Console.WriteLine(WriteAllPermutations(names,0, -1));*/
        }
        /* static int FactorialImperitive(int n)
         {
             int runningTotal = 1;

             while (n != 0)
             {
                 runningTotal *= n;
                 n--;
             }
             return runningTotal;
         }

         static int Factorial(int n)
         {
             int nFactorial;
             if (n == 0)
             {
                 nFactorial = 1;
             }
             else
             {
                 nFactorial = n * Factorial(n - 1);

             }
             return nFactorial;
         }*/

        static void WriteAllPermutations(List<string> items)
        {

            WriteAllPermutations(items, 0);
        }

        static void WriteAllPermutations(List<string> items, int lockedIn)
        {
            if (lockedIn == items.Count)
            {
                Console.WriteLine(string.Join(",", items));
            }

            for (int i = lockedIn; i < items.Count; i++)
            {
                List<string> swappedItems = SwapItems(items, lockedIn, i);
                WriteAllPermutations(swappedItems, lockedIn + 1);
                
            }



        }

        static List<string> SwapItems(List<string> items, int a, int b)
        {
            List<string> swappedItems = new List<string>(items);

            (swappedItems[a], swappedItems[b]) = (swappedItems[b], swappedItems[a]);


            return swappedItems;
        }
    }
}
