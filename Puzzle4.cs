class Puzzle4
{
    public async Task GetAnswerAsync()
    {
        var input = await File.ReadAllLinesAsync("./input/input-4.txt");

        var draw = input.First().Split(",").Select(t => Convert.ToInt32(t));
        var boards = input.Skip(1).Select((t, idx) => (t, idx)).GroupBy(t => t.idx / 6, (g, c) => new
        {
            BoardNumber = g,
            Definition = c.Skip(1).Select(t =>
            {
                return Enumerable.Range(0, 5).Select(x => Convert.ToInt32(string.Join("", t.t.Skip(x * 3).Take(3))));
            })
        });

        var possibleWinsH = boards.SelectMany(t => t.Definition.Select(x => x), (board, sub) => new { board.BoardNumber, LineDefintion = sub });
        var possibleWinsV = boards.SelectMany(t => t.Definition, (m, s) => new { m.BoardNumber, Lines = s }).SelectMany(t => t.Lines.Select((x, idx) => new { Item = x, ItemIndex = idx }), (m, s) => new { m.BoardNumber, s.Item, s.ItemIndex }).GroupBy(t => new { t.BoardNumber, t.ItemIndex }, (g, c) => new { g.BoardNumber, LineDefintion = c.Select(x => x.Item) });

        var allPossibleWins = possibleWinsH.Concat(possibleWinsV).ToList();

        int endingDraw = -1;
        int winningBoardNumber = -1;
        var drawHash = new HashSet<int>();
        foreach (var drawNum in draw)
        {
            drawHash.Add(drawNum);

            var winningBoard = allPossibleWins.FirstOrDefault(t => t.LineDefintion.All(x => drawHash.Contains(x)));
            if (winningBoard is not null)
            {
                endingDraw = drawNum;
                winningBoardNumber = winningBoard.BoardNumber;
                break;
            }
        }

        if (endingDraw == -1 || winningBoardNumber == -1) throw new ArgumentException();

        var allWinningBoardNumbers = boards.First(t => t.BoardNumber == winningBoardNumber).Definition.SelectMany(t => t);
        var score = allWinningBoardNumbers.Where(t => !drawHash.Contains(t)).Sum();

        var answer = score * endingDraw;

        System.Console.WriteLine(answer);

        int endingDraw2 = -1;
        var drawHash2 = new HashSet<int>();
        var noWinnerBoards = new HashSet<int>(boards.Select(t => t.BoardNumber));
        int lastWinnerBoardNumber = -1;
        foreach (var drawNum in draw)
        {
            drawHash2.Add(drawNum);

            var winningBoards = allPossibleWins.Where(t => t.LineDefintion.All(x => drawHash2.Contains(x)));
            noWinnerBoards.RemoveWhere(t => winningBoards.Select(x => x.BoardNumber).Contains(t));
            if (noWinnerBoards.Count == 1)
            {
                lastWinnerBoardNumber = noWinnerBoards.First();
            }
            if (noWinnerBoards.Count == 0)
            {
                endingDraw2 = drawNum;
                break;
            }
        }

        if (noWinnerBoards.Count > 1) throw new ArgumentException();

        var allWinningBoardNumbers2 = boards.First(t => t.BoardNumber == lastWinnerBoardNumber).Definition.SelectMany(t => t);
        var score2 = allWinningBoardNumbers2.Where(t => !drawHash2.Contains(t)).Sum();

        var answer2 = score2 * endingDraw2;

        System.Console.WriteLine(answer2);
    }
}