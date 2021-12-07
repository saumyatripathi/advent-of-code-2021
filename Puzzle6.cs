class Puzzle6
{
    public async Task GetAnswerAsync()
    {
        // await OldSolution();
        var input = await File.ReadAllLinesAsync("./input/input-6.txt");
        // var input = new[] { "3,4,3,1,2" };
        var parsed = input.First().Split(",").Select(x => int.Parse(x));

        int numDays = 256;

        long totalFishes = parsed.Count();
        foreach (var fish in parsed)
        {
            int genCtr = 1;
            int genStartDay = fish + 1;
            var posCount = Enumerable.Range(0, (int)Math.Ceiling(numDays / 7m)).Select(x => (genStartDay + x * 7, 1)).Where(x => x.Item1 <= numDays).ToList();
            totalFishes += posCount.Sum(x => x.Item2);
            while (genStartDay <= numDays)
            {
                var prevGenStartDay = genStartDay;
                genStartDay = genStartDay + 9;
                posCount = Enumerable.Range(0, (int)Math.Ceiling(numDays / 7m - genCtr)).Select(x => (genStartDay + x * 7, posCount.Where(p => p.Item1 <= prevGenStartDay + x * 7).Sum(p => p.Item2))).Where(x => x.Item1 <= numDays).ToList();
                totalFishes += posCount.Sum(x => x.Item2);
                genCtr++;
            }
        }
        //var totalFishes = parsed.Select(x => GetChildrenCount(numDays, -x)).Sum() + parsed.Count();
        System.Console.WriteLine(totalFishes);
    }

    // private int GetChildrenCount(int days, int fishRepr)
    // {
    //     var childrenCount = (int)Math.Ceiling((days + fishRepr) / 7m);
    //     if (childrenCount <= 0) return 0;
    //     for (int i = 1; i < childrenCount + 1; i++)
    //     {
    //         childrenCount += GetChildrenCount(days, fishRepr - 2 - 7 * i);
    //     }
    //     return childrenCount;
    // }

    // private async Task OldSolution()
    // {
    //     // var input = await File.ReadAllLinesAsync("./input/input-6.txt");
    //     var input = new[] { "3,4,3,1,2" };
    //     var parsed = input.First().Split(",").Select(x => int.Parse(x));
    //     int numDays = 80;

    //     for (int i = 0; i < numDays; i++)
    //     {
    //         var newFishes = parsed.Where(x => x == 0).Select(x => 8);
    //         var currentFishesCounter = parsed.Select(x => x == 0 ? 6 : --x);

    //         parsed = currentFishesCounter.Concat(newFishes).ToList();
    //     }

    //     System.Console.WriteLine(parsed.Count());
    // }
}