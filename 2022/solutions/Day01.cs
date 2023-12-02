using System.Reflection;

namespace solutions
{
    internal class Day01
    {
        static void Main(string[] args)
        {
            List<string> lines = Helper.get_input("day01_input.txt");

            int calories = 0;
            int maxCalories = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    if (maxCalories < calories)
                    {
                        maxCalories = calories;
                    }
                    calories = 0;
                }
                else
                {
                    calories += int.Parse(line);
                }
            }

            Console.WriteLine(maxCalories);

            // Part 1 - 69281
        }
    }
}
