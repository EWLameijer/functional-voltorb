using System.Text;

internal class Level
{
    readonly Dictionary<(int, int), Card> _board = new();

    readonly IEnumerable<int> _range = Enumerable.Range(1, 5);

    public bool HasFlippedCards => _board.Values.Any(c => c.IsFlipped);

    public int LevelScore => HasFlippedCards ? _board.Values.Where(c => c.IsFlipped).Aggregate(1, (a, b) => a * b.Value) : 0;

    public void Flip(string flipString)
    {
        if (flipString.Length == 2)
            _range.Any(c => c == flipString[0] - '0') && _range.Any(c => c == flipString[1] - '0'))
        {
            // -'0' is fine! Ignoring official C# method which returns a double for now.
        }
        else
        {
            Console.WriteLine("Erroneous location!");
        }
    }

    internal void AddLine(string line)
    {
        var currentLine = (_board.Count == 0) ? 1 : _board.Keys.Select(x => x.Item2).Max() + 1;
        Console.WriteLine($"currentLine is {currentLine}");
        for (int i = 0; i < line.Length; i++)
        {
            _board[(i + 1, currentLine)] = new Card { Value = line[i] - '0' };
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (int y in _range)
        {
            AddRowToDisplay(sb, y);
        }
        foreach (int x in _range)
        {
            sb.Append($"{ColumnScore(x),-6}");
        }
        return sb.ToString();
    }

    private void AddRowToDisplay(StringBuilder sb, int y)
    {
        foreach (int x in _range)
        {
            sb.Append(_board[(x, y)] + "     ");
        }
        sb.Append(" " + RowScore(y));
        sb.Append('\n');
    }

    private string RowScore(int y)
    {
        var cells = _range.Select(x => _board[(x, y)]);
        return Score(cells);
    }

    private string ColumnScore(int x)
    {
        var cells = _range.Select(y => _board[(x, y)]);
        return Score(cells);
    }

    private static string Score(IEnumerable<Card> cells)
    {
        var voltorbs = cells.Count(x => x.Value == 0);
        var score = (voltorbs == 5) ? 0 : cells.Where(x => x.Value != 0).Aggregate(1, (a, b) => a * b.Value);
        return $"V{voltorbs}/S{score}";
    }
}