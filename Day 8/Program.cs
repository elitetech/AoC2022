using System.Data;
using ObjectSerializerForLog;


namespace AoC2022
{
    class Day_7
    {
        public static Dictionary<int, Dictionary<int, int>> treeGrid = new();
        

        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            SetupGrid(input);
            //Console.WriteLine(ObjectSerializerForLog.ObjectSerializerForLog.Log(treeGrid, true));
            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        public static int Part1()
        {
            var total = 0;
            // perimeter of the square grid
            total += treeGrid.FirstOrDefault().Value.Count() ^ 2;

            treeGrid.Where(row => row.Key != 0 && row.Key != treeGrid.Count - 1).ToList().ForEach(row =>
            {
                row.Value.Where(col => col.Key != 0 && col.Key != row.Value.Count - 1).ToList().ForEach(col =>
                {
                    bool notVisible = true;
                    if (treeGrid.Where(x => x.Key < row.Key && x.Value[col.Key] > col.Value).Count() > 0) notVisible = false;
                    if (treeGrid.Where(x => x.Key > row.Key && x.Value[col.Key] > col.Value).Count() > 0) notVisible = false;
                });
            });

            //foreach(var row in treeGrid)
            //{
            //    if (row.Key == 0 || row.Key == treeGrid.Count - 1)
            //    {
            //        total++;
            //        continue;
            //    }
            //    foreach (var col in row.Value)
            //    {
            //        if (col.Key == 0 || col.Key == row.Value.Count - 1)
            //        {
            //            total++;
            //        }

            //    }
            //}


            return total;
        }
        
        public static int Part2()
        {
            return 0;
        }

        private static void SetupGrid(string[] input)
        {
            treeGrid = input
                .Select((x,i) => new {x,i})
                .ToDictionary(x => x.i, x=> x.x
                    .Select((x2,i2) => new { x2, i2 })
                    .ToDictionary(x2 => x2.i2, x2 => int.Parse(x2.x2.ToString())));
        }
    }
}