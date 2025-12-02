using Advent;
using Serilog;

namespace Day_02
{
    //https://adventofcode.com/2025/day/02
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Gift Shop. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(long start, long end)> input)
        {
            var invalidIds = new List<long>();

            foreach (var (start, end) in input)
            {
                for (long id = start; id <= end; id++)
                {
                    var idString = id.ToString();
                    var idLeft = idString[..(idString.Length / 2)];
                    var idRight = idString[(idString.Length / 2)..];
                    if (idLeft == idRight)
                    {
                        invalidIds.Add(id);
                    }
                }
            }

            var invalidIdSum = invalidIds.Sum(id => id);

            Log.Information("Sum of invalid id's found in all ranges is {sum}", invalidIdSum);
        }

        public static List<(long start, long end)> ParseInput(string filePath)
        {
            var input = File.ReadAllText(filePath);
            var pairs = input.Split(",");

            var ranges = new List<(long start, long end)>();
            foreach (var pair in pairs)
            {
                var ids = pair.Split('-');
                var startid = long.Parse(ids[0]);
                var endid = long.Parse(ids[1]);
                ranges.Add((startid, endid));
            }

            return ranges;
        }
    }
}