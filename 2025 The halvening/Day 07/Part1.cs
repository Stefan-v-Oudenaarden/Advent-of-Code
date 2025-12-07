using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_07
{
    //https://adventofcode.com/2025/day/07
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Laboratories. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            var splits = 0;
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Heigth - 1; y++)

                {
                    var cell = input[x, y];
                    if (cell == "|" || cell == "S")
                    {
                        var cellBelowBeam = input[x + 1, y];
                        if (cellBelowBeam == ".")
                        {
                            input[x + 1, y] = "|";
                        }
                        else if (cellBelowBeam == "^")
                        {
                            input[x + 1, y + 1] = "|";
                            input[x + 1, y - 1] = "|";
                            splits++;
                        }
                    }
                }
            }

            Log.Information("The manifold diagram splits the beam {splits} times.", splits);
        }

        public static TextGrid ParseInput(string filePath)
        {
            return new(Helpers.ReadStringsFile(filePath));
        }
    }
}