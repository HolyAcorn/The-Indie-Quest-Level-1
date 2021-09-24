using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace Classes
{
    /*
        !!IDEA!!
            We could create a list with colors, similar to the adventure map that allows us to set a different color for like,
            the border on the title than the actual text. Although, since that is the only place we really need it, it would probably
            be easier to hardcode the title in.

        !!IDEA #2!!
            Adding 1 variables to the WriteCool method that are something like: (ConsoleColor startColor).
            This would allow us to very easily switch colors for different types of text etc etc.
     */



    /*
        Here we create a class for every location, its name, its description, all its closest neighbours, 
        and the shortest path to all other locations
     */
    class Location
    {
        public string Name;
        public string Description;
        public List<Neighbor> Neighbors = new List<Neighbor>();
        public List<Path> ShortestPath = new List<Path>();

        public override string ToString() => Name;
    }
    /*
        Here we create a class for all closest neighbours, it holds its location, and the distance between the source location
        and the neighbour
     */
    class Neighbor
    {
        public Location Location;
        public int Distance;
    }
    /*
        Here we create a class for the paths between the source location and every other location. It contains the destination,
        the total distance, and all the stops on the way.
     */
    class Path
    {
        public Location Location;
        public int Distance;
        public List<string> StopNames = new List<string>();
    }

    class Program
    {
        static bool quitGame = false;
        static int charTimer = 50;
        static int charLongTimer = 100;
        static int charExtraLongTimer = 150;
        static int rowTimer = 200;
        static int rowLongTimer = 400;
        static int rowExtraLongTimer = 700;
        static ConsoleColor textColor = ConsoleColor.Blue;
        static ConsoleColor askInputColor = ConsoleColor.Cyan;
        static ConsoleColor inputColor = ConsoleColor.White;
        static ConsoleColor travelColor = ConsoleColor.Cyan;

        static void Main(string[] args)
        {

            //_DebugNoCharSpeed(); //TODO: ONLY USE IN DEBUG, TURN OFF LATER!!!!!!!!!!!!!!!
            List<Location> locations = new List<Location> {
            new Location {
            Name = "Winterfell",
            Description = "the capital of the Kingdom of the North"},
            new Location {
            Name = "Pyke",
            Description = "the stronghold and seat of House Greyjoy"},
            new Location {
            Name = "Riverrun",
            Description = "a large castle located in the central-western part of the Riverlands"},
            new Location {
            Name = "The Trident",
            Description = "one of the largest and most well-known rivers on the continent of Westeros"},
            new Location {
            Name = "King's Landing",
            Description = "the capital, and largest city, of the Seven Kingdoms"},
            new Location {
            Name = "Highgarden",
            Description = "the seat of House Tyrell and the regional capital of the Reach"},
            };

            //Title Generation
            string titleFile = "Title.txt";
            string[] title = File.ReadAllLines(titleFile);

            for (int rIndex = 0; rIndex < title.Length; rIndex++)
            {
                for (int c = 0; c < title[rIndex].Length; c++)
                {
                    if (rIndex < 13 && rIndex > 1 && c > 6 && c < 100)
                    {
                        if (rIndex > 3 && rIndex < 6 && ((c > 6 && c < 12) || (c > 94 && c < 100)))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.Write(title[rIndex][c]);
                    Thread.Sleep(0);
                }
                Console.WriteLine();
                Thread.Sleep(75);
            }

            //WriteCool(title, 0, rowTimer);
            Console.ReadKey();
            Console.Clear();


            // Adding a dictionary so we can write the name instead of the Index.
            Dictionary<string, Location> locationsByName = new Dictionary<string, Location> { };

            foreach (Location location in locations)
            {
                locationsByName.Add(location.Name, location);
                
            }

            // Adding all paths to create a node map.
            ConnectLocations(locationsByName["Winterfell"], locationsByName["Pyke"], 18);
            ConnectLocations(locationsByName["Winterfell"], locationsByName["The Trident"], 10);
            ConnectLocations(locationsByName["King's Landing"], locationsByName["The Trident"], 5);
            ConnectLocations(locationsByName["King's Landing"], locationsByName["Highgarden"], 8);
            ConnectLocations(locationsByName["Pyke"], locationsByName["Highgarden"], 14);
            ConnectLocations(locationsByName["Riverrun"], locationsByName["Highgarden"], 10);
            ConnectLocations(locationsByName["Riverrun"], locationsByName["The Trident"], 2);
            ConnectLocations(locationsByName["Riverrun"], locationsByName["Pyke"], 3);
            ConnectLocations(locationsByName["Riverrun"], locationsByName["King's Landing"], 25);

            foreach (Location location in locations)
            {
                Dijkstra(locations, location);
            }

            
            Location currentLocation = locations[new Random().Next(0,locations.Count)];
            while (!quitGame)
            {
                DisplayDestination(currentLocation);
                Path path = currentLocation.ShortestPath[AskForDirections(currentLocation) - 1];
                if (!quitGame)
                {
                    
                    currentLocation = path.Location;
                    Console.Clear();
                    if (path.StopNames.Count > 0)
                    {
                        string[] travellingThroughText = new string[] { "Travelling through", ".", ".", "." };
                        WriteCool(travellingThroughText, charTimer, rowExtraLongTimer, travelColor, false);
                        Console.WriteLine();
                        WriteCool(path.StopNames.ToArray(), charExtraLongTimer, rowLongTimer, travelColor);
                        WriteCool(new string[] { ".", ".", ".", ".", "." }, charTimer, rowExtraLongTimer, travelColor, false);
                        Console.Clear();
                    }
                }
                
                
            }
            Thread.Sleep(rowLongTimer);
            Console.Clear();

            //Don't Panic Generation
            string panicFile = "DontPanic.txt";
            string[] dontPanic = File.ReadAllLines(panicFile);

            for (int rIndex = 0; rIndex < dontPanic.Length; rIndex++)
            {
                for (int c = 0; c < dontPanic[rIndex].Length; c++)
                {
                    if (dontPanic[rIndex][c] == ':')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write(dontPanic[rIndex][c]);
                    Thread.Sleep(0);
                }
                if (rIndex != dontPanic.Length-1)
                {
                    Console.WriteLine();
                    Thread.Sleep(75);
                }
                
            }
            Console.ReadKey();
            Environment.Exit(0);


        }

        static void DisplayDestination(Location currentLocation)
        {
            WriteCool(new string[] { $"Welcome to {currentLocation.Name},",$" {currentLocation.Description}." }, charTimer, rowExtraLongTimer, textColor, false);
            Console.WriteLine();
            Console.WriteLine();
            string[] availDestinations = new string[currentLocation.ShortestPath.Count+2];

            availDestinations[0] = "Available destinations are:";
            availDestinations[1] = "";
            for (int i = 0; i < currentLocation.ShortestPath.Count; i++)
            {
                Path neighbour = currentLocation.ShortestPath[i];
                availDestinations[i + 2] = $"{ 1 + i}. { neighbour.Location.Name} ({ neighbour.Distance})";
                
            }
            WriteCool(availDestinations, charTimer, rowExtraLongTimer -200, textColor);
            WriteCool(new string[] {"Or have you reached your destination and would like to (q)uit?" }, charTimer, rowExtraLongTimer -200, ConsoleColor.DarkBlue);
        }

        //asking for input from player
        static int AskForDirections(Location currentLocation)
        {
            string input;
            int travelDestination = 0;
            while (travelDestination == 0)
            {
                Console.WriteLine();
                WriteCool(new string[] { "Where would you like to travel?" }, charTimer, rowTimer, askInputColor);
                Console.ForegroundColor = inputColor;
                input = Console.ReadLine();
                Thread.Sleep(rowLongTimer);
                Console.WriteLine();
                try
                {
                    travelDestination = Convert.ToInt32(input);
                    
                    if (travelDestination > currentLocation.ShortestPath.Count)
                    {

                        travelDestination = 0;
                        WriteCool(new string[] { "Please enter the number of your destination."}, charTimer, rowTimer, askInputColor);
                        ResetCursor();
                        

                    }
                }
                catch (Exception e)
                {
                    if (input == "q" || input == "Q")
                    {
                        quitGame = true;
                        travelDestination = 1;
                        break;
                    }
                    
                    WriteCool(new string[] { "Enter a number"," u idiot" }, charExtraLongTimer, rowExtraLongTimer, ConsoleColor.Red, false);
                    ResetCursor();
                }

            }
            return travelDestination;
        }

        static void ConnectLocations(Location a, Location b, int distance)
        {
            a.Neighbors.Add(new Neighbor { Location = b, Distance = distance });
            b.Neighbors.Add(new Neighbor { Location = a, Distance = distance });
        }

        static void Dijkstra(List<Location> map, Location source)
        {
            // Q = set of neighbours
            // u = Neighbour in Q that has the shortest distance from source
            // v = Location in map
            List<Location> Q = new List<Location> { };
            Dictionary<Location, int> dist = new Dictionary<Location, int> { };
            Dictionary<Location, Location> prev = new Dictionary<Location, Location> { };
            foreach (Location v in map)
            {
                dist.Add(v, 99);
                prev.Add(v, null);
                Q.Add(v);
            }
            dist[source] = 0;

            while (Q.Count != 0)
            {
                Location u = Q.OrderBy((v) => dist[v]).First();

                Q.Remove(u);
                for (int v = 0; v < u.Neighbors.Count; v++)
                {
                    Location neighbour = u.Neighbors[v].Location;
                    if (Q.Contains(u.Neighbors[v].Location))
                    {
                        int alt = dist[u] + u.Neighbors[v].Distance;
                        if (alt < dist[neighbour])
                        {
                            dist[neighbour] = alt;
                            prev[neighbour] = u;
                        }
                    }
                }
            }


            foreach (Location otherLocation in map)
            {

                if (otherLocation == source) continue;

                var path = new Path { Location = otherLocation, Distance = dist[otherLocation] };
                source.ShortestPath.Add(path);

                Location stop = prev[otherLocation];
                while (stop != source)
                {
                    path.StopNames.Insert(0, stop.Name);
                    stop = prev[stop];
                }
            }
            source.ShortestPath.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        }

        static void WriteCool(string[] text, int charSpeed, int rowSpeed, ConsoleColor color, bool newRow = true)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            for (int i = 0; i < text.Length; i++)
            {
                foreach (char c in text[i])
                {
                    Console.Write(c);
                    Thread.Sleep(charSpeed);
                }
                if (i == text.Length-1)
                {
                    Thread.Sleep(rowSpeed/2);
                }
                else
                {
                    Thread.Sleep(rowSpeed);
                }
                
                if (newRow)
                {
                    Console.WriteLine();
                }
            }
            
        }

        static void ResetCursor(int rows = 5)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - rows);
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("                                                                                                    ");
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - rows-2);
        }

        static void _DebugNoCharSpeed()
        {
            charTimer = 0;
            charLongTimer = 0;
            charExtraLongTimer = 0;
        }
        
    }
}
