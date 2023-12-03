using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace solutions
{
    internal class day03
    {        
        static void Main(string[] args)
        {
            //List<string> lines = Helper.get_input("day03_input_sample.txt");
            List<string> lines = Helper.get_input("day03_input.txt");

            int numCols = lines[0].Length;

            Dictionary<Point, Point> possibleGears = new Dictionary<Point, Point>(); // co-ordinate of asterisk, then Point(countAdjacent, productOfAdjacent)
            int total = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

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
                        //partNumbers.Add(new PartNumber(int.Parse(number), new Point(i, startIndex), new Point(i, j), isAdjacentSymbol));
                        if (isAdjacentSymbol)
                        {
                            total += int.Parse(number);
                        }

                        // maintain asterisk dictionary with our findings from this number
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

                        // tidy up for remaining numbers in row
                        inNumber = false;
                        number = "";
                        isAdjacentSymbol = false;
                        adjacentAsterisks = new List<Point>();

                    }
                    // detected start of new number
                    else if (Char.IsNumber(c) && !inNumber)
                    {
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
                        if (isAdjacentSymbol)
                        {
                            total += int.Parse(number);
                        }

                        // add new items 
                        adjacentAsterisks.AddRange(getAllAdjacentToAsterisk(i, j, lines));
                        // remove dupes
                        adjacentAsterisks = adjacentAsterisks.Distinct().ToList();

                        // maintain asterisk dictionary with our findings from this number
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


            if (i > 0 && j > 0 && isSymbol(lines[i - 1][j - 1]))
            {
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

