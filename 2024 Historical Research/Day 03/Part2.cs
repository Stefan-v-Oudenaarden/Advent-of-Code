using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text.RegularExpressions;

namespace Day_03
{
    //https://adventofcode.com/2024/day/03#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Mull It Over. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(string input)
        {
            var mulRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");
            List<int> products = [];

            var doMult = true;

            foreach (Match result in mulRegex.Matches(input))
            {
                switch (result.Groups[0].Value.Substring(0, 3))
                {
                    case "do(":
                        doMult = true;
                        break;

                    case "don":
                        doMult = false;
                        break;

                    case "mul":
                        if (doMult)
                        {
                            var left = int.Parse(result.Groups[1].Value);
                            var right = int.Parse(result.Groups[2].Value);

                            products.Add(left * right);
                        }
                        break;
                }
            }

            Log.Information("Found {count} multiplication with a total sum of {sum}.",
                products.Count,
                products.Sum());
        }
    }
}