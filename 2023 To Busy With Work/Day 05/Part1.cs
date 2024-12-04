using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Collections.Specialized;

namespace Day_05
{
    //https://adventofcode.com/2023/day/05
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: If You Give A Seed A Fertilizer. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<double> Seeds, Dictionary<string, AlmanacMap> Almanac) input)
        {
            var (seeds, almanac) = input;
            string[] order = ["soil", "fertilizer", "water", "light", "temperature", "humidity", "location"];

            List<double> locations = [];

            foreach (var seed in seeds)
            {
                var tracker = seed;
                //string trackedOrder = $"seed {tracker},";

                foreach (var target in order)
                {
                    tracker = almanac[target].Convert(tracker);
                    //trackedOrder += $"{target} {tracker},";
                }

                //Log.Verbose(trackedOrder);
                locations.Add(tracker);
            }

            var lowestLocationNumber = locations.OrderByDescending(x => x).LastOrDefault();
            Log.Information("The lowest location number corrosponding to an initial seed number is {l}", lowestLocationNumber);
        }

        public static (List<double> Seeds, Dictionary<string, AlmanacMap> Almanac) ParseInput(string filePath)
        {
            var inputString = File.ReadAllText(filePath);
            var inputSections = inputString.Split($"{Environment.NewLine}{Environment.NewLine}");

            var seeds = new List<double>();
            Dictionary<string, AlmanacMap> almanac = [];

            foreach (var section in inputSections)
            {
                if (section.StartsWith("seeds:"))
                {
                    seeds = Helpers.ReadAllDoublesInString(section);
                    continue;
                }

                var lines = section.Split(Environment.NewLine);
                var mapName = lines[0].Split(' ')[0];

                var nameElements = mapName.Split("-to-");
                var source = nameElements[0];
                var destination = nameElements[1];

                var AlmanacMap = new AlmanacMap
                {
                    Source = source,
                    Destination = destination
                };

                foreach (var line in lines.Skip(1))
                {
                    var mapEntry = new AlmanacMapEntry();

                    var numbers = Helpers.ReadAllDoublesInString(line);
                    mapEntry.DestinationRangeStart = numbers[0];
                    mapEntry.SourceRangeStart = numbers[1];
                    mapEntry.Range = numbers[2];

                    AlmanacMap.Entries.Add(mapEntry);
                }

                almanac.Add(destination, AlmanacMap);
            }

            return (seeds, almanac);
        }
    }

    public class AlmanacMap()
    {
        public string Source = "";
        public string Destination = "";

        public List<AlmanacMapEntry> Entries = [];

        public double Convert(double input)
        {
            foreach (var entry in Entries)
            {
                if (entry.isInSourceRange(input))
                {
                    return entry.Convert(input);
                }
            }
            return input;
        }
    }

    public class AlmanacMapEntry()
    {
        public double DestinationRangeStart = 0;
        public double SourceRangeStart = 0;
        public double Range = 0;

        public bool isInDestinationRange(double number)
        {
            return number >= DestinationRangeStart && number < DestinationRangeStart + Range;
        }

        public bool isInSourceRange(double number)
        {
            return number >= SourceRangeStart && number < SourceRangeStart + Range;
        }

        public double Convert(double input)
        {
            if (isInSourceRange(input))
            {
                var diff = DestinationRangeStart - SourceRangeStart;
                return input += diff;
            }

            return input;
        }
    }
}