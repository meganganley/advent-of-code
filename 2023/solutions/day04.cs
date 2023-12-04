using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace solutions
{
    internal class day04
    {
        static void Main(string[] args)
        {
           // List<string> lines = Helper.get_input("day04_input_sample.txt");
            List<string> lines = Helper.get_input("day04_input.txt");

            int total = 0;

            int winningLines = 0;

            foreach (string line in lines)
            {
                var input = line.Split(':')[1].Split('|');
                List<int> winningNumbers = getNumbers(input[0]);
                List<int> myNumbers = getNumbers(input[1]);

                var CommonList = winningNumbers.Intersect(myNumbers).ToList();

                if (CommonList.Count > 0)
                {
                    total += (int)Math.Pow(2, CommonList.Count-1);
                }
               
            }

            Console.WriteLine(total);   

        }

        static List<int> getNumbers(string input)
        {
            var stringNumbers = input.Split(' ');
            // remove empty string elements
            var resultList = stringNumbers.Where(x => x != "").ToList();
            return resultList.Select(int.Parse).ToList();

        }
    }
}

