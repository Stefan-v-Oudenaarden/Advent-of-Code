using Advent;
using Serilog;
using System.Text;

namespace Day_02
{
    //https://adventofcode.com/2025/day/02#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Gift Shop. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(long start, long end)> input)
        {
            var invalidIds = new HashSet<long>();

            foreach (var (start, end) in input)
            {
                for (long id = start; id <= end; id++)
                {
                    var idString = id.ToString();
                    var idLength = idString.Length;

                    foreach (var div in (List<int>)[.. Enumerable.Range(2, idLength).Where(i => idLength % i == 0)])
                    {
                        var sequence = idString[..(idLength / div)];
                        var expandedSequence = new StringBuilder().Insert(0, sequence, div).ToString();

                        if (idString == expandedSequence)
                        {
                            invalidIds.Add(id);
                        }
                    }
                }
            }
            var invalidIdSum = invalidIds.Sum(id => id);
            Log.Information("Sum of invalid id's found in all ranges is {sum}", invalidIdSum);
        }
    }
}