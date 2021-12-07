class Puzzle5
{
    public async Task GetAnswerAsync()
    {
        var input = await File.ReadAllLinesAsync("./input/input-5.txt");

        var parsed = input
            .Select(x => x.Split(" -> "))
            .Select(x => new { P1 = x[0].Split(","), P2 = x[1].Split(",") })
            .Select(x => new
            {
                P1 = new { X = int.Parse(x.P1[0]), Y = int.Parse(x.P1[1]) },
                P2 = new { X = int.Parse(x.P2[0]), Y = int.Parse(x.P2[1]) }
            });

        var answer = parsed
            .Where(x => x.P1.X == x.P2.X || x.P1.Y == x.P2.Y)
            .Select(x => CalculatePoints((x.P1.X, x.P1.Y), (x.P2.X, x.P2.Y)))
            .SelectMany(x => x)
            .GroupBy(x => x, (g, c) => new { P = g, Count = c.Count() })
            .Where(x => x.Count > 1)
            .Count();

        System.Console.WriteLine(answer);

        var answer2 = parsed
            .Select(x => CalculatePoints((x.P1.X, x.P1.Y), (x.P2.X, x.P2.Y)))
            .SelectMany(x => x)
            .GroupBy(x => x, (g, c) => new { P = g, Count = c.Count() })
            .Where(x => x.Count > 1)
            .Count();

        System.Console.WriteLine(answer2);
    }

    private IEnumerable<(int, int)> CalculatePoints((int x, int y) p1, (int x, int y) p2)
    {
        var xAdder = p1.x < p2.x ? 1 : p1.x == p2.x ? 0 : -1;
        var yAdder = p1.y < p2.y ? 1 : p1.y == p2.y ? 0 : -1;

        var range = (p1.x - p2.x != 0 ? Math.Abs(p1.x - p2.x) : Math.Abs(p1.y - p2.y)) + 1;

        var xRange = Enumerable.Range(0, range).Select(x => p1.x + xAdder * x);
        var yRange = Enumerable.Range(0, range).Select(x => p1.y + yAdder * x);

        return xRange.Zip(yRange).Select(x => (x.First, x.Second));
    }
}