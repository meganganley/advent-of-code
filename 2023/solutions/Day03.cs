using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace solutions
{
    internal class Day03
    {
        static void Main(string[] args)
        {
            //List<string> lines = Helper.get_input("day03_input.txt");
            List<string> lines = Helper.get_input("day03_input.txt");

            int numRows = lines.Count;
            int numCols = lines[0].Length;

            char[,] characters = new char[lines.Count, lines[0].Length];

            List<PartNumber> partNumbers = new List<PartNumber>();

            Dictionary<Point, int> possibleGears = new Dictionary<Point, int>();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                int startIndex = 0;
                bool inNumber = false;
                string number = "";
                bool isAdjacent = false;

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    // number is complete
                    if (!char.IsLetterOrDigit(c) && inNumber)
                    {
                        partNumbers.Add(new PartNumber(int.Parse(number), new Point(i, startIndex), new Point(i, j), isAdjacent));
                        inNumber = false;
                        number = "";                        
                        isAdjacent = false;

                    } 
                    // detected start of new number
                    else if (Char.IsNumber(c) && !inNumber)
                    {
                        startIndex = j;
                        inNumber = true;
                        number += c;

                        // see if it is adjacent (don't overwrite if not)
                        if(getAdjacent(i, j, lines))
                        {
                            isAdjacent = true;
                        }

                    } 
                    // current number is continuing
                    else if (Char.IsNumber(c) && inNumber)
                    {
                        number += c;

                        // see if it is adjacent (don't overwrite if not)
                        if (getAdjacent(i, j, lines))
                        {
                            isAdjacent = true;
                        }
                    }
                    // the last number in the column doesn't have a . to signify the end
                    if (j == numCols-1 && inNumber)
                    {
                        partNumbers.Add(new PartNumber(int.Parse(number), new Point(i, startIndex), new Point(i, j), isAdjacent));
                        inNumber = false;
                        number = "";
                        isAdjacent = false;
                    }
                }
                
            }

            int total = 0;
            foreach (var partNumber in partNumbers)
            {
                Console.WriteLine(partNumber);
                if (partNumber.isAdjacent)
                {
                    total += partNumber.number;

                }
                if (!partNumber.isAdjacent)
                {
                    //Console.WriteLine(partNumber);

                }
            }

            Console.WriteLine(total); // Part 1 -  522144
            Console.WriteLine(""); // Part 2 - 

        }

        static bool isSymbol(char c)
        {
            return !char.IsLetterOrDigit(c) && c != '.';
        }
        static bool isGear(char c)
        {
            return c == '*';
        }
        static bool getAdjacent(int i, int j, List<string> lines, Dictionary<Point, int> possibleGears)
        {
            int numRows = lines.Count;
            int numCols = lines[0].Length;

            //bool isAdjacent = true;

            if (i > 0 && j > 0 && isSymbol(lines[i - 1][j - 1]))
            {
                if (lines[i - 1][j - 1] == '*')
                {
                    //possibleGears.TryGetValue(new Point(i-1, j-1), out var currentCount);
                    //possibleGears[new Point(i - 1, j - 1)] = currentCount + 1;
                }
                return true;
            }

            if (i > 0 && isSymbol(lines[i - 1][j]))
            {
                return true;
            }

            if (i > 0 && j < numCols - 1 && isSymbol(lines[i - 1][j + 1]))
            {
                return true;
            }

            if (j > 0 && isSymbol(lines[i][j - 1]))
            {
                return true;
            }

            if (j < numCols - 1 && isSymbol(lines[i][j + 1]))
            {
                return true;
            }

            if (i < numRows - 1 && j > 0 && isSymbol(lines[i + 1][j - 1]))
            {
                return true;
            }

            if (i < numRows - 1 && isSymbol(lines[i + 1][j]))
            {
                return true;
            }

            if (i < numRows - 1 && j < numCols - 1 && isSymbol(lines[i + 1][j + 1]))
            {
                return true;
            }

            return false;
        }

    }

    internal class PartNumber
    {
        public int number;
        public Point start;
        public Point end;
        public bool isAdjacent;

        public PartNumber(int a_number, Point a_start, Point a_end, bool adjacent)
        {
            number = a_number;
            start = a_start;
            end = a_end;
            isAdjacent = adjacent;
        }


    public override string ToString()
        {
            return "Part number: " + number + ". Start =  " + start.X + " " + start.Y + ". End =  " + end.X + " " + end.Y + ". Adjacent = " + isAdjacent;
        }
    }
}
