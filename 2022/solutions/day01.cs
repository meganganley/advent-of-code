using System.Reflection;

namespace solutions
{
    internal class day01
    {
        static void Main(string[] args)
        {
            List<string> lines = Helper.get_input("day01_input.txt");

            int calories = 0;
            int maxCalories = 0;

            List<int> allCalories = new List<int>();

            foreach (string line in lines)
            {
                // move on to the next elf
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    if (maxCalories < calories)
                    {
                        maxCalories = calories;
                    }
                    allCalories.Add(calories);
                    calories = 0;
                }
                else
                {
                    calories += int.Parse(line);
                }
            }
            allCalories.Sort();
            allCalories.Reverse();

            Console.WriteLine(maxCalories); // Part 1 - 69281
            Console.WriteLine(allCalories.Take(3).Sum()); // Part 2 - 201524

        }
    }
}
