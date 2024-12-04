using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_05
{
    //https://adventofcode.com/2023/day/05#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: If You Give A Seed A Fertilizer. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<double> Seeds, Dictionary<string, AlmanacMap> Almanac) input)
        {
            Log.Verbose("TODO, Write a soluton that runs in a reasonable amount of time");
        }

        public void SlowSolve((List<double> Seeds, Dictionary<string, AlmanacMap> Almanac) input)
        {
            var (seedRanges, almanac) = input;
            string[] order = ["soil", "fertilizer", "water", "light", "temperature", "humidity", "location"];

            List<(double start, double end, double result)> ranges = [];

            for (int i = 0; i < seedRanges.Count(); i += 2)
            {
                double start = seedRanges[i];
                double end = seedRanges[i] + seedRanges[i + 1];

                var lowestResultInRange = Double.MaxValue;

                for (double seed = start; seed < end; seed++)
                {
                    var (contained, containedresult) = ContainedInRanges(ranges, seed);
                    if (contained)
                    {
                        if (containedresult < lowestResultInRange)
                        {
                            lowestResultInRange = containedresult;
                        }
                        continue;
                    }

                    var tracker = seed;

                    foreach (var target in order)
                    {
                        tracker = almanac[target].Convert(tracker);
                    }

                    if (i == 70)
                    {
                        Console.WriteLine("wtf");
                    }

                    if (tracker < lowestResultInRange)
                    {
                        lowestResultInRange = tracker;
                    }
                }

                ranges.Add((start, end, lowestResultInRange));
            }

            var lowestLocationNumber = ranges.OrderBy(x => x.result).First().result;
            Log.Information("The lowest location number corrosponding to an initial seed number is {l}", lowestLocationNumber);
        }

        public (bool, double) ContainedInRanges(List<(double start, double end, double result)> ranges, double number)
        {
            foreach (var range in ranges)
            {
                if (number >= range.start && number < range.end)
                {
                    return (true, range.result);
                }
            }

            return (false, 0);
        }
    }
}