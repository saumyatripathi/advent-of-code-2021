class Puzzle2
{
    public async Task GetAnswerAsync()
    {
        var input = await File.ReadAllLinesAsync("./input/input-2.txt");
        var parsedInput = input.Select(t => t.Split(' ')).Select(t => (t[0], int.Parse(t[1])));

        var calc1 = parsedInput.Aggregate((0, 0), (curr, next) => next.Item1 switch
         {
             "forward" => (curr.Item1 + next.Item2, curr.Item2),
             "up" => (curr.Item1, curr.Item2 - next.Item2),
             "down" => (curr.Item1, curr.Item2 + next.Item2),
             _ => curr
         });

        System.Console.WriteLine(calc1.Item1 * calc1.Item2);


        var calc2 = parsedInput.Aggregate((0, 0, 0), (curr, next) => next.Item1 switch
         {
             "forward" => (curr.Item1 + next.Item2, curr.Item2 + curr.Item3 * next.Item2, curr.Item3),
             "up" => (curr.Item1, curr.Item2, curr.Item3 - next.Item2),
             "down" => (curr.Item1, curr.Item2, curr.Item3 + next.Item2),
             _ => curr
         });

        System.Console.WriteLine(calc2.Item1 * calc2.Item2);
    }
}