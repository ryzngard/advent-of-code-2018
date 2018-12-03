using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args.Length > 0 ? args[0] : "input.txt";
            int frequency = 0;

            var lines = File.ReadAllLines(file);
            HashSet<int> frequencies = new HashSet<int>();

            bool waiting = true;
            while(waiting)
            {
                foreach (var line in lines)
                {
                    frequency += parseInt(line);

                    if (frequencies.Contains(frequency))
                    {
                        waiting = false;
                        break;
                    }

                    frequencies.Add(frequency);
                }
            }

            Console.WriteLine($"First frequency encountered twice : {frequency}");
        }

        static int parseInt(string line)
        {
            return int.Parse(line);
        }


    }
}
