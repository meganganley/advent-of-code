namespace solutions
{
    public class day04
    {
        public static void Run()
        {
            //List<string> lines = Helper.get_input("day04_input_sample.txt");
            List<string> lines = Helper.get_input("day04_input.txt");

            int total = 0;

            int[] numberCards = Enumerable.Repeat(1, lines.Count).ToArray(); ; // every element once to start

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                var input = line.Split(':')[1].Split('|');
                List<int> winningNumbers = getNumbers(input[0]);
                List<int> myNumbers = getNumbers(input[1]);

                var commonList = winningNumbers.Intersect(myNumbers).ToList();

                // some winning numbers
                if (commonList.Count > 0)
                {
                    total += (int)Math.Pow(2, commonList.Count-1);
                }
                for (int j = 0; j < commonList.Count; j++)
                {
                    numberCards[i+j+1] += numberCards[i];
                }
               
            }

            Console.WriteLine(total);    // 17782
            Console.WriteLine(numberCards.Sum());    // 8477787


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

