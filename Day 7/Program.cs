﻿using System.Text.Json;

namespace AoC2022
{
    class Day_7
    {
        public static int TotalSpace = 70000000;
        public static int SpaceRequired = 30000000;
        public static int MinSpaceToFree = 0;
        public static Dictionary<string, Dictionary<string, int>> directoryTree = new();
        static void Log<T>(T obj) => Console.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));
        public static void Main()
        {

            var input = File.ReadAllLines("input.txt");
            SetupTree(input);
            Console.WriteLine($"Part 1: {Part1(input)}");// not 905276
            Console.WriteLine($"Part 2: {Part2(input)}");
        }
        
        public static int Part1(string[] input)
        {
            int total = 0;
            foreach(var directory in directoryTree)
            {
                var dirs = directoryTree
                    .Where(x => x.Key
                        .StartsWith(directory.Key)).ToList();
                var dirSize = dirs
                        .Sum(x => x.Value
                            .Sum(y => y.Value));
                if (directory.Key == "/") MinSpaceToFree = dirSize - (TotalSpace - SpaceRequired);
                if (dirSize <= 100000)
                {
                    total += dirSize;
                }
            }
            return total;
        }

        public static int Part2(string[] input)
        {
            var currentSelection = SpaceRequired;
            foreach (var directory in directoryTree)
            {
                var dirs = directoryTree
                    .Where(x => x.Key
                        .StartsWith(directory.Key)).ToList();
                var dirSize = dirs
                    .Sum(x => x.Value
                        .Sum(y => y.Value));
                if (dirSize >= MinSpaceToFree && dirSize < currentSelection)
                {
                    currentSelection = dirSize;
                    if (dirSize == MinSpaceToFree) return dirSize;
                }
            }
            return currentSelection;
        }

        private static void SetupTree(string[] input)
        {
            var currentDir = string.Empty;
            foreach(var item in input)
            {
                var itemParts = item.Split(' ');
                if (itemParts[0] == "$")
                {
                    if (itemParts[1] == "cd")
                    {
                        var dir = itemParts[2];
                        if (dir != "..") currentDir = currentDir.Length == 0 ? dir : $"{currentDir}{dir}/";
                        else currentDir = GetParent(currentDir);
                        if (!directoryTree.ContainsKey(currentDir)) directoryTree.Add(currentDir, new());
                    }
                }
                else if (itemParts[0] != "dir")
                {
                    directoryTree[currentDir].Add(itemParts[1], int.Parse(itemParts[0]));
                }
            }
        }

        private static string GetParent(string directory)
        {
            if (directory == "/") return directory;
            directory = directory.TrimEnd('/');
            return directory.Substring(0, directory.LastIndexOf('/') + 1);
        }
    }
}