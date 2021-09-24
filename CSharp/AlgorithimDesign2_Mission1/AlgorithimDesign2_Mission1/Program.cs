using System;
using System.Collections.Generic;

namespace AlgorithimDesign2_Mission1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> partyList = new List<string>
            {
                "Viktor",
                "Matej",
                "Anni",
                "Andjela",
                "Marnix",
                "Gustaf",
                "Gommes",
                "Hilda",
                "Sebastian"
            };
            Console.WriteLine("The participants are: ");
            foreach (string name in partyList)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            Console.WriteLine("The starting order is: ");
            List<string> newList = ShuffleList(partyList);
            foreach (string name in newList)
            {
                Console.WriteLine(name);
            }

        }

        static List<string> ShuffleList(List<string> items)
        {
            int count = items.Count;
            Random random = new Random();
            for(int i = count - 1; i >= 0; --i)
            {
                int j = random.Next(i, count);
                string tempI = items[i];
                items[i] = items[j];
                items[j] = tempI;
            }
            return items;
        }
    }
}
