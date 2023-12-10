using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace solutions
{
    public class day10
    {
        public static void Run()
        {
            //List<string> lines = Helper.get_input("day10_input.txt");
            List<string> lines = Helper.get_input("day10_sample_input.txt");

            char[] pipes = ['|', '-', 'L', 'J', '7', 'F'];
   

            int numFilledRows = lines.Count;
            int numFilledCols = lines[0].Length;
            int numRows = numFilledRows + 2;
            int numCols = numFilledCols + 2;

            char[,] grid = CreateEmptyGrid(lines);

            Point start = new Point(0,0);

            List<Point> leftLoop = new List<Point>();
            List<Point> rightLoop = new List<Point>();
            List<Point> usedPoints = new List<Point>();

            // populate with input
            for (int i = 1; i <= numFilledRows; i++)
            {
                for (int j = 1; j <= numFilledCols; j++)
                {
                    grid[i, j] = (char)lines[i - 1][j - 1];
                    if (grid[i,j] == 'S')
                    {
                        start = new Point(j, i);
                        usedPoints.Add(start);
                    }
                }
            }

            // get the two next points
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(pipes.Contains(grid[start.X+i, start.Y + j]))
                    {
                        if ( i == 0 && j == 0 )
                        {
                            continue;
                        }
                        char symbol = grid[start.X + i, start.Y + j];
         
                        if (!isValidPipe(symbol, i, j))
                        {
                            continue;
                        }

                        if (!leftLoop.Any())
                        {
                            Point leftStart = new Point(start.X + i, start.Y + j);
                            leftLoop.Add(leftStart);
                            usedPoints.Add(leftStart);
                        }
                        else
                        {
                            Point rightStart = new Point(start.X + i, start.Y + j);
                            rightLoop.Add(rightStart);
                            usedPoints.Add(rightStart);
                        }
                    }
                }
            }

            int forceStop = 0;
            bool keepLooping = true;

            Point curr = new Point(0,0);
            while (keepLooping)
            {
                if (forceStop == 13)
                {
                    Console.WriteLine('a');
                }
                // do left loop
                curr = leftLoop[forceStop];

                Point next = getNextPipe(grid, curr, pipes, usedPoints);
                if (next == new Point(-1, -1))
                {
                    break;
                }
                leftLoop.Add(next);
                usedPoints.Add(next);

                curr = rightLoop[forceStop];

                next = getNextPipe(grid, curr, pipes, usedPoints);
                if (next == new Point(-1, -1))
                {
                    break;
                }
                rightLoop.Add(next);
                usedPoints.Add(next);

                forceStop++;
                /*
                if(forceStop > 10)
                {
                    keepLooping = false;
                }*/

            }

            /*
            foreach (var item in leftLoop)
            {
                Console.WriteLine(item.X + " " + item.Y);
            }
            Console.WriteLine();
            foreach (var item in rightLoop)
            {
                Console.WriteLine(item.X + " " + item.Y);
            }*/

            // print grid
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }    

            Console.WriteLine(Math.Max(rightLoop.Count, leftLoop.Count));    // Part 1 - 251029473
            Console.WriteLine(0);    // Part 2 - 251003917

        }

        public static Point getNextPipe(char[,] grid, Point curr, char[] pipes, List<Point> usedPoints)
        {
            char currSymbol = grid[curr.X, curr.Y];

            Point coord = new Point(-1, -1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == j || (i == -1  && j == 1) || (i == 1 && j == -1))
                    {
                        continue;
                    }

                    if (currSymbol == '|' && j != 0)
                    {
                        continue;
                    }
                    if (currSymbol == '-' && i != 0)
                    {
                        continue;
                    }
                    if (currSymbol == 'L' && ((j != 0 && i != -1) && (j != 1 && i != 0)))
                    {
                        continue;
                    }
                    if (currSymbol == 'J' && ((j != 1 && i != 0) && (j != 0 && i != 1)))
                    {
                        continue;
                    }
                    if (currSymbol == '7' && ((j != -1 && i != 0) && (j != 0 && i != 1)))
                    {
                        continue;
                    }
                    if (currSymbol == 'F' && ((j != 0 && i != -1) && (j != -1 && i != 0)))
                    {
                        continue;
                    }

                    coord = new Point(curr.X + i, curr.Y + j);
                    if (pipes.Contains(grid[coord.X, coord.Y]))
                    {
                        if (i == 0 && j == 0)
                        {
                            coord = new Point(-1, -1);
                            continue;
                        }
                        char newSymbol = grid[coord.X, coord.Y];

                        if (!isValidPipe(newSymbol, i, j))
                        {
                            coord = new Point(-1, -1);
                            continue;
                        }
                        if (usedPoints.Contains(coord))
                        {
                            coord = new Point(-1, -1);
                            continue;
                        }
                        //rightLoop.Add(coord);
                        //usedPoints.Add(coord);
                        return coord;
                    }
                }
            }
            return new Point(-1, -1);
        }

        public static bool isValidPipe(char symbol, int x, int y)
        {
            bool isValid = false;
            if (symbol == '|' && y == 0)
            {
                isValid = true;
            }
            if (symbol == '-' && x == 0)
            {
                isValid = true; 
            }
            if (symbol == 'L' && ((x == 0 && y == -1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (symbol == 'J' && ((x == 1 && y == 0) || (x == 0 && y == 1)))
            {
                isValid = true;
            }
            if (symbol == '7' && ((x == -1 && y == 0) || (x == 0 && y == 1)))
            {
                isValid = true;
            }
            if (symbol == 'F' && ((x == 0 && y == -1) || (x == -1 && y == 0)))
            {
                isValid = true;
            }
            return isValid;
        }

        public static char[,] CreateEmptyGrid(List<string> lines)
        {
            int numFilledRows = lines.Count;
            int numFilledCols = lines[0].Length;
            int numRows = numFilledRows + 2;
            int numCols = numFilledCols + 2;

            char[,] grid = new char[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                grid[i, 0] = '.';
                grid[i, numRows - 1] = '.';
            }

            for (int i = 0; i < numCols; i++)
            {
                grid[0, i] = '.';
                grid[numCols - 1, i] = '.';
            }

            return grid;
        }
    }

}

