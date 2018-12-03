using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args.Length > 0 ? args[0] : "input.txt";
            var claims = File.ReadAllLines(file).Select(line => new Claim(line));

            int[,] fabric = new int[1000, 1000];

            foreach (var claim in claims)
            {
                claim.Fill(fabric);
            }

            int count = 0;
            var enumerator = fabric.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int value = (int)enumerator.Current;
                count += value > 1 ? 1 : 0;
            }
            Console.WriteLine($"Overlapping squares: {count}");
        }
    }

    struct Claim 
    {
        public int Id {get;}
        public int X {get;}
        public int Y {get;}
        public int Width {get;}
        public int Height {get;}

        private static Regex rx = new Regex(@"#(?<id>\d+)\s+@\s+(?<x>\d+),(?<y>\d+):\s+(?<width>\d+)x(?<height>\d+)");
        public Claim(string input)
        {
            var matches = rx.Matches(input);

            if (matches.Count != 1)
            {
                throw new InvalidOperationException(matches.ToString());
            }

            var match = matches[0];
            var groups = match.Groups;
            Id = int.Parse(groups["id"].Value);
            X = int.Parse(groups["x"].Value);
            Y = int.Parse(groups["y"].Value);
            Width = int.Parse(groups["width"].Value);
            Height = int.Parse(groups["height"].Value);
        }

        public void Fill(int[,] fabric)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    fabric[X + i, Y + j] += 1;
                }
            }
        }
    }
}
