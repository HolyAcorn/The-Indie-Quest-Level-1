using System;

namespace Arrays_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dasVariabel = {2, 5, 6, 4, 22, 13, 29, 18, 7, 9};
            Array.Sort(dasVariabel, 0, 9);
            Console.WriteLine("[{0}]", string.Join(", ", dasVariabel));
        }

    }
}
