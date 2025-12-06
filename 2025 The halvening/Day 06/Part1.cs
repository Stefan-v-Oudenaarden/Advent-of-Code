using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_06
{
    //https://adventofcode.com/2025/day/06
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Trash Compactor. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<List<double>> numbers, List<string> operators) input)
        {
            var results = new List<double>();
            for (int i = 0; i < input.operators.Count; i++)
            {
                var numbersForProblem = input.numbers.Select(s => s[i]).ToList();
                var op = input.operators[i];

                if (input.operators[i] == "+")
                {
                    results.Add(numbersForProblem.Sum());
                }
                else
                {
                    results.Add(numbersForProblem.Aggregate((a, x) => a * x));
                }
            }

            Log.Verbose("The total sum of all math problems is {sum}.",
                        results.Sum());
        }

        public static (List<List<double>> numbers, List<string> operators) ParseInput(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var operators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            lines = [.. lines.Take(lines.Length - 1)];

            var numbers = new List<List<double>>();

            foreach (var line in lines)
            {
                var splitItems = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                var splitNumbers = splitItems.ConvertAll(s => double.Parse(s));
                numbers.Add(splitNumbers);
            }

            return (numbers, operators);
        }
    }
}