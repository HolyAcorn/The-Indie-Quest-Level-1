using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CreateDayDescription(121, 2, 1994));
        }

        static string CreateDayDescription(int day, int season, int year)
        {
            string[] seasons = { "Spring", "Summer", "Fall", "Winter" };
            string output = $"{day}";
            output = OrdinalMaker(day, output);


            
            output += $" day of {seasons.GetValue(season)} in the year {year}.";
                //
                return output;
        }
        static string OrdinalMaker(int day, string output)
        {
            switch (day % 100)
            {
                case 11:
                case 12:
                case 13:
                    return output += "th";
            }
            switch (day % 10)
            {
                case 1:
                    return output += "st";
                case 2:
                    return output += "nd";
                case 3:
                    return output += "rd";
                default:
                    return output += "th";
            }
        }
    }
}
