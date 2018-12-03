using System;
using System.IO;
using System.Linq;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args.Length > 0 ? args[0] : "input.txt";

            int twice = 0;
            int thrice = 0;

            foreach (var line in File.ReadAllLines(file))
            {
                var groups = line.GroupBy(a => a).GroupBy(g => g.Count());
                twice += groups.Any(g => g.Key == 2) ? 1 : 0;
                thrice += groups.Any(g => g.Key == 3) ? 1 : 0;
            }

            Console.WriteLine($"{twice} * {thrice} = {twice*thrice}");
        }
    }
}
