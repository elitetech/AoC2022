namespace AoC2022
{
    class Day_7
    {
        public static Dictionary<string, Dictionary<string, int>> directoryTree = new();
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            SetupTree(input);
            Console.WriteLine($"Part 1: {Part1(input)}");// not 905276
            Console.WriteLine($"Part 2: {Part2(input)}");
        }
        
        public static int Part1(string[] input)
        {
            int total = 0;
            foreach(var directory in directoryTree)
            {
                var dirs = directoryTree
                    .Where(x => x.Key
                        .StartsWith(directory.Key)).ToList();
                var dirSize = dirs
                        .Sum(x => x.Value
                            .Sum(y => y.Value));
                if (dirSize <= 100000)
                {
                    total += dirSize;
                }
            }
            return total;
        }

        public static int Part2(string[] input)
        {
            return 0;
        }

        private static void SetupTree(string[] input)
        {
            var currentDir = string.Empty;
            foreach(var item in input)
            {
                if (item.Contains('$'))
                {
                    if (!item.Contains("ls"))
                    {
                        var dir = item.Split(' ')[2];
                        if (dir != "..") currentDir = currentDir.Length == 0 ? dir : $"{currentDir}{dir}/";
                        else currentDir = GetParent(currentDir);
                    }
                }
                else
                {
                    var itemParts = item.Split(' ');
                    if (itemParts[0] != "dir")
                    {
                        if (!directoryTree.ContainsKey(currentDir)) directoryTree.Add(currentDir, new());
                        directoryTree[currentDir].Add(itemParts[1], int.Parse(itemParts[0]));
                    }
                }
            }
        }

        private static string GetParent(string directory)
        {
            directory = directory.TrimEnd('/');
            return directory.Substring(0, directory.LastIndexOf('/') + 1);
        }
    }
}