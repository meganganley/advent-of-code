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

            char[,] grid = getGrid(lines);

            int numberRows = grid.GetLength(0);
            int numberCols = grid.GetLength(1);

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
                Console.WriteLine(i);
            }

            int totalDistances = 0;

            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i; j < galaxies.Count; j++)
                {
                    totalDistances += getShortestDistance(galaxies[i], galaxies[j]);
                }
            }

            Console.WriteLine(totalDistances); // 9599070

        }

        public static int getShortestDistance(Galaxy g1, Galaxy g2)
        {
            int totalDistance = 0;

            int x = g1.RowNum;
            int y = g1.ColNum;

            if (x > g2.RowNum)
            {
                while (x != g2.RowNum)
                {
                    x--;
                    totalDistance++;
                }
            }
            else
            {
                while (x != g2.RowNum)
                {
                    x++;
                    totalDistance++;
                }
            }

            if (y > g2.ColNum)
            {
                while (y != g2.ColNum)
                {
                    y--;
                    totalDistance++;
                }
            }
            else
            {
                while (y != g2.ColNum)
                {
                    y++;
                    totalDistance++;
                }
            }
            return totalDistance;
        }


        public static char[,] getGrid(List<string> lines)
        {
            int numberRows = lines.Count;
            int numberCols = lines[0].Length;

            List<int> emptyRows = new List<int>();
            List<int> emptyCols = new List<int>();

            // populate grid, check for empty rows 
            for (int i = 0; i < numberRows; i++)
            {
                string line = lines[i];
                if (line.All(ch => ch == line[0]) && line[0] == '.')
                {
                    emptyRows.Add(i);
                }
            }

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

            numberRows += emptyRows.Count;
            numberCols += emptyCols.Count;

            string emptyRow = new string('.', numberCols);

            int count = 0;
            foreach (var i in emptyRows)
            {
                lines.Insert(i + count, emptyRow);
                count++;
            }

            count = 0;
            foreach (var i in emptyCols)
            {
                for (int j = 0; j < numberRows; j++)
                {
                    lines[j] = lines[j].Insert(i+count, ".");
                }
                count++;
            }

            /*
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }*/


            char[,] grid = new char[numberRows, numberCols];

            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberCols; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }

            return grid;
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

