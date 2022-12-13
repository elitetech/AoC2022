namespace Day_12
{
    record struct Point(int x, int y)
    {
        public  IEnumerable<Point> GetAdjacentPoints()
        {
            yield return new(x - 1, y);
            yield return new(x, y - 1);
            yield return new(x + 1, y);
            yield return new(x, y + 1);
        }
    }
        
}