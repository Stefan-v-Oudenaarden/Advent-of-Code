using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Runtime.CompilerServices;

namespace Day_01
{
    //https://adventofcode.com/2025/day/01
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Secret Entrance. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(bool right, int amount)> input)
        {
            int dialPosition = 50;
            int dialAtZeroCount = 0;

            foreach (var item in input)
            {
                for (int i = 0; i < item.amount; i++)
                {
                    if (item.right)
                    {
                        dialPosition++;
                    }
                    else
                    {
                        dialPosition--;
                    }

                    if (dialPosition < 0)
                    {
                        dialPosition = 99;
                    }

                    if (dialPosition >= 100)
                    {
                        dialPosition -= 100;
                    }
                }

                if (dialPosition == 0)
                {
                    dialAtZeroCount++;
                }
            }

            Log.Information("After {inputcount} instructions the dial pointed at zero {count} times.", input.Count, dialAtZeroCount);
        }

        public static List<(bool right, int amount)> ParseInput(string filePath)
        {
            List<(bool right, int amount)> instructions = new();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                bool right = line[0] == 'R';
                int amount = int.Parse(line.Substring(1));

                instructions.Add((right, amount));
            }
            return instructions;
        }
    }
}