using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2022
{
    class Day_5
    {
        static void Main()
        {
            var filename = "input.txt";
            var p1 = Part1(filename);
            var p2 = Part2(filename);
            Console.WriteLine($"Part 1: {p1}");
            Console.WriteLine($"Part 2: {p2}");
        }

        public static string Part1(string filename)
        {
            var stacks = GetInitialStacks(filename);
            var moves = GetMoveOrders(filename);
            string topCrates = string.Empty;

            foreach (var move in moves)
            {
                stacks = DoMove(stacks, move);
            }
            var top = stacks.Select(x => x.Value.Count() > 0 ? x.Value.Last() : " ").ToArray();
            return string.Join("", top);
        }

        public static string Part2(string filename)
        {
            var stacks = GetInitialStacks(filename);
            var moves = GetMoveOrders(filename);
            string topCrates = string.Empty;

            foreach (var move in moves)
            {
                stacks = DoMove(stacks, move, true);
            }
            var top = stacks.Select(x => x.Value.Count() > 0 ? x.Value.Last() : " ").ToArray();
            return string.Join("", top);
        }

        private static Dictionary<int, List<string>> GetInitialStacks(string filename)
        {
            Dictionary<int, List<string>> stacks = new();
            var stackString = System.IO.File.ReadAllText(filename).Split("\r\n\r\n")[0];
            var stackLines = stackString.Split("\n").Where(x => x.Contains("[")).ToArray();
            int stackCount = stackLines[0].Length / 4;
            for (var i = 0; i < stackCount; i++)
            {
                var stack = stackLines
                    .Select(x => x.Substring(i * 4 + 1, 1))
                    .Where(x => x.Trim().Length > 0)
                    .Reverse()
                    .ToList();
                stacks.Add(i + 1, stack);
            }
            return stacks;
        }

        private static List<GroupCollection> GetMoveOrders(string filename)
        {
            List<GroupCollection> moves = new();
            var regex = new Regex("^move (.*?) from (.*?) to (.*?)$");
            var moveString = System.IO.File.ReadAllText(filename).Split("\r\n\r\n")[1];
            var lines = moveString.Split("\n");
            moves = lines.Select(x => regex.Match(x).Groups).ToList();
            return moves;
        }

        private static Dictionary<int, List<string>> DoMove(Dictionary<int, List<string>> stacks, GroupCollection move, bool moveMultiple = false)
        {
            var amount = int.Parse(move[1].Value);
            var from = int.Parse(move[2].Value);
            var to = int.Parse(move[3].Value);
            if (moveMultiple)
            {
                var crates = stacks[from].TakeLast(amount).ToList();
                stacks[from].RemoveRange(stacks[from].Count() - amount, amount);
                stacks[to].AddRange(crates);
            }
            else
            {
                for (var i = 0; i < amount; i++)
                {
                    var crate = stacks[from].Last();

                    stacks[from].RemoveAt(stacks[from].Count() - 1);
                    stacks[to].Add(crate);
                }
            }
            return stacks;
        }
    }
}