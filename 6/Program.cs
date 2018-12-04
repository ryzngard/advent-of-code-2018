using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _6
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args.Length > 0 ? args[0] : "input.txt";
            var claims = File.ReadAllLines(file).Select(line => new Claim(line)).ToArray();

            // for(int i = 0; i < claims.Length; i++)
            // {
            //     bool overlap = false;
            //     for (int j = i + 1; j < claims.Length; j++)
            //     {
            //         if (claims[i].Overlaps(claims[j]))
            //         {
            //             Console.WriteLine($"{claims[i]} overlaps {claims[j]}");
            //             overlap = true;
            //             break;
            //         }
            //     }

            //     if (!overlap)
            //     {
            //         Console.WriteLine(claims[i]);
            //         return;
            //     }
            // }

            List<Claim>[,] fabric = new List<Claim>[10000, 10000];

            foreach (var claim in claims)
            {
                claim.Fill(fabric);
            }

            HashSet<Claim> overlaps = new HashSet<Claim>();
            
            var iter = fabric.GetEnumerator();
            while (iter.MoveNext())
            {
                List<Claim> l = (List<Claim>)iter.Current;
                if (l == null || l.Count == 1) continue;

                foreach (var c in l)
                {
                    if (!overlaps.Contains(c))
                    {
                        overlaps.Add(c);
                    }
                }
            }

            var allExcept = claims.Except(overlaps).ToArray();
            Console.WriteLine(allExcept);
        }
    }

    public struct Claim 
    {
        public int Id {get;}
        public int X {get;}
        public int Y {get;}
        public int Width {get;}
        public int Height {get;}

        public int Left => X;
        public int Right => X + Width;
        public int Top => Y;
        public int Bottom => Y + Height;

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

        public bool Overlaps(Claim claim)
        {
            if (Width == 0 || Height == 0) return false;

            return overlap(Left, Right, claim.Left, claim.Right) &&
                overlap(Top, Bottom, claim.Top, claim.Bottom);
        }

        public override string ToString()
        {
            return $"{Id} : ([{Left}, {Right}], [{Top}, {Bottom}])";
        }

        private static bool overlap(int startOne, int endOne, int startTwo, int endTwo)
        {
            return between(startTwo, startOne, endTwo) || 
                between(startTwo, endOne, endTwo) || 
                between(startOne, startTwo, endOne) ||
                between(startOne, endTwo, endOne);
        }

        private static bool between(int start, int value, int end)
        {
            return (start <= value) && (value <= end);
        }

        internal void Fill(List<Claim>[,] fabric)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    fabric[X + i, Y + j] = fabric[X + i, Y + j] ?? new List<Claim>();
                    fabric[X + i, Y + j].Add(this);
                }
            }
        }
    }
}
