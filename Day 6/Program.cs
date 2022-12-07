namespace AoC2022
{
    class Day_6
    {
        public static void Main()
        {
            var sample = File.ReadAllText("input.txt");

            System.Console.WriteLine($"Part 1: {Part1(sample)}");
            System.Console.WriteLine($"Part 2: {Part2(sample)}");
        }

        public static int Part1(string sample)
        {
            return GetStartOf(sample, 4);
        }

        public static int Part2(string sample)
        {
            return GetStartOf(sample, 14);
        }

        private static int GetStartOf(string sample, int lengthOfStartOf)
        {
            for (var i = 0; i < sample.Length - lengthOfStartOf; i++)
            {
                var unique = sample
                    .Substring(i, lengthOfStartOf)
                    .GroupBy(x => x)
                    .Any(g => g.Count() > 1);
                if (!unique) return i + lengthOfStartOf;
            }
            return 0;
        }
    }
}