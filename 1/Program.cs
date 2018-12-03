using System;
using System.IO;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args.Length > 0 ? args[0] : "input.txt";
            int frequency = 0;

            foreach (var line in File.ReadAllLines(file))
            {
                frequency += parseInt(line);
            }

            Console.WriteLine($"Final frequency is : {frequency}");
        }

        static int parseInt(string line)
        {
            return int.Parse(line);
        }
    }
}
