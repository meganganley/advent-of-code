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
            List<string> lines = Helper.get_input("day10_input.txt");
            //List<string> lines = Helper.get_input("day10_sample_input.txt");
            //List<string> lines = Helper.get_input("day10_sample_input_2.txt");

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
            for (int x = 1; x <= numFilledRows; x++)
            {
                for (int y = 1; y <= numFilledCols; y++)
                {
                    grid[x,y] = lines[x - 1][y - 1];
                    if (grid[x,y] == 'S')
                    {
                        start = new Point(x, y); // x = col number (position on x axis), y = row number (position on y axis)
                        usedPoints.Add(start);
                    }
                }
            }

            /*
            for (int x = 0; x < numRows; x++)
            {
                for (int y = 0; y < numCols; y++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }*/

            // get the two first points
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(pipes.Contains(grid[start.X+i, start.Y+j]))
                    {
                        if ( i == 0 && j == 0 )
                        {
                            continue;
                        }
                        char symbol = grid[start.X+i, start.Y+j];
         
                        if (!isValidNextPipe(symbol,i,j))
                        {
                            continue;
                        }

                        if (!leftLoop.Any())
                        {
                            Point leftStart = new Point(start.X+i, start.Y+j);
                            leftLoop.Add(leftStart);
                            usedPoints.Add(leftStart);
                        }
                        else
                        {
                            Point rightStart = new Point(start.X+i, start.Y+j);
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
            }

            int farthestPipe = Math.Max(rightLoop.Count, leftLoop.Count);
            Console.WriteLine(farthestPipe);    // Part 1 - 6640

            // create list in order for input as 'polygon'
            rightLoop.Reverse();
            leftLoop.AddRange(rightLoop);
            leftLoop.Insert(0, start);


            PointF[] usedPointF = new PointF[leftLoop.Count];

            for (int i = 0; i < leftLoop.Count; i++)
            {
                usedPointF[i] = new PointF(leftLoop[i].X, leftLoop[i].Y);
            }

            int tilesEnclosed = 0;
            for (int x = 1; x <= numFilledCols; x++)                
            {
                for (int y = 1; y <= numFilledRows; y++)
                {
                    // part of the pipe - don't count for enclosed
                    if (usedPoints.Contains(new Point(x, y)))
                    {
                        continue;

                    } else if (IsPointInPolygon4(usedPointF, new PointF(x, y)))
                    {
                        tilesEnclosed++;
                    }
                }
            }

            Console.WriteLine(tilesEnclosed);    // Part 2 - 411

        }

        public static bool IsPointInPolygon4(PointF[] polygon, PointF testPoint)
        {
            bool result = false;
            int j = polygon.Length - 1;
            for (int i = 0; i < polygon.Length; i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y ||
                    polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) /
                       (polygon[j].Y - polygon[i].Y) *
                       (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }


        public static bool isTileEnclosed(List<Point> usedPoints, int x, int y)
        {
            bool isEnclosed = true;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ( i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (!usedPoints.Contains(new Point(x+j, y+i)))
                    {
                        isEnclosed = false;
                    }
                }
            }
            return isEnclosed;
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

                    // Valid next index
                    if (!isValidNextIndex(currSymbol, i, j)){
                        continue;
                    }

                    coord = new Point(curr.X+i, curr.Y+j);
                    if (pipes.Contains(grid[coord.X, coord.Y]))
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }
                        char newSymbol = grid[coord.X, coord.Y];

                        if (!isValidNextPipe(newSymbol, i, j))
                        {
                            continue;
                        }
                        if (usedPoints.Contains(coord))
                        {
                            continue;
                        }
                        return coord;
                    }
                }
            }
            return new Point(-1, -1);
        }

        public static bool isValidNextPipe(char nextSymbol, int x, int y)
        {
            bool isValid = false;
            if (nextSymbol == '|' && y == 0 && (x == 1 || x == -1))
            {
                isValid = true;
            }
            if (nextSymbol == '-' && x == 0 && (y == 1 || y == -1))
            {
                isValid = true;
            }
            if (nextSymbol == 'L' && ((x == 0 && y == -1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (nextSymbol == 'J' && ((x == 0 && y == 1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (nextSymbol == '7' && ((x == -1 && y == 0) || (x == 0 && y == 1)))
            {
                isValid = true;
            }
            if (nextSymbol == 'F' && ((x == -1 && y == 0) || (x == 0 && y == -1)))
            {
                isValid = true;
            }
            return isValid;
        }
        public static bool isValidNextIndex(char currSymbol, int x, int y)
        {
            bool isValid = false;
            if (currSymbol == '|' && y == 0 && (x == 1 || x == -1))
            {
                isValid = true;
            }
            if (currSymbol == '-' && x == 0 && ( y == 1 || y == -1 ))
            {
                isValid = true;
            }
            if (currSymbol == 'L' && ((x == -1 && y == 0) || (x == 0 && y == 1)))
            {
                isValid = true;
            }
            if (currSymbol == 'J' && ((x == -1 && y == 0) || (x == 0 && y == -1)))
            {
                isValid = true; 
            }
            if (currSymbol == '7' && ((x == 0 && y == -1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (currSymbol == 'F' && ((x == 0 && y == 1) || (x == 1 && y == 0)))
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
                for (int j = 0; j < numCols; j++)
                {
                    grid[i, j] = '.';
                }
                   
            }
            return grid;
        }
    }

}

