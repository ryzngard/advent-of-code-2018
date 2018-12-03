using System;
using System.Collections.Generic;
using System.IO;

namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args.Length > 0 ? args[0] : "input.txt";
            var lines = File.ReadAllLines(file);
            var answer = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    var diff = new Diff(lines[i], lines[j]);
                    if (diff.Count == 1)
                    {
                        Console.WriteLine(diff.Merged);
                        return;
                    }
                }
            }

            throw new Exception("No solution found");
        }
    }

    struct Diff
    {
        public int Count { get; set; }
        public string Merged { get; }
        public Diff(string one, string two)
        {
            var list = new List<int>();
            string tmp = string.Empty;
            for (int i = 0; i < one.Length; i++)
            {
                if (one[i] == two[i])
                {
                    tmp += one[i];
                }
            }

            Merged = tmp;
            Count = one.Length - Merged.Length;
        }
    }
}
