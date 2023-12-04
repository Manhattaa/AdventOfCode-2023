using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Day3
{
    internal class SolutionV2
    {
        internal static void SolvePartOne(List<string> input)
        {
            var numberRegex = new Regex(@"\d+");
            var specialSymbolsRegex = new Regex(@"[^.0-9]");

            var numbers = Parse(input, numberRegex);
            var specialSymbols = Parse(input, specialSymbolsRegex);

            var sum = numbers
              .Where(num => specialSymbols
                .Any(s => IsAdjascentTo(s, num)))
              .Select(item => item.ToInt)
              .Sum();

            Console.WriteLine("--== PART ONE ==--");
            Console.WriteLine("Sum: " + sum);
        }

        internal static void SolvePartTwo(List<string> input)
        {
            var gearsRegex = new Regex(@"\*");
            var numbersRegex = new Regex(@"\d+");

            var gears = Parse(input, gearsRegex);
            var numbers = Parse(input, numbersRegex);

            var sum = 0;
            foreach (var gear in gears)
            {
                var neighbours = numbers
                  .Where(num => IsAdjascentTo(num, gear))
                  .Select(num => num.ToInt)
                  .ToList();

                if (neighbours.Count != 2)
                {
                    continue;
                }

                sum += neighbours[0] * neighbours[1];
            }

            Console.WriteLine("--== PART TWO ==--");
            Console.WriteLine("Sum: " + sum);
        }

        private static IEnumerable<Item> Parse(List<string> rows, Regex regex)
        {
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                MatchCollection matches = regex.Matches(rows[rowIndex]);
                foreach (Match match in matches)
                {
                    yield return new Item(match.Value, rowIndex, match.Index);
                }
            }
        }

        // https://stackoverflow.com/a/3269471
        private static bool IsAdjascentTo(Item item1, Item item2)
        {
            return Math.Abs(item2.rowIndex - item1.rowIndex) <= 1
              && item1.colIndex <= item2.colIndex + item2.Text.Length
              && item2.colIndex <= item1.colIndex + item1.Text.Length;
        }

        private record Item(string Text, int rowIndex, int colIndex)
        {
            internal int ToInt => int.Parse(Text);
        }
    }
}