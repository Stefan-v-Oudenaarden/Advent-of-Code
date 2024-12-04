using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Diagnostics.Tracing;

namespace Day_04
{
    //https://adventofcode.com/2024/day/04
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Ceres Search. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var xmasSum = 0;

            foreach (var cell in input.AllCells())
            {
                foreach (var direction in input.AdjacentDirections)
                {
                    var path = input.CellsAlongPath(cell.x, cell.y, direction, 3);
                    if (path.Count() == 4)
                    {
                        var word = string.Concat(path);

                        if (word == "XMAS")
                        {
                            xmasSum++;
                        }
                    }
                }
            }

            Log.Information("XMAS apears a total of {sum} times.", xmasSum);
        }

        public static TextGrid ParseInput(string filePath)
        {
            return new TextGrid(File.ReadAllLines(filePath).ToList());
        }
    }
}