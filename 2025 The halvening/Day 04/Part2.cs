using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2025/day/04#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Printing Department. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var removedRolls = 0;
            var cycles = 0;
            int lastCycleRemovedRolls = 0;

            do
            {
                lastCycleRemovedRolls = removedRolls;
                cycles++;

                var removals = new List<(string value, int x, int y)>();
                foreach (var cell in input.FindString("@"))
                {
                    var adjacentToCell = input.AdjacentCellsCount(cell.x, cell.y, "@");

                    if (adjacentToCell < 4)
                    {
                        removals.Add(cell);
                    }
                }

                removedRolls += removals.Count;

                foreach (var (_, x, y) in removals)
                {
                    input[x, y] = ".";
                }
            } while (lastCycleRemovedRolls < removedRolls);

            Log.Information("After {cycles} rounds {removalCount} rolls can be removed by a forklift.", cycles, removedRolls);
        }
    }
}