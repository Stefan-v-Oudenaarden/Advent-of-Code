using Serilog;
using Advent;

namespace Day_01
{
    //https://adventofcode.com/2025/day/01#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Secret Entrance. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(bool right, int amount)> input)
        {
            int dialPosition = 50;
            int dialAtZeroCount = 0; foreach (var item in input)
            {
                for (int i = 0; i < item.amount; i++)
                {
                    if (item.right)
                    {
                        dialPosition++;
                    }
                    else
                    {
                        dialPosition--;
                    }

                    if (dialPosition < 0)
                    {
                        dialPosition = 99;
                    }

                    if (dialPosition >= 100)
                    {
                        dialPosition -= 100;
                    }

                    if (dialPosition == 0 && i != item.amount - 1)
                    {
                        dialAtZeroCount++;
                    }
                }
                if (dialPosition == 0)
                {
                    dialAtZeroCount++;
                }
            }
            Log.Information("After {inputcount} instructions the dial passed zero {count} times.", input.Count, dialAtZeroCount);
        }
    }
}