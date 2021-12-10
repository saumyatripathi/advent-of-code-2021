class Puzzle8
{
    public async Task GetAnswerAsync()
    {

        var input = await File.ReadAllLinesAsync("./input/input-8.txt");

        var parsed = input.Select(x => x.Split('|')[1].Trim().Split(' '));
        var nums1478 = new HashSet<int>() { 2, 4, 3, 7 };
        var answer = parsed
            .SelectMany(x => x)
            .Count(x => nums1478.Contains(x.Length));

        System.Console.WriteLine(answer);
    }
}