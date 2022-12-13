using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static Day_11.Program;

namespace Day_11
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var monkeys = Setup(input);
            var part1 = Part1(monkeys);
            Console.WriteLine($"Part 1: {part1}");//78960
            monkeys = Setup(input);
            var part2 = Part2(monkeys);
            Console.WriteLine($"Part 2: {part2}");//14561971968
        }
        
        private static List<Monkey> Setup(string input)
        {
            return input.Split("\r\n\r\n")
                .Select(x => Monkey.Parse(x.Split("\r\n"))).ToList();
        }

        private static long Part1(List<Monkey> monkeys)
        {
            for(var i =0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.PlayTurn(monkeys, null);
                }
            }
            var top2 = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).ToList();
            return top2[0] * top2[1];
        }
        
        private static long Part2(List<Monkey> monkeys)
        {
            var factor = monkeys.Aggregate(1, (c, m) => c * m.DivisibilityCheck);
            for (var i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.PlayTurn(monkeys, factor);
                }
            }
            var top2 = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).ToList();
            return top2[0] * top2[1];
        }
        
    }


    internal partial class Monkey
    {
        public int Id { get; private set; }

        public Queue<long> Items { get; private set; } = new(); // yeah, bad, but you gotta satisfy the null checker.

        public Func<long, long> Operation { get; private set; } = i => i;

        public int DivisibilityCheck { get; private set; }

        public int TrueDestination { get; private set; }

        public int FalseDestination { get; private set; }

        public long InspectionCount { get; private set; }

        public void PlayTurn(List<Monkey> monkeys, int? moduloFactor)
        {
            while (Items.Count > 0)
            {
                // count this inspection
                InspectionCount++;

                // get the item from the queue
                var item = Items.Dequeue();

                // run the operation on the item
                item = Operation(item);

                if (moduloFactor == null)
                {
                    // get bored with the item
                    item /= 3;
                }
                else
                {
                    // manage the worry
                    item %= moduloFactor.Value;
                }

                // see who to throw the item to
                if (item % DivisibilityCheck == 0)
                {
                    monkeys[TrueDestination].Items.Enqueue(item);
                }
                else
                {
                    monkeys[FalseDestination].Items.Enqueue(item);
                }
            }
        }

        public static Monkey Parse(IEnumerable<string> input)
        {
            var lines = input.ToArray();

            if (lines.Length != 6)
            {
                throw new InvalidDataException($"Expected 6 lines to define a monkey, got {lines.Length}");
            }

            var idMatch = IdRegex().Match(lines[0]);

            if (!idMatch.Success)
            {
                throw new InvalidDataException("Could not parse monkey id");
            }

            var itemsMatch = ItemsRegex().Match(lines[1]);

            if (!itemsMatch.Success)
            {
                throw new InvalidDataException("Could not parse starting item list");
            }

            var operationMatch = OperationRegex().Match(lines[2]);

            if (!operationMatch.Success)
            {
                throw new InvalidDataException("Could not parse operation");
            }

            var testMatch = TestRegex().Match(lines[3]);

            if (!testMatch.Success)
            {
                throw new InvalidDataException("Could not parse test");
            }

            var trueMatch = TrueActionRegex().Match(lines[4]);

            if (!trueMatch.Success)
            {
                throw new InvalidDataException("Could not true action");
            }

            var falseMatch = FalseActionRegex().Match(lines[5]);

            if (!falseMatch.Success)
            {
                throw new InvalidDataException("Could not false action");
            }

            var operand1 = Expression.Parameter(typeof(long), "old");
            var operand2 = long.TryParse(operationMatch.Groups["operand"].Value, out var constant) ? (Expression)Expression.Constant(constant, typeof(long)) : operand1;
            var opExpression = operationMatch.Groups["op"].Value switch
            {
                "*" => Expression.Multiply(operand1, operand2),
                "+" => Expression.Add(operand1, operand2),
                _ => throw new NotImplementedException($"The operation {operationMatch.Groups["op"].Value} is not implemented."),
            };

            return new Monkey()
            {
                Id = int.Parse(idMatch.Groups["id"].Value),
                Items = new Queue<long>(itemsMatch.Groups["worry"].Captures.Select(c => long.Parse(c.Value))),
                Operation = Expression.Lambda<Func<long, long>>(opExpression, operand1).Compile(),
                DivisibilityCheck = int.Parse(testMatch.Groups["value"].Value),
                TrueDestination = int.Parse(trueMatch.Groups["destination"].Value),
                FalseDestination = int.Parse(falseMatch.Groups["destination"].Value),
            };
        }

        [GeneratedRegex(@"Monkey (?<id>\d+):")]
        private static partial Regex IdRegex();

        [GeneratedRegex(@"  Starting items: ((?<worry>\d+)((, )|))+")]
        private static partial Regex ItemsRegex();

        [GeneratedRegex(@"  Operation: new = old (?<op>[\*\+]) (?<operand>(old|\d+))")]
        private static partial Regex OperationRegex();

        [GeneratedRegex(@"  Test: divisible by (?<value>\d+)")]
        private static partial Regex TestRegex();

        [GeneratedRegex(@"    If true: throw to monkey (?<destination>\d+)")]
        private static partial Regex TrueActionRegex();

        [GeneratedRegex(@"    If false: throw to monkey (?<destination>\d+)")]
        private static partial Regex FalseActionRegex();
    }
}