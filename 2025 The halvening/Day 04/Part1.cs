using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2025/day/04
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Printing Department. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var accesibleRolls = 0;

            foreach (var (_, x, y) in input.FindString("@"))
            {
                var adjacentToCell = input.AdjacentCellsCount(x, y, "@");

                if (adjacentToCell < 4)
                {
                    accesibleRolls++;
                }
            }

            Log.Information("In this configuration {count} rolls can be access by a forklift.", accesibleRolls);
        }

        public static TextGrid ParseInput(string filePath)
        {
            return new TextGrid(Helpers.ReadStringsFile(filePath));
        }
    }
}