namespace AoC2022
{
    class Day_1
    {
        static void Main()
        {
            var sample = System.IO.File.ReadAllLines("input.txt");
            var p1 = Part1(sample);
            var p2 = Part2(sample);
            Console.WriteLine($"Part 1: {p1}");
            Console.WriteLine($"Part 2: {p2}");
        }

        public static int Part1(string[] sample)
        {
            List<int> elvesCals = new();

            int cals = 0;
            foreach (var line in sample)
            {
                if (line.Length == 0)
                {
                    elvesCals.Add(cals);
                    cals = 0;
                    continue;
                }
                cals += Convert.ToInt32(line);
            }
            return elvesCals.Max();
        }

        public static int Part2(string[] sample)
        {
            List<int> elvesCals = new();

            int cals = 0;
            foreach (var line in sample)
            {
                if (line.Length == 0)
                {
                    elvesCals.Add(cals);
                    cals = 0;
                    continue;
                }
                cals += Convert.ToInt32(line);
            }
            List<int> top = elvesCals.OrderBy(x => x).TakeLast(3).ToList();

            return top.Sum();
        }
    }
}