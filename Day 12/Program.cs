using System.IO;

namespace Day_12
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            var input = File.ReadAllLines("input.txt");

            var map = Map.GetMap(input);
            Console.WriteLine($"Part 1: {Part1(map)}");// not 78960 
            Console.WriteLine($"Part 2: {Part2(map)}");//
        }

        private static int Part1(Map map)
        {
            return GetPathLength(map, map.Start) ?? -1;
        }

        private static int Part2(Map map)
        {
            return map.Heights
                .Where(x => x.Value == 1)
                .Select(x => GetPathLength(map, x.Key))
                .Where(x => x.HasValue)
                .Min() ?? -1;
        }

        public static int? GetPathLength(Map map, Point start)
        {
            var visited = new Dictionary<Point, int>() { [start] = 0 };
            var queue = new Queue<Point>(visited.Keys);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if (point == map.End)
                    break;

                var d = visited[point];
                var adjacent = map.GetAdjacentPointsWithinReach(point)
                                  .Where(x => !visited.ContainsKey(x));
                foreach (var item in adjacent)
                {
                    visited[item] = d + 1;
                    queue.Enqueue(item);
                }
            }

            return visited.TryGetValue(map.End, out var result)
              ? result
              : default(int?);
        }
    }
        
}