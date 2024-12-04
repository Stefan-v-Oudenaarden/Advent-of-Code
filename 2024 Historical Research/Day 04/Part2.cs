using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2024/day/04#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Ceres Search. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var xMasSum = 0;

            foreach (var cell in input.FindString("A"))
            {
                var rightDiag = string.Concat(input.CellsAlongPath(cell.x - 1, cell.y - 1, TextGrid.DownRight, 2));
                var leftDiag = string.Concat(input.CellsAlongPath(cell.x - 1, cell.y + 1, TextGrid.DownLeft, 2));

                if ((rightDiag == "MAS" || rightDiag == "SAM")
                    && (leftDiag == "MAS" || leftDiag == "SAM"))
                {
                    xMasSum++;
                }
            }

            Log.Information("X-MAS apears a total of {sum} times.", xMasSum);
        }
    }
}