using System.Runtime.CompilerServices;

namespace Day10
{
    class Day10
    {

        public static int cycle = 0;
        public static int register = 1;
        public static int strength = 0;
        public static int checkIndex = 20;
        public const int row = 40;
        public static int position = 0;
        public static (int start, int end) draw = (1, 3);
        public static List<string> screen = new();
        public static string screenLine = string.Empty;

        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            Setup(input);
            Console.WriteLine();
            Console.WriteLine($"Part 1: {Part1()}"); // 420
            Console.WriteLine($"Part 2: ");
            Part2();
        }

        public static int Part1()
        {
            return strength;
        }

        public static void Part2()
        {
            foreach (var line in screen)
            {
                Console.WriteLine(line);
            }
        }

        public static void Setup(string[] input)
        {
            foreach (var line in input)
            {

                NextCycle();
                if (line == "noop") continue;

                NextCycle();
                int addX = int.Parse(line.Split(' ')[1]);
                register += addX;

                draw = (register, register + 2);
            }
        }

        public static void NextCycle()
        {
            cycle++;
            position++;
            if (cycle == checkIndex)
            {
                strength += cycle * register;
                checkIndex += 40;
            }
            screenLine += position >= draw.start && position <= draw.end ? "#" : ".";
            if (position == row)
            {
                position = 0;
                screen.Add(screenLine);
                screenLine = string.Empty;
            }
        }
    }
}