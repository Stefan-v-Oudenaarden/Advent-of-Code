using Advent;
using RegExtract;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day_06
{
    //https://adventofcode.com/2025/day/06#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Trash Compactor. Part Two."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<List<string>> numbers, List<string> operators) input)
        {
            var results = new List<double>();

            for (int i = 0; i < input.operators.Count; i++)
            {
                var sheetNumbers = input.numbers.Select(s => s[i]); //Picks numbers down columns
                var digitCount = sheetNumbers.Max(s => s.Length); //The digit count for numbers in this column
                var problemNumbers = new List<double>();

                //Transpose the numbers from normal to squid math
                for (int column = 0; column < digitCount; column++)
                {
                    var digits = sheetNumbers.Select(s => s[column]).Where(d => d > 0).ToList(); //Pick the digits down the column to construct the squid numbers
                    if (digits.Count != 0)
                    {
                        var n = string.Join("", digits).Replace("0", ""); //Strip the padding 0's
                        problemNumbers.Add(double.Parse(n));
                    }
                }

                //Finally do the math
                if (input.operators[i] == "+")
                {
                    results.Add(problemNumbers.Sum());
                }
                else
                {
                    results.Add(problemNumbers.Aggregate((a, x) => a * x));
                }
            }

            Log.Verbose("The total sum of all squind transposed math problems is {sum}.",
                    results.Sum());
        }

        public static (List<List<string>> numbers, List<string> operators) ParseInput(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var numbers = new List<List<string>>();
            var operators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Reverse().ToList();
            lines = [.. lines.Take(lines.Length - 1)];

            foreach (var line in lines)
            {
                var adjustedLine = line.Replace(" ", "0");
                var paddedNumbers = new List<string>();

                var paddedNumber = "";

                //Parse lines right to left, collecting digits into a number until we find an index thats whitespace in all lines
                //that marks the end of a number separating column in the input
                for (int i = adjustedLine.Length - 1; i >= 0; i--)
                {
                    var zeroInAll = lines.All(l => l[i] == ' ');
                    if (zeroInAll)
                    {
                        paddedNumbers.Add(paddedNumber.Reverse());
                        paddedNumber = "";
                    }
                    else
                    {
                        paddedNumber += adjustedLine[i];
                    }
                }

                paddedNumbers.Add(paddedNumber.Reverse());
                numbers.Add(paddedNumbers);
            }

            return new(numbers, operators);
        }
    }
}