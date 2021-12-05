class Puzzle3
{
    public async Task GetAnswerAsync()
    {
        var input = await File.ReadAllLinesAsync("./input/input-3.txt");

        var parsed = input.Select(t => t.Select(x => char.GetNumericValue(x)));
        var gamma =
        string.Join("",
        parsed
            .SelectMany(t => t.Select((t, idx) => (t, idx)))
            .GroupBy(t =>
                t.idx,
                (g, c) => c
                    .GroupBy(x => x.t, (ig, ic) => (ig, ic.Count()))
                    .OrderByDescending(t => t.Item2)
                    .First().ig
            ));
        var epsilon = string.Join("", gamma.Select(t => t.Equals('0') ? '1' : '0'));

        var answer = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        System.Console.WriteLine(answer);

        var o2Value = string.Join("",
            Enumerable
            .Range(0, parsed.First().Count())
            .Aggregate(parsed, (prev, curr) =>
            {
                if (prev.Count() == 1) return prev;
                var valid = prev.Select(x => x.ElementAt(curr)).GroupBy(x => x, (x, c) => (x, c.Count())).OrderByDescending(x => x.Item2).ThenByDescending(x => x.x).First().x;
                return prev.Where(t => t.ElementAt(curr) == valid).ToList();
            })
            .First());

        var co2Value = string.Join("",
            Enumerable
            .Range(0, parsed.First().Count())
            .Aggregate(parsed, (prev, curr) =>
            {
                if (prev.Count() == 1) return prev;
                var valid = prev.Select(x => x.ElementAt(curr)).GroupBy(x => x, (x, c) => (x, c.Count())).OrderBy(x => x.Item2).ThenBy(x => x.x).First().x;
                return prev.Where(t => t.ElementAt(curr) == valid).ToList();
            })
            .First());

        var answer2 = Convert.ToInt32(o2Value, 2) * Convert.ToInt32(co2Value, 2);
        System.Console.WriteLine(answer2);
    }
}