using System.Collections.Immutable;

namespace Day_12
{
    record Map(ImmutableDictionary<Point, int> Heights, Point Start, Point End)
    {
        public IEnumerable<Point> GetAdjacentPointsWithinReach(Point point)
        {
            if (!Heights.TryGetValue(point, out var height)) return Enumerable.Empty<Point>();

            var max = height + 1;
            return point.GetAdjacentPoints().Where(x => Heights.TryGetValue(x, out var ph) && ph <= max);
        }
        
        public static Map GetMap(string[] input)
        {
            Point start = default;
            Point end = default;
            var map = ImmutableDictionary.CreateBuilder<Point, int>();
            for (var row = 0; row < input.Length; ++row)
            {
                var line = input[row];
                for (var col = 0; col < line.Length; ++col)
                {
                    var c = line[col];
                    var pos = new Point(col, row);

                    if (c == 'S') (map[pos], start) = (1, pos);
                    else if (c == 'E') (map[pos], end) = (26, pos);
                    else map[pos] = (int)c % 32;

                }
            }

            return new(map.ToImmutable(), start, end);
        }
    }
        
}