using static System.Net.Mime.MediaTypeNames;

namespace solutions
{
    public class day06
    {
        public static void Run()
        {
            //List<string> lines = Helper.get_input("day06_input_sample.txt");
            List<string> lines = Helper.get_input("day06_input.txt");

            // Part 1 
            string[] str_times = lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split(' ');
            str_times = str_times.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            long[] times = Array.ConvertAll(str_times, s => long.Parse(s));

            string[] str_distances = lines[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split(' ');
            str_distances = str_distances.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            long[] distances = Array.ConvertAll(str_distances, s => long.Parse(s));

            // Part 2 
            long time = long.Parse(lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));
            long distance = long.Parse(lines[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));


            int numberWays = 1;
            for (int i = 0; i < times.Count(); i++)
            {
                numberWays *= CalculateHoldOptions(times[i], distances[i]).Count;
            }
            
            Console.WriteLine(numberWays);    // Part 1 - 1108800
            Console.WriteLine(CalculateHoldOptions(time, distance).Count);    // Part 2 - 36919753

        }

        public static List<int> CalculateHoldOptions(long time, long distance)
        {
            List<int> holdOptions = new List<int>();
            for (int i = 1; i < time; i++)
            {                
                if ((time - i) * i > distance)
                {
                    holdOptions.Add(i);
                }
            }
            return holdOptions;
        }  
        
    }
}

