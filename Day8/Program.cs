using System.Diagnostics;
using System.IO;

namespace Day8
{

    //for future reference:
    //cur = current position
    //pointer = points to a value, at first set to 0
    //instr short for instruction - updates pointer and cur to reach answer1
    //IF instr reaches max (the size of the string) it resets pointer to 0.
    //pointer is constantly incremented (pointer++)

    internal class Program
    {
        static void Main(string[] args)
        {
            //probanswer = probableanswer - We check for the EXPECTED answers
            //answer = specific answers These values are then compared with probanswer

            //Testing out the "Run". 
            Run(@"..\..\..\input.txt", true, 2, 2);
            void Run(string inputfile, bool isTest, long probanswer1 = 0, long probanswer2 = 0)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                //our inputfile is stored in a string that we call "S"
                var S = File.ReadAllLines(inputfile).ToList();
                long answer1 = 0;
                long answer2 = 0;

                var instr = S[0];
                var map = new Dictionary<string, (string L, string R)>();
                foreach (var s in S.Skip(2))
                {
                    var a = s.Split(" = ");
                    var b = a[1].Substring(1, 3);
                    var c = a[1].Substring(6, 3);
                    map[a[0]] = (b, c);
                }
                //p1 - here we want to simulate the instructions through "instr" string until we hit the sequence "ZZZ"
                //answer1 = # of steps taken and then we compare it with probanswer. answer = more specific
                //we also need to take L and R into account based on the instructions in the input
                var cur = "AAA";
                var pointer = 0;
                if (probanswer1 > -1)
                {
                    while (cur != "ZZZ")
                    {
                        cur = instr[pointer] == 'L' ? map[cur].L : map[cur].R;
                        answer1++;
                        pointer++;
                        if (pointer == instr.Length) pointer = 0;
                    }
                }
                //p2 prepare to get sweaty
                //here we do the same as before but here we only use keys that end with an "A" then we calculate the # of steps needed to reach a key ending with a "Z"
                var cur2 = map.Keys.Where(k => k[2] == 'A').ToList();
                pointer = 0;
                answer2 = 1;

                foreach (var c2 in cur2)
                {
                    var c3 = c2;
                    long count = 0;
                    pointer = 0;
                    while (c3[2] != 'Z')
                    {
                        c3 = instr[pointer] == 'L' ? map[c3].L : map[c3].R;
                        pointer++;
                        count++;
                        if (pointer == instr.Length) pointer = 0;
                    }
                    // determine smallest common multpointerlier
                    var (g1, g2) = answer2 > count ? (answer2, count) : (count, answer2);
                    while (g2 != 0) (g1, g2) = (g2, g1 % g2);

                    answer2 = answer2 * (count / g1);
                }

                stopwatch.Stop();
                Console.WriteLine($"=== PART 1 === \n {answer1}");
                Console.WriteLine($"=== PART 2 === \n {answer2}");
                Console.WriteLine("Time in miliseconds: " + stopwatch.ElapsedMilliseconds.ToString());

            }
        }
    }
}
