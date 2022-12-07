namespace AoC2022
{
    class Day_3
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
            int total = 0;
            foreach (var line in sample)
            {
                var sack = line.ToCharArray();
                var compartmentOne = sack.Take(sack.Length / 2).ToArray();
                var compartmentTwo = sack.Skip(sack.Length / 2).ToArray();
                var common = compartmentOne.Intersect(compartmentTwo).ToArray();
                total += GetPriority(common[0]);
            }
            return total;
        }

        public static int Part2(string[] sample)
        {
            int total = 0;
            List<char[]> group = new();
            foreach (var line in sample)
            {
                var sack = line.ToCharArray();
                group.Add(sack);
                if (group.Count() == 3)
                {
                    var common = group[0].Intersect(group[1]).Intersect(group[2]).ToArray();
                    total += GetPriority(common[0]);
                    group = new();
                }
            }
            return total;
        }

        public static int GetPriority(char character)
        {

            int index = (int)character % 32;

            index += char.IsUpper(character) ? 26 : 0;

            return index;
        }
    }
}