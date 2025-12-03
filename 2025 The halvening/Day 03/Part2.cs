using Advent;
using Serilog;

namespace Day_03
{
    //https://adventofcode.com/2025/day/03#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Lobby. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<int>> input)
        {
            var maxJoltsPerBank = new List<double>();
            var maxJoltDigitCount = 12;

            foreach (var jolts in input)
            {
                var skips = jolts.Count - maxJoltDigitCount;
                double maxJolts = 0;
                var i = 0;
                var maxJoltDigits = new List<int>();

                //Input numbers are 15 digits and we must pick the 12 digits in sequence
                //that make the largest number. That means we can "skip" 3 numbers

                //Look at skips+1 numbers pick the best one. Subtract any skipping we did. Repeat untill out of skips.
                while (skips > 0 && maxJoltDigits.Count < maxJoltDigitCount)
                {
                    var options = jolts.Skip(i).Take(skips + 1).ToArray();

                    var pickedDigitIndex = IndexOfLargestEntry(options);

                    skips -= pickedDigitIndex;
                    i = i + pickedDigitIndex + 1;
                    maxJoltDigits.Add(options[pickedDigitIndex]);
                }

                //Now that we cannot skip just fill up with the remaining digits.
                while (maxJoltDigits.Count < maxJoltDigitCount)
                {
                    maxJoltDigits.Add(jolts[i]);
                    i++;
                }

                //Construct number out of digits. could also tostring and double.parse or any number of things
                maxJoltDigits.Reverse();
                double digitPlace = 1;
                foreach (var digit in maxJoltDigits)
                {
                    maxJolts += digit * digitPlace;
                    digitPlace *= 10;
                }

                maxJoltsPerBank.Add(maxJolts);
            }

            Log.Information("Jolts per bank sum {sum}.", maxJoltsPerBank.Sum());
        }

        public static int IndexOfLargestEntry(int[] input)
        {
            var index = 0;
            var max = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var n = input[i];
                if (n == 9)
                {
                    return i;
                }

                if (n > max)
                {
                    max = n;
                    index = i;
                }
            }

            return index;
        }
    }
}