using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Regex4_Steamlisten
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            string[] steamGameSites = {
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/262060/Darkest_Dungeon/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/201510/Flatout_3_Chaos__Destruction/?curator_clanid=32686107").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/341640/Relativity_Wars__A_Science_Space_RTS/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/977880/Eastward/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/1630580/Legend_of_Keepers_Return_of_the_Goddess/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/1490610/METALLIC_CHILD/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/1118310/RetroArch/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/740130/Tales_of_Arise/").Result,
                httpClient.GetStringAsync(@"https://store.steampowered.com/app/840720/Sword_Art_Online_Lost_Song/").Result,
                };

            string titleRegex = @"<title>(?:Save \d+. on )?(.*).on\sSteam.*<";
            string recentRatingRegex = @"<.*?>(Recent Reviews).*\n\t*.*\n\t*.*?>(.*(Positive|Mixed|Negative))<";
            string ratingRegex = @"<.*summary (?:positive|mixed)?.*?>(.*(Positive|Negative|Mixed))<";

            foreach (string htmlCode in steamGameSites)
            {
                Match recentRatingMatch = Regex.Match(htmlCode, recentRatingRegex);
                Match ratingMatch = Regex.Match(htmlCode, ratingRegex);
                Match titleMatch = Regex.Match(htmlCode, titleRegex);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(titleMatch.Groups[1].Value.ToUpper());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("All reviews: ");
                switch (ratingMatch.Groups[2].Value)
                {
                    case "Positive":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "Negative":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "Mixed":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }

                Console.WriteLine($"{ ratingMatch.Groups[1].Value}");
                Console.ForegroundColor = ConsoleColor.White;
                if (recentRatingMatch.Success)
                {
                    Console.Write("Recent reviews: ");
                    switch (recentRatingMatch.Groups[3].Value)
                    {
                        case "Positive":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "Negative":
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case "Mixed":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }
                    Console.WriteLine($"{ recentRatingMatch.Groups[2].Value}");
                    
                }
                Console.WriteLine();
            }
            

            



            
        }
    }
}
