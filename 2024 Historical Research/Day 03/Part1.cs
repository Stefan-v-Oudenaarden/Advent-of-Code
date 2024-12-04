using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day_03
{
    //https://adventofcode.com/2024/day/03
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Mull It Over. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(string input)
        {
            var mulRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
            List<int> products = [];

            var results = mulRegex.Matches(input);
            foreach (Match result in results)
            {
                var left = int.Parse(result.Groups[1].Value);
                var right = int.Parse(result.Groups[2].Value);

                products.Add(left * right);
            }

            Log.Information("Found {count} multiplication with a total sum of {sum}.",
                products.Count,
                products.Sum());
        }

        public static string ParseInput(string filePath)
        {
            return string.Join("", File.ReadAllLines(filePath));
        }
    }
}