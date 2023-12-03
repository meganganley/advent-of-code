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
            //List<string> lines = Helper.get_input("day03_input_sample.txt");
            List<string> lines = Helper.get_input("day03_input.txt");

            int numRows = lines.Count;
            int numCols = lines[0].Length;

            char[,] characters = new char[lines.Count, lines[0].Length];

            List<PartNumber> partNumbers = new List<PartNumber>();

            Dictionary<Point, Point> possibleGears = new Dictionary<Point, Point>(); // co-ordinate of asterisk, then Point(countAdjacent, productOfAdjacent)

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                int startIndex = 0;
                bool inNumber = false;
                string number = "";
                bool isAdjacentSymbol = false;
                List<Point> adjacentAsterisks = new List<Point>();

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    // number is complete
                    if (!char.IsLetterOrDigit(c) && inNumber)
                    {
                        partNumbers.Add(new PartNumber(int.Parse(number), new Point(i, startIndex), new Point(i, j), isAdjacentSymbol));

                        foreach (var asterisk in adjacentAsterisks)
                        {
                            if (!possibleGears.ContainsKey(asterisk))
                            {
                                possibleGears.Add(asterisk, new Point(1, int.Parse(number)));
                            } else
                            {
                                var value = possibleGears[asterisk];
                                value.X++;
                                value.Y *= int.Parse(number);
                                possibleGears[asterisk] = value;
                            }

                        }

                        inNumber = false;
                        number = "";
                        isAdjacentSymbol = false;
                        adjacentAsterisks = new List<Point>();

                    }
                    // detected start of new number
                    else if (Char.IsNumber(c) && !inNumber)
                    {
                        startIndex = j;
                        inNumber = true;
                        number += c;

                        // see if it is adjacent (don't overwrite if not)
                        if (isAdjacentToSymbol(i, j, lines))
                        {
                            isAdjacentSymbol = true;
                        }
                        // add new items 
                        adjacentAsterisks.AddRange(getAllAdjacentToAsterisk(i, j, lines));
                        // remove dupes
                        adjacentAsterisks = adjacentAsterisks.Distinct().ToList(); 
  
                    }
                    // current number is continuing
                    else if (Char.IsNumber(c) && inNumber)
                    {
                        number += c;

                        // see if it is adjacent (don't overwrite if not)
                        if (isAdjacentToSymbol(i, j, lines))
                        {
                            isAdjacentSymbol = true;
                        }
                        // add new items 
                        adjacentAsterisks.AddRange(getAllAdjacentToAsterisk(i, j, lines));
                        // remove dupes
                        adjacentAsterisks = adjacentAsterisks.Distinct().ToList();

                    }
                    // the last number in the column doesn't have a . to signify the end
                    if (j == numCols - 1 && inNumber)
                    {
                        partNumbers.Add(new PartNumber(int.Parse(number), new Point(i, startIndex), new Point(i, j), isAdjacentSymbol));
                        // add new items 
                        adjacentAsterisks.AddRange(getAllAdjacentToAsterisk(i, j, lines));
                        // remove dupes
                        adjacentAsterisks = adjacentAsterisks.Distinct().ToList();

                        foreach (var asterisk in adjacentAsterisks)
                        {
                            if (!possibleGears.ContainsKey(asterisk))
                            {
                                possibleGears.Add(asterisk, new Point(1, int.Parse(number)));
                            }
                            else
                            {
                                var value = possibleGears[asterisk];
                                value.X++;
                                value.Y *= int.Parse(number);
                                possibleGears[asterisk] = value;
                            }

                        }

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
            }
            Console.WriteLine(total); // Part 1 -  525181

            total = 0;

            foreach (KeyValuePair<Point, Point> entry in possibleGears)
            {
                if (entry.Value.X == 2) 
                {
                    total += entry.Value.Y;
                }
            }
            Console.WriteLine(total); // Part 2 -  84289137
        }

        static bool isSymbol(char c)
        {
            return !char.IsLetterOrDigit(c) && c != '.';
        }

        static bool isAdjacentToSymbol(int i, int j, List<string> lines)
        {
            int numRows = lines.Count;
            int numCols = lines[0].Length;

            bool isAdjacent = false;


            if (i > 0 && j > 0 && isSymbol(lines[i - 1][j - 1]))
            {
                isAdjacent = true;
               
            }

            if (i > 0 && isSymbol(lines[i - 1][j]))
            {
                isAdjacent = true;
                
            }

            if (i > 0 && j < numCols - 1 && isSymbol(lines[i - 1][j + 1]))
            {
                isAdjacent = true;
                
            }

            if (j > 0 && isSymbol(lines[i][j - 1]))
            {
                isAdjacent = true;
                
            }

            if (j < numCols - 1 && isSymbol(lines[i][j + 1]))
            {
                isAdjacent = true;
                
            }

            if (i < numRows - 1 && j > 0 && isSymbol(lines[i + 1][j - 1]))
            {
                isAdjacent = true;
                
            }

            if (i < numRows - 1 && isSymbol(lines[i + 1][j]))
            {
                isAdjacent = true;
                
            }

            if (i < numRows - 1 && j < numCols - 1 && isSymbol(lines[i + 1][j + 1]))
            {
                isAdjacent = true;
                
            }

            return isAdjacent;
        }


        static List<Point> getAllAdjacentToAsterisk(int i, int j, List<string> lines)
        {
            int numRows = lines.Count;
            int numCols = lines[0].Length;

            List<Point> adjacentAsterisks = new List<Point>();

            if (i > 0 && j > 0 && lines[i - 1][j - 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i - 1, j - 1));
            }

            if (i > 0 && lines[i - 1][j] == '*')
            {
                adjacentAsterisks.Add(new Point(i - 1, j));
            }

            if (i > 0 && j < numCols - 1 && lines[i - 1][j + 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i - 1, j + 1));
            }

            if (j > 0 && lines[i][j - 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i, j - 1));
            }

            if (j < numCols - 1 && lines[i][j + 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i, j + 1));
            }

            if (i < numRows - 1 && j > 0 && lines[i + 1][j - 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i + 1, j - 1));
            }

            if (i < numRows - 1 && lines[i + 1][j] == '*')
            {
                adjacentAsterisks.Add(new Point(i + 1, j));
            }

            if (i < numRows - 1 && j < numCols - 1 && lines[i + 1][j + 1] == '*')
            {
                adjacentAsterisks.Add(new Point(i + 1, j + 1));

            }

            return adjacentAsterisks;

        }
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

