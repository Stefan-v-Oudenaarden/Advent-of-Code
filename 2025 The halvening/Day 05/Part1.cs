using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_05
{
    //https://adventofcode.com/2025/day/05
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cafeteria. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<(double start, double end)> ranges, List<double> values) input)
        {
            var freshIngredients = new HashSet<double>();

            foreach (var ingredient in input.values)
            {
                foreach (var (start, end) in input.ranges)
                {
                    if (ingredient >= start && ingredient <= end)
                    {
                        freshIngredients.Add(ingredient);
                    }
                }
            }

            Log.Information("Out out {total} there are {fresh} fresh ingredients.",
                input.values.Count, freshIngredients.Count);
        }

        public static (List<(double start, double end)> ranges, List<double> values) ParseInput(string filePath)
        {
            var inputTextHalves = File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine);

            var ranges = new List<(double start, double end)>();
            foreach (var line in inputTextHalves[0].Split(Environment.NewLine))
            {
                var split = line.Split("-");

                var s = double.Parse(split[0]);
                var e = double.Parse(split[1]);
                ranges.Add((s, e));
            }

            var values = new List<double>();
            foreach (var line in inputTextHalves[1].Split(Environment.NewLine))
            {
                var value = double.Parse(line);
                values.Add(value);
            }

            return (ranges, values);
        }
    }
}