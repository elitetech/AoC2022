namespace AoC
{
    class AoC
    {
        static void Main()
        {
            var sample = System.IO.File.ReadAllLines("sample.txt");
            var p1 = Part1(sample);
            var p2 = Part2(sample);
            Console.WriteLine($"Part 1: {p1}");
            Console.WriteLine($"Part 2: {p2}");
        }

        public static int Part1(string[] sample)
        {
            int total = 0;
            foreach(var line in sample)
            {
                var pair = line.Split(',');
                int xMin = int.Parse(pair[0].Split('-')[0]);
                int xMax = int.Parse(pair[0].Split('-')[1]);
                int yMin = int.Parse(pair[1].Split('-')[0]);
                int yMax = int.Parse(pair[1].Split('-')[1]);
                if((inRange(xMax,xMin,yMin) && inRange(xMax,xMin,yMax)) || (inRange(yMax,yMin,xMin) && inRange(yMax,yMin,xMax)))
                {
                    total++;
                }
            }
            return total;
        }

        public static int Part2(string[] sample)
        {
            int total = 0;
            foreach(var line in sample)
            {
                var pair = line.Split(',');
                int xMin = int.Parse(pair[0].Split('-')[0]);
                int xMax = int.Parse(pair[0].Split('-')[1]);
                int yMin = int.Parse(pair[1].Split('-')[0]);
                int yMax = int.Parse(pair[1].Split('-')[1]);
                if((inRange(xMax,xMin,yMin) || inRange(xMax,xMin,yMax)) || (inRange(yMax,yMin,xMin) || inRange(yMax,yMin,xMax)))
                {
                    total++;
                }
            }
            return total;
        }

        public static bool inRange(int high, int low, int checkNum)
        {
            return ((checkNum-high)*(checkNum-low) <= 0);
        }
    }
}