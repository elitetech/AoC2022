using System.Text.Json;

namespace AoC2022
{
    class Day_9
    {
        public enum Direction
        {
            Up = 'U',
            Down = 'D',
            Left = 'L',
            Right = 'R'
        }

        public static List<Knot> knots = new();
        public static Dictionary<string, int> visited = new();

        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine($"Part 1: {Run(input,2)}"); // 5907
            Console.WriteLine($"Part 2: {Run(input,10)}"); // 2303
        }

        private static int Run(string[] input, int knotCount)
        {
            visited = new();
            knots.RemoveAll(x => true);
            for(var i = 0; i < knotCount; i++) knots.Add(new() { position = new() { x = 0, y = 0 } });

            visited.Add(JsonSerializer.Serialize(knots.Last()), 1);

            foreach (var line in input)
            {
                var direction = (Direction)line[0];
                var distance = int.Parse(line[1..]);
                for (var i = 0; i < distance; i++)
                {
                    CalculateMove(0, knotCount, direction);
                }
            }
            return visited.Count();
        }

        private static void CalculateMove(int index, int knotCount, Direction direction)
        {
            knots[index].position = Move(knots[index].position, direction);
            if (index == knotCount - 1)
            {
                if (visited.ContainsKey(JsonSerializer.Serialize(knots[index]))) visited[JsonSerializer.Serialize(knots[index])]++;
                else visited.Add(JsonSerializer.Serialize(knots[index]), 1);
                return;
            }

            var nextIndex = index + 1;
            if (GetDistance(knots[index].position, knots[nextIndex].position) < 2) return;
            if (knots[index].position.y == knots[nextIndex].position.y) // move once in y direction
            {
                CalculateMove(nextIndex, knotCount, knots[index].position.x > knots[nextIndex].position.x ? Direction.Right : Direction.Left);
            }
            else if (knots[index].position.x == knots[nextIndex].position.x) // move once in x direction
            {
                CalculateMove(nextIndex, knotCount, knots[index].position.y > knots[nextIndex].position.y ? Direction.Up : Direction.Down);
            }
            else // move once in both directions
            {
                if (knots[index].position.y > knots[nextIndex].position.y && knots[index].position.x > knots[nextIndex].position.x)
                {
                    knots[nextIndex].position = Move(knots[nextIndex].position, Direction.Up);
                    CalculateMove(nextIndex, knotCount, Direction.Right);
                }
                else if (knots[index].position.y > knots[nextIndex].position.y && knots[index].position.x < knots[nextIndex].position.x)
                {
                    knots[nextIndex].position = Move(knots[nextIndex].position, Direction.Up);
                    CalculateMove(nextIndex, knotCount, Direction.Left);
                }
                else if (knots[index].position.y < knots[nextIndex].position.y && knots[index].position.x > knots[nextIndex].position.x)
                {
                    knots[nextIndex].position = Move(knots[nextIndex].position, Direction.Down);
                    CalculateMove(nextIndex, knotCount, Direction.Right);
                }
                else if (knots[index].position.y < knots[nextIndex].position.y && knots[index].position.x < knots[nextIndex].position.x)
                {
                    knots[nextIndex].position = Move(knots[nextIndex].position, Direction.Down);
                    CalculateMove(nextIndex, knotCount, Direction.Left);
                }
            }
        }

        private static Position Move(Position position, Direction direction)
        {
            return direction switch
            {
                Direction.Up => new() { x = position.x, y = position.y + 1 },
                Direction.Down => new() { x = position.x, y = position.y - 1 },
                Direction.Left => new() { x = position.x - 1, y = position.y },
                Direction.Right => new() { x = position.x + 1, y = position.y },
                _ => throw new Exception("Invalid direction")
            };
        }

        private static double GetDistance(Position head, Position tail)
        {
            return Math.Sqrt(Math.Pow(head.x - tail.x, 2) + Math.Pow(head.y - tail.y, 2));
        }
    }

    internal class Knot
    {
        public Position position { get; set; }
    }

    internal class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}