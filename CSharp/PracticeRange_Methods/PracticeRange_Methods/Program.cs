using System;

namespace PracticeRange_Methods
{
    class Program
    {
        public const double PI = 3.1415926535897931;
        static void Main(string[] args)
        {
            Console.WriteLine($"1: 4 + 3 = { Add(4, 3)}");
            Console.WriteLine($"2: 10 / 2 = {SafeDivision(10,2)}");
            Console.WriteLine($"2.5: 10 / 0 = {SafeDivision(10, 0)}");
            Console.WriteLine($"Radius of the circle is 4, its area is {AreaOfCircle(4)} ");
            Console.WriteLine($"Two integers are 5 and 3, the largest is {MaximumIntegers(5,3)}");
        }
        static int Add(int a, int b)
        {
            return a + b;
        }

        static int SafeDivision(int d, int r)
        {
            if (r == 0)
            {
                return d;
            }
            else
            {
                return d / r;
            }
        }

        static double AreaOfCircle(double r)
        {
            return PI * (r * r);
        }
        
        static int MaximumIntegers(int a, int b)
        {
            if (a > b)
            {
                return a;

            }
            else
            {
                return b;
            }

        }
    }
}
