using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_07
{
    //https://adventofcode.com/2025/day/07#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Laboratories. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var splits = 0;
            var quantumCounts = new double[input.Heigth, input.Width];
            var start = input.FindString("S").First();
            quantumCounts[start.x, start.y] = 1;

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Heigth - 1; y++)
                {
                    var quantumCellCount = quantumCounts[x, y];
                    if (quantumCellCount > 0)
                    {
                        var cellBelowBeam = input[x + 1, y];
                        if (cellBelowBeam == ".")
                        {
                            quantumCounts[x + 1, y] += quantumCellCount;
                        }
                        else if (cellBelowBeam == "^")
                        {
                            quantumCounts[x + 1, y + 1] += quantumCellCount;
                            quantumCounts[x + 1, y - 1] += quantumCellCount;
                            splits++;
                        }
                    }
                }
            }

            double sum = 0;
            for (int x = 0; x < input.Width; x++)
            {
                sum += quantumCounts[input.Heigth - 1, x];
            }

            Log.Information("The particle travels into {sum} different timelines.", sum);
        }
    }
}