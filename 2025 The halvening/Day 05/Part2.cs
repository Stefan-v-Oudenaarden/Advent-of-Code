using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_05
{
    //https://adventofcode.com/2025/day/05#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cafeteria. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<(double start, double end)> ranges, List<double> values) input)
        {
            var computedRanges = new List<(double start, double end)>();

            foreach (var inputRange in input.ranges.OrderBy(r => r.start))
            {
                var insertNewRange = true;
                for (var i = 0; i < computedRanges.Count; i++)
                {
                    var range = computedRanges[i];

                    switch (DetermineOverlap(inputRange, range))
                    {
                        case RangeOverlapType.FullyContained:
                            insertNewRange = false;
                            break;

                        case RangeOverlapType.OverlapsStart:
                            insertNewRange = false;
                            range.start = inputRange.start;
                            computedRanges[i] = range;
                            break;

                        case RangeOverlapType.OverlapsEnd:
                            insertNewRange = false;
                            range.end = inputRange.end;
                            computedRanges[i] = range;
                            break;

                        case RangeOverlapType.FullyLarger:
                            insertNewRange = false;
                            range.start = inputRange.start;
                            range.end = inputRange.end;
                            computedRanges[i] = range;
                            break;
                    }
                }

                if (insertNewRange)
                {
                    computedRanges.Add(inputRange);
                }
            }

            double freshIngredients = 0;

            foreach (var (start, end) in computedRanges)
            {
                freshIngredients += end - start + 1;
            }

            Log.Information("There are {sum} fresh ingredients contained in the ranges",
                freshIngredients);
        }

        private static RangeOverlapType DetermineOverlap((double start, double end) rangeA, (double start, double end) rangeB)
        {
            if (rangeA.start < rangeB.start
                && rangeA.end > rangeB.end)
            {
                return RangeOverlapType.FullyLarger;
            }
            else if (rangeA.start >= rangeB.start
                && rangeA.end <= rangeB.end)
            {
                return RangeOverlapType.FullyContained;
            }
            else if (rangeA.start < rangeB.start
                && rangeA.end >= rangeB.start
                && rangeA.end <= rangeB.end)
            {
                return RangeOverlapType.OverlapsStart;
            }
            else if (rangeA.start >= rangeB.start
                && rangeA.start <= rangeB.end
                && rangeA.end > rangeB.end)
            {
                return RangeOverlapType.OverlapsEnd;
            }

            return RangeOverlapType.NoOverlap;
        }

        public enum RangeOverlapType
        {
            FullyLarger,
            FullyContained,
            OverlapsStart,
            OverlapsEnd,
            NoOverlap
        }
    }
}