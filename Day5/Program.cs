using System.Diagnostics;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            //typ stoppur räknar tidsförlopp.
            var watch = Stopwatch.StartNew();

            var seeds = input[0].Split(' ').Skip(1).Select(x => long.Parse(x)).ToList();
            var maps = new List<List<(long from, long to, long adjustment)>>();
            List<(long from, long to, long adjustment)>? currentMap = null;
            foreach (var line in input.Skip(2))
            {
                if (line.EndsWith(':'))
                {
                    currentMap = new List<(long from, long to, long adjustment)>();
                    continue;
                }
                else if (line.Length == 0 && currentMap != null)
                {
                    maps.Add(currentMap!);
                    currentMap = null;
                    continue;
                }

                var nums = line.Split(' ').Select(x => long.Parse(x)).ToArray();
                currentMap!.Add((nums[1], nums[1] + nums[2] - 1, nums[0] - nums[1]));
            }
            if (currentMap != null)
                maps.Add(currentMap);

            // p1
            var part1 = long.MaxValue;
            foreach (var seed in seeds)
            {
                var value = seed;
                foreach (var map in maps)
                {
                    foreach (var item in map)
                    {
                        if (value >= item.from && value <= item.to)
                        {
                            value += item.adjustment;
                            break;
                        }
                    }
                }
                part1 = Math.Min(part1, value);
            }

            // p2
            var ranges = new List<(long from, long to)>();
            for (int i = 0; i < seeds.Count; i += 2)
                ranges.Add((from: seeds[i], to: seeds[i] + seeds[i + 1] - 1));

            foreach (var map in maps)
            {
                var orderedmap = map.OrderBy(x => x.from).ToList();

                var newranges = new List<(long from, long to)>();
                foreach (var r in ranges)
                {
                    var range = r;
                    foreach (var mapping in orderedmap)
                    {
                        if (range.from < mapping.from)
                        {
                            newranges.Add((range.from, Math.Min(range.to, mapping.from - 1)));
                            range.from = mapping.from;
                            if (range.from > range.to)
                                break;
                        }

                        if (range.from <= mapping.to)
                        {
                            newranges.Add((range.from + mapping.adjustment, Math.Min(range.to, mapping.to) + mapping.adjustment));
                            range.from = mapping.to + 1;
                            if (range.from > range.to)
                                break;
                        }
                    }
                    if (range.from <= range.to)
                        newranges.Add(range);
                }
                ranges = newranges;
            }
            var part2 = ranges.Min(r => r.from);

            watch.Stop();

            Console.WriteLine($"=== PART 1 === \n {part1}");
            Console.WriteLine($"=== PART 2 === \n {part2}");
            Console.WriteLine($"Took = {watch.ElapsedMilliseconds}ms");
        }
    }
}