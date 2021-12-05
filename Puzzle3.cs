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

        var o2ColGroups = parsed
            .SelectMany(t => t.Select<double, (double value, int colIdx)>((x, idx) => (x, idx)))
            .GroupBy(t=>t.colIdx, (g,c)=> (g,c.Select(x=>(x.value))));

        // o2ColGroups.Aggregate(parsed,(prev,curr)=> prev.Where(t=>t.Where(x=>)))
        //curr.Item2.GroupBy(x=>x, (x,c)=> (x,c.Count())).OrderByDescending(x=>x.Item2).First().x
    }
}