using System;
using System.Collections.Generic;

namespace Algorithm_Design_Mission2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> players = new List<string>()
            { 
                "Viktor",
                "Marnix",
                "Gommes",
                "Sebastian",
                "Arthur"
            };
            string outputText = "";

            outputText = $"The heroes in the party are: {JoinWithAnd(players, false)}.";
            Console.WriteLine(outputText);
        }
        static string JoinWithAnd(List<string> items, bool useSerialComma = true)
        {
            int count = items.Count;
            string finalOutput = "";
            if (count == 0)
            {
                return "";
            }
            if (count == 1)
            {
                return items[0];
            }
            if (count >= 2)
            {
                for (int x = 0; x < count; x++)
                {

                    if (useSerialComma)
                    {
                        finalOutput += items[x] + ", ";
                    }
                    else
                    {

                        
                        if (x < count - 2)
                        {
                            finalOutput += items[x] + ", ";
                        }
                        else
                        {
                            if (x != count - 1)
                            {
                                finalOutput += items[x] + " and ";
                                
                            }
                            else
                            {
                                finalOutput += items[x];
                            }
                          
                        }
                    }
                    
                }
                
            }
            return finalOutput;
        }
    }
}
