using System.Data;


namespace AoC2022
{
    class Day_8
    {
        public static List<Tree> trees = new();


        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            SetupTrees(input);
            Console.WriteLine($"Part 1: {Part1(input)}");// 1796
            Console.WriteLine($"Part 2: {Part2(input)}");// 288120
        }

        public static double Part1(string[] input)
        {
            foreach (Tree tree in trees)
            {
                if (tree.x == 0 || tree.y == 0 || tree.x == input.Length - 1 || tree.y == input.Length - 1) 
                { 
                    tree.visible = true; 
                    continue; 
                }
                if (!trees.Where(t => t.y == tree.y).Where(t => t.x < tree.x).Where(t => t.height >= tree.height).Any()) 
                { 
                    tree.visible = true; 
                    continue; 
                }
                if (!trees.Where(t => t.y == tree.y).Where(t => t.x > tree.x).Where(t => t.height >= tree.height).Any()) 
                { 
                    tree.visible = true; 
                    continue; 
                }
                if (!trees.Where(t => t.x == tree.x).Where(t => t.y < tree.y).Where(t => t.height >= tree.height).Any()) 
                { 
                    tree.visible = true; 
                    continue; 
                }
                if (!trees.Where(t => t.x == tree.x).Where(t => t.y > tree.y).Where(t => t.height >= tree.height).Any()) 
                { 
                    tree.visible = true; 
                    continue; 
                }
            }
            return trees.Where(t => t.visible).ToList().Count;
        }

        public static int Part2(string[] input)
        {
            foreach (Tree tree in trees)
            {
                if (trees.Where(t => t.y == tree.y).Where(t => t.x < tree.x).Where(t => t.height >= tree.height).Any())
                {
                    tree.score *= Math.Abs(tree.x - trees.Where(t => t.y == tree.y).Where(t => t.x < tree.x).Where(t => t.height >= tree.height).OrderBy(t => t.x).Last().x);
                }
                else 
                { 
                    tree.score *= tree.x; 
                }
                if (trees.Where(t => t.y == tree.y).Where(t => t.x > tree.x).Where(t => t.height >= tree.height).Any())
                {
                    tree.score *= Math.Abs(tree.x - trees.Where(t => t.y == tree.y).Where(t => t.x > tree.x).Where(t => t.height >= tree.height).OrderBy(t => t.x).First().x);
                }
                else 
                { 
                    tree.score *= input[0].Length - 1 - tree.x; 
                }
                if (trees.Where(t => t.x == tree.x).Where(t => t.y < tree.y).Where(t => t.height >= tree.height).Any())
                {
                    tree.score *= Math.Abs(tree.y - trees.Where(t => t.x == tree.x).Where(t => t.y < tree.y).Where(t => t.height >= tree.height).OrderBy(t => t.y).Last().y);
                }
                else 
                { 
                    tree.score *= tree.y; 
                }
                if (trees.Where(t => t.x == tree.x).Where(t => t.y > tree.y).Where(t => t.height >= tree.height).Any())
                {
                    tree.score *= Math.Abs(tree.y - trees.Where(t => t.x == tree.x).Where(t => t.y > tree.y).Where(t => t.height >= tree.height).OrderBy(t => t.y).First().y);
                }
                else 
                { 
                    tree.score *= input.Length - 1 - tree.y; 
                }
            }
            return trees.OrderBy(t => t.score).Last().score;
        }

        private static void SetupTrees(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                for (int j = 0; j < line.Length; j++)
                {
                    Tree tree = new();
                    tree.x = i;
                    tree.y = j;
                    tree.height = line[j] - 48;
                    trees.Add(tree);
                }
            }
        }
    }
    
    public class Tree
    {
        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        public bool visible = false;
        public int score = 1;
    }
}