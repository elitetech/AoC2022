namespace AoC2022
{
    class Day_2
    {
        static Dictionary<string, Dictionary<string, int>> conditions = new();
        static void Main()
        {
            var sample = System.IO.File.ReadAllLines("input.txt");
            SetupConditions();
            var p1 = Part1(sample);
            var p2 = Part2(sample);
            Console.WriteLine($"Part 1: {p1}");
            Console.WriteLine($"Part 2: {p2}");
        }

        public static int Part1(string[] sample)
        {
            int total = 0;
            foreach (var line in sample)
            {
                var op = line.Split(' ')[0];
                var me = line.Split(' ')[1];
                total += RunGame(op, me);
            }

            return total;
        }

        public static int Part2(string[] sample)
        {
            int total = 0;
            foreach (var line in sample)
            {
                var op = line.Split(' ')[0];
                var gameResult = line.Split(' ')[1];
                string me = string.Empty;
                switch (line.Split(' ')[1])
                {
                    case "X": // lose
                        me = conditions[op].FirstOrDefault(x => x.Value == 0).Key;
                        break;
                    case "Y": // draw
                        me = conditions[op].FirstOrDefault(x => x.Value == 3).Key;
                        break;
                    case "Z": // win
                        me = conditions[op].FirstOrDefault(x => x.Value == 6).Key;
                        break;
                }

                total += RunGame(op, me);
            }

            return total;
        }

        public static int RunGame(string oponent, string me)
        {

            int selectionModifier = 0;
            switch (me)
            {
                case "X": // rock
                    selectionModifier = 1;
                    break;
                case "Y": // papper
                    selectionModifier = 2;
                    break;
                case "Z": // scissor
                    selectionModifier = 3;
                    break;
            }
            return conditions[oponent][me] + selectionModifier;
        }

        public static void SetupConditions()
        {
            // A, X rock
            // B, Y Papper
            // C, Z Scissors
            // loss 0
            // draw 3
            // win 6
            conditions = new();
            Dictionary<string, int> outcome = new();
            outcome.Add("X", 3); // r v r
            outcome.Add("Y", 6); // r v p
            outcome.Add("Z", 0); // r v s
            conditions.Add("A", outcome);
            outcome = new();
            outcome.Add("X", 0); // p v r
            outcome.Add("Y", 3); // p v p
            outcome.Add("Z", 6); // p v s
            conditions.Add("B", outcome);
            outcome = new();
            outcome.Add("X", 6); // s v r
            outcome.Add("Y", 0); // s v p
            outcome.Add("Z", 3); // s v s
            conditions.Add("C", outcome);
        }
    }
}