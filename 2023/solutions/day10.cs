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

            char[] pipes = ['|', '-', 'L', 'J', '7', 'F'];
   

            int numFilledRows = lines.Count;
            int numFilledCols = lines[0].Length;
            int numRows = numFilledRows + 2;
            int numCols = numFilledCols + 2;

            char[,] grid = CreateEmptyGrid(lines);

            for (int x = 0; x < numCols; x++)
            {
                for (int y = 0; y < numRows; y++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }

            Point start = new Point(0,0);

            List<Point> leftLoop = new List<Point>();
            List<Point> rightLoop = new List<Point>();
            List<Point> usedPoints = new List<Point>();

            // populate with input
            for (int y = 1; y <= numFilledRows; y++)
            {
                for (int x = 1; x <= numFilledCols; x++)
                {
                    grid[x, y] = lines[y - 1][x - 1];
                    if (grid[x, y] == 'S')
                    {
                        start = new Point(x, y);
                        usedPoints.Add(start);
                    }
                }
            }

            // get the two next points
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(pipes.Contains(grid[start.X+j, start.Y+i]))
                    {
                        if ( i == 0 && j == 0 )
                        {
                            continue;
                        }
                        char symbol = grid[start.X+j, start.Y+i];
         
                        if (!isValidNextPipe(symbol, j, i))
                        {
                            continue;
                        }

                        if (!leftLoop.Any())
                        {
                            Point leftStart = new Point(start.X+j, start.Y+i);
                            leftLoop.Add(leftStart);
                            usedPoints.Add(leftStart);
                        }
                        else
                        {
                            Point rightStart = new Point(start.X+j, start.Y+i);
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
                
                /*if(forceStop > 10)
                {
                    keepLooping = false;
                }*/

            }

            int tilesEnclosed = 0;

            for (int y = 1; y <= numFilledRows; y++)
            {
                for (int x = 1; x <= numFilledCols; x++)
                {
                    // part of the pipe - don't count for enclosed
                    if (usedPoints.Contains(new Point(x, y)))
                    {
                        continue;
                    } else if (IsPointInPolygon4(usedPoints.ToArray(), new Point(x, y)))
                    {
                        tilesEnclosed++;
                    }
                }
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
            /*
            

            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numCols; x++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }  */

            Console.WriteLine(Math.Max(rightLoop.Count, leftLoop.Count));    // Part 1 - 6640
            Console.WriteLine(tilesEnclosed);    // Part 2 - 251003917

        }

        public static bool IsPointInPolygon4(Point[] polygon, Point testPoint)
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

        public static bool IsInPolygon(Point[] poly, Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
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
                    if (!isValidNextIndex(currSymbol, j, i)){
                        continue;
                    }

                    coord = new Point(curr.X+j, curr.Y+i);
                    if (pipes.Contains(grid[coord.X, coord.Y]))
                    {
                        if (i == 0 && j == 0)
                        {
                            coord = new Point(-1, -1);
                            continue;
                        }
                        char newSymbol = grid[coord.X, coord.Y];

                        if (!isValidNextPipe(newSymbol, j, i))
                        {
                            coord = new Point(-1, -1);
                            continue;
                        }
                        if (usedPoints.Contains(coord))
                        {
                            coord = new Point(-1, -1);
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
            if (nextSymbol == '|' && x == 0 && (y == 1 || y == -1))
            {
                isValid = true;
            }
            if (nextSymbol == '-' && y == 0 && (x == 1 || x == -1))
            {
                isValid = true;
            }
            if (nextSymbol == 'L' && ((x == -1 && y == 0) || (x == 0 && y == 1)))
            {
                isValid = true;
            }
            if (nextSymbol == 'J' && ((x == 0 && y == 1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (nextSymbol == '7' && ((x == 0 && y == -1) || (x == 1 && y == 0)))
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
            if (currSymbol == '|' && x == 0 && (y == 1 || y == -1))
            {
                isValid = true;
            }
            if (currSymbol == '-' && y == 0 && ( x == 1 || x == -1 ))
            {
                isValid = true;
            }
            if (currSymbol == 'L' && ((x == 0 && y == -1) || (x == 1 && y == 0)))
            {
                isValid = true;
            }
            if (currSymbol == 'J' && ((x == -1 && y == 0) || (x == 0 && y == -1)))
            {
                isValid = true; 
            }
            if (currSymbol == '7' && ((x == -1 && y == 0) || (x == 0 && y == 1)))
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
                grid[i, 0] = '.';
                grid[i, numCols - 1] = '.';
            }

            for (int i = 0; i < numCols; i++)
            {
                grid[0, i] = '.';
                grid[numRows - 1, i] = '.';
            }

            //grid[x, y] = lines[y - 1][x - 1];
            return grid;
        }
    }

}

