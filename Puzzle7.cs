class Puzzle7
{
    public async Task GetAnswerAsync()
    {
        var input = await File.ReadAllLinesAsync("./input/input-7.txt");
        var parsed = input.First().Split(",").Select(x => int.Parse(x));

        int min = parsed.Min();
        int max = parsed.Max();

        var answer = Enumerable.Range(min, max - min + 1)
            .Select(x => parsed.Sum(p => (1 + Math.Abs(p - x)) * Math.Abs(p - x) / 2))
            .Min();
        System.Console.WriteLine(answer);
    }
}