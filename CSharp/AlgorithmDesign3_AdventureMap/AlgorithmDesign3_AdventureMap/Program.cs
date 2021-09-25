using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Algorithm_design_3
{

    class Program
    {
        static int height = 30;
        static int width = 50;
        static bool canMakeMiniRoad = true;

        static char[,] mapCharData = new char[width, height];
        static bool[,] glowRiver = new bool[width, height];
        static ConsoleColor[,] mapColorData = new ConsoleColor[width, height];

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            CreateMap2();
            DrawMap();
            Console.BackgroundColor = ConsoleColor.Black;
        }


        static void CreateMap2()
        {
          
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    
                    mapCharData[x, y] = ' ';
                }
            }
            Random random = new Random();
            // Forest Generation
            char[] forestChar = { '╦', '╬', '^', 'T', 'A', 'Ŧ', ' ', ' ', ' ', ' ' };
            ConsoleColor forestColor = ConsoleColor.Green;

            /* For every row, we want to decide how many columns is occupied by forest.
                We find a random number that is between 1/4th to 1/3rd of the map size 
                for every column. Then we check if the column is one of the first 15% of
                the map, if that is true, then we decrease the chance of there being a blank
                step at that index.
             */
            for (int y = 0; y < height - 1; y++)
            {
                int forestLength = random.Next((width - 1) / 8, (width - 1) / 5);
                for (int x = 0; x < forestLength; x++)
                {
                    if (x < Convert.ToInt32(width * 0.01))
                    {
                        SetPlacement(x, y, forestChar[random.Next(0, 7)], forestColor);
                    }
                    else
                    {
                        SetPlacement(x, y, forestChar[random.Next(0, 10)], forestColor);

                    }

                }
            }
            // River Generation
            ConsoleColor riverColor = ConsoleColor.DarkBlue;
            char[] riverChar = { '|', '/', '\\' };
            GenerateCurve(riverChar, 0.66, 0.75, 3, 3, riverColor);


            // Wall Generation
            char[] wallChar = { '|', '/', '\\' };
            ConsoleColor wallColor = ConsoleColor.DarkMagenta;
            GenerateCurve(wallChar, 0.25, 0.33, 8, 2, wallColor);

            // Main Road & Bridge Generation
            char roadChar = '#';
            char bridgeChar = '=';

            int roadStartIndex = (height - 1) / 2;

            //Here we define a method that allows us to check if a specific index in the map is a river.
            //This is done by looking for the river character.
            bool IsRiverOrWall(int x, int y, ConsoleColor colour)
            {
                y = roadStartIndex + y;
                if (x >= width || y >= height || y < 0 || x < 0)
                {
                    return false;
                }

                return mapColorData[x, y] == colour;
            }




            /*
                Here we are checking for every index around the main road index for every column. If a river
                is not in the area around the index, then we can decide a new random direction. There is a
                1/15th chance that the road will go up or down
            
                If a river is found on any of the area around the index, then we know there is a river 
                around and we should not allow the road to change direction. We also then call a method
                to start generating the mini-road.
             */
            
            bool canMakeWallTop = true;
            bool canMakeWallBot = true;
            for (int x = 1; x < width - 1; x++)
            {
                if (IsRiverOrWall(x, -1, wallColor))
                {
                    if (canMakeWallTop)
                    {
                        SetPlacement(x, roadStartIndex - 1, '[', wallColor);

                        SetPlacement(x + 1, roadStartIndex - 1, ']', wallColor);

                        canMakeWallTop = false;
                    }
                }
                if (IsRiverOrWall(x, +1, wallColor))
                {
                    if (canMakeWallBot)
                    {
                        SetPlacement(x + 1, roadStartIndex + 1, ']', wallColor);
                        SetPlacement(x, roadStartIndex + 1, '[', wallColor);
                        canMakeWallBot = false;
                    }
                }



                //I understand this is a hefty, clunky and outright ugly piece of code. I tried to make a method for this within a forloop
                // But couldn't get it to work.
                if (!IsRiverOrWall(x, 1, riverColor)
                    && !IsRiverOrWall(x, -1, riverColor)
                    && !IsRiverOrWall(x + 1, 0, riverColor)
                    && !IsRiverOrWall(x - 1, 1, riverColor)
                    && !IsRiverOrWall(x - 1, -1, riverColor)
                    && !IsRiverOrWall(x - 2, -1, riverColor)
                    && !IsRiverOrWall(x - 2, 1, riverColor)
                    && !IsRiverOrWall(x - 3, -1, riverColor)
                    && !IsRiverOrWall(x - 3, 1, riverColor)
                    && !IsRiverOrWall(x + 1, 1, riverColor)
                    && !IsRiverOrWall(x + 1, -1, riverColor)
                    && !IsRiverOrWall(x + 2, -1, riverColor)
                    && !IsRiverOrWall(x + 2, 1, riverColor)
                    && !IsRiverOrWall(x, 2, riverColor)
                    && !IsRiverOrWall(x, -2, riverColor)
                    && !IsRiverOrWall(x - 1, 2, riverColor)
                    && !IsRiverOrWall(x - 1, -2, riverColor)
                    && !IsRiverOrWall(x - 2, -2, riverColor)
                    && !IsRiverOrWall(x - 2, 2, riverColor)
                    && !IsRiverOrWall(x - 3, -2, riverColor)
                    && !IsRiverOrWall(x - 3, 2, riverColor)
                    && !IsRiverOrWall(x + 1, 2, riverColor)
                    && !IsRiverOrWall(x + 1, -2, riverColor)
                    && !IsRiverOrWall(x + 2, -2, riverColor)
                    && !IsRiverOrWall(x + 2, 2, riverColor)
                    && !IsRiverOrWall(x, +1, wallColor)
                    && !IsRiverOrWall(x, -1, wallColor)
                    && !IsRiverOrWall(x + 1, -1, wallColor)
                    && !IsRiverOrWall(x + 1, +1, wallColor)
                    && !IsRiverOrWall(x - 1, -1, wallColor)
                    && !IsRiverOrWall(x - 1, +1, wallColor))

                {
                    int randDirection = random.Next(0, 15);

                    if (randDirection == 0)
                    {
                        roadStartIndex--;
                    }
                    else if (randDirection == 1)
                    {
                        roadStartIndex++;
                    }
                }
                else if (!IsRiverOrWall(x, +1, wallColor)
                    && !IsRiverOrWall(x, -1, wallColor)
                    && !IsRiverOrWall(x + 1, -1, wallColor)
                    && !IsRiverOrWall(x + 1, +1, wallColor)
                    && !IsRiverOrWall(x - 1, -1, wallColor)
                    && !IsRiverOrWall(x - 1, +1, wallColor))
                {
                    if (canMakeMiniRoad)
                    {
                        MakeRiverRoad(x, roadStartIndex);
                        canMakeMiniRoad = false;
                    }
                    // Here is where we also create the bridge, which we can do because we have detected the river.
                    ConsoleColor bridgeColor = ConsoleColor.White;
                    SetPlacement(x, roadStartIndex - 1, bridgeChar, bridgeColor);
                    SetPlacement(x, roadStartIndex + 1, bridgeChar, bridgeColor);

                }

                SetPlacement(x, roadStartIndex, roadChar, ConsoleColor.Gray);

            }



            // Border Generation
            char[] borderChar = { '═', '║' };
            ConsoleColor borderColor = ConsoleColor.DarkCyan;

            /*
                No matter the size of the map, the border will always be on the... well, border.
                This allows us to easily place the border at row 0, and the final row.
                Then we simply go through every column and add a border character at those two rows.
                Finally we add the corners at the first column and last column on the first and last rows.
             */
            for (int x = 0; x < width - 1; x++)
            {
                SetPlacement(x, 0, borderChar[0], borderColor);
                SetPlacement(x, height - 1, borderChar[0], borderColor);
            }
            for (int y = 0; y < height - 1; y++)
            {
                SetPlacement(0, y, borderChar[1], borderColor);
                SetPlacement(width - 1, y, borderChar[1], borderColor);
            }
            SetPlacement(0, 0, '╔', borderColor);
            SetPlacement(width - 1, 0, '╗', borderColor);
            SetPlacement(0, height - 1, '╚', borderColor);
            SetPlacement(width - 1, height - 1, '╝', borderColor);

            //Name Generation
            char[] mapName = "ADVENTURE MAP".ToCharArray();

            /*
                Quite simple, we add a variable that is half of the map - 6 (which is half of the letters in mapName)
                We also know that no matter the size of the map, the text should always be on row 1, so we simply
                go through every letter in mapName and add that at the right place.

                One way to make this more dynamic/modular would be to change the 6 in nameIndex to a variable that is perhaps
                something along the lines of (Psuedocode): int nameIndex = width / 2 - mapName.length / 2
             */
            int nameIndex = width / 2 - 6;
            foreach (var letter in mapName)
            {

                SetPlacement(nameIndex, 1, letter, ConsoleColor.Cyan);
                nameIndex++;
            }


            /*
                Here we first decide what the previous column of the road was, because we will need it
                Then we go through every row after the main row (from where the river was detected)
                and check if we detect a river in every column from the current column. We then alter
                the roads column depending on if the river is turning left or right.
             */
            void MakeRiverRoad(int roadStartX, int roadStartY)
            {


                int previousRoadX = roadStartX;

                for (int y = roadStartY + 1; y < height - 1; y++)
                {
                    int x = previousRoadX;
                    while (true)
                    {
                        char testChar = mapCharData[x, y];
                        if (testChar == '/' || testChar == '\\' || testChar == '|')
                        {
                            previousRoadX = x - 5;
                            SetPlacement(previousRoadX, y, roadChar, ConsoleColor.Gray);
                            break;
                        }

                        else
                        {
                            x++;
                        }

                    }

                }

            }
        }

        static void GenerateCurve(char[] symbol, double randomStartMin, double randomStartMax, int randomDirectionMax, int curveWidth, ConsoleColor colour)
        {
            Random random = new Random();


            // We decide on a random starting location for the river that is somewhere between 66% and 75% of the map.
            int StartIndex = random.Next(Convert.ToInt32((width - 1) * randomStartMin), Convert.ToInt32((width - 1) * randomStartMax));

            for (int y = 0; y < height - 1; y++)
            {

                int randDirection = random.Next(0, randomDirectionMax);
                if (randDirection > 0)
                {
                    if (randDirection > 1)
                    {
                        if (randDirection > 2)
                        {
                            randDirection = 0;
                        }
                        if (y > 0 && mapCharData[StartIndex, y - 1] != symbol[1] && randDirection != 0)
                        {
                            StartIndex++;
                        }

                    }
                    else
                    {
                        if (y > 0 && mapCharData[StartIndex, y - 1] != symbol[2])
                        {
                            StartIndex--;
                        }
                    }


                }
                /* Here we place the wall at every row three times to give it width.
                    One way to make this more dynamic/modular would be to make the wallWidth
                    into a variable and allow it to be changed easier.
                 */
                for (int x = 0; x < curveWidth; x++)
                {
                    SetPlacement(StartIndex + x - 1, y, symbol[randDirection], colour);
                }
            }
        }


        static void DrawMap()
        {
            /*
                Simply color and place every single character in every column in a row, then once we have done so,
                we write a new line for every row.
             */
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    Console.ForegroundColor = mapColorData[x, y];
                    Console.Write(mapCharData[x, y]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine();

            }



        }
        /*
            Here we simply set the placement in the array we use to print, we also set
            the color in an array at the same location
        */
        static void SetPlacement(int x, int y, char input, ConsoleColor colour)
        {
            mapCharData[x, y] = input;
            mapColorData[x, y] = colour;
        }
    }
}
