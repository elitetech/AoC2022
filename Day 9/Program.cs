namespace AoC2022
{
    class Day_9
    {
        enum Direction
        {
            Up = 'U',
            Down = 'D',
            Left = 'L',
            Right = 'R'
        }

        private static Dictionary<int, int> visited = new();

        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine($"Part 1: {Part1(input)}");
            Console.WriteLine($"Part 2: {Part2(input)}");
        }

        private static int Part1(string[] input)
        {
            Head head = new() { x = 0, y = 0 };
            Tail tail = new() { x = 0, y = 0 };
            visited.Add(tail.x, tail.y);
            foreach(var line in input)
            {

            }
            return 0;
        }
        private static int Part2(string[] input)
        {
            return 0;
        }
    }

    class Head
    {
        public int x { get; set; }
        public int y { get; set; }
    }
        
    class Tail
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}