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
    public class day11
    {
        public static void Run()
        {
            List<string> lines = Helper.get_input("day11_input.txt");
            //List<string> lines = Helper.get_input("day11_sample_input.txt");

            int numberRows = lines.Count;
            int numberCols = lines[0].Length;

            char[,] grid = new char[numberRows, numberCols];

            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberCols; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }

            List<int> emptyRows = getEmptyRows(lines);
            List<int> emptyCols = getEmptyCols(lines);


            List<Galaxy> galaxies = new List<Galaxy>();
            int numberGalaxies = 0;

            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberCols; j++)
                {
                    if (grid[i, j] == '#')
                    {
                        numberGalaxies++;
                        galaxies.Add(new Galaxy(rowNum: i, colNum: j, id: numberGalaxies));
                    }
                    //Console.Write(grid[i, j]);
                }
                //Console.WriteLine();    
            }

            foreach (var i in galaxies)
            {
                //Console.WriteLine(i);
            }

            long totalDistances = 0;
            //int emptyOffset = 2; // Part 1 
            int emptyOffset = 1000000; // Part 2 

            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    int totalDistance = getShortestDistance(galaxies[i], galaxies[j], emptyRows, emptyCols, emptyOffset);
                    //Console.WriteLine(totalDistance);
                    totalDistances += totalDistance;
                }
            }
        
            //Console.WriteLine(totalDistances); // Part 1 - 9599070
            Console.WriteLine(totalDistances); // Part 2 - 842645913794

        }

        public static int getShortestDistance(Galaxy g1, Galaxy g2, List<int> emptyRows, List<int> emptyCols, int emptyOffset)
        {
            int totalDistance = 0;

            int x = g1.RowNum;
            int y = g1.ColNum;

            if (x > g2.RowNum)
            {
                while (x != g2.RowNum)
                {
                    x--;
                    if (emptyRows.Contains(x))
                    {
                        totalDistance += emptyOffset;
                    } else
                    {
                        totalDistance++;
                    }                    
                }
            }
            else
            {
                while (x != g2.RowNum)
                {
                    x++;
                    if (emptyRows.Contains(x))
                    {
                        totalDistance += emptyOffset;
                    }
                    else
                    {
                        totalDistance++;
                    }
                }
            }

            if (y > g2.ColNum)
            {
                while (y != g2.ColNum)
                {
                    y--;
                    if (emptyCols.Contains(y))
                    {
                        totalDistance += emptyOffset;
                    }
                    else
                    {
                        totalDistance++;
                    }
                }
            }
            else
            {
                while (y != g2.ColNum)
                {
                    y++;
                    if (emptyCols.Contains(y))
                    {
                        totalDistance += emptyOffset;
                    }
                    else
                    {
                        totalDistance++;
                    }
                }
            }
            return totalDistance;
        }

        public static List<int> getEmptyRows(List<string> lines)
        {
            int numberRows = lines.Count;
            List<int> emptyRows = new List<int>();

            // check for empty rows 
            for (int i = 0; i < numberRows; i++)
            {
                string line = lines[i];
                if (line.All(ch => ch == line[0]) && line[0] == '.')
                {
                    emptyRows.Add(i);
                }
            }
            return emptyRows;
        }
        public static List<int> getEmptyCols(List<string> lines)
        {
            int numberRows = lines.Count;
            int numberCols = lines[0].Length;
            List<int> emptyCols = new List<int>();

            // check for empty cols 
            for (int i = 0; i < numberCols; i++)
            {
                string column = "";
                for (int j = 0; j < numberRows; j++)
                {
                    column += lines[j][i];
                }

                if (column.All(ch => ch == column[0]) && column[0] == '.')
                {
                    emptyCols.Add(i);
                }
            }

            return emptyCols;
        }

    }

    public class Galaxy
    {
        public int RowNum;
        public int ColNum;
        public int Id;

        public Galaxy(int rowNum, int colNum, int id)
        {
            RowNum = rowNum;
            ColNum = colNum;
            Id = id;
        }
        public override string ToString()
        {
            return "Galaxy: " + Id + " , row = " + RowNum + " , col = " + ColNum;
        }
    }

}

