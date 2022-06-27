using System.Text;
using LocalFunctionalVoltorb;

internal class Level
{
    private const int BoardSize = 5;

    private readonly Dictionary<(int, int), Card> _board = new();

    private readonly IEnumerable<int> _range = Enumerable.Range(1, BoardSize);

    public void Reset()
    {
        foreach (var value in _board.Values) value.Reset();
    }

    public bool IsVoltorbFlipped => _board.Values.Any(c => c.IsFlipped && c.IsVoltOrb);

    public bool HasFlippedCards => _board.Values.Any(c => c.IsFlipped);

    public int LevelScore => _board.Values.ScoreFlipped();

    public int MaxScore => _board.Values.Where(c => !c.IsVoltOrb).ScoreAll();

    public void Flip(string flipString)
    {
        if (flipString.Length == 2)
        {
            var xCoordinate = DigitFromChar(flipString[0]);
            var yCoordinate = DigitFromChar(flipString[1]);
            if (IsValidCoordinate(xCoordinate) && IsValidCoordinate(yCoordinate))
            {
                _board[(xCoordinate ?? 0, yCoordinate ?? 0)].Flip();
                return;
            }
        }
        Console.WriteLine("Erroneous location!");
    }

    private static int? DigitFromChar(char ch)
    {
        // -'0' is fine! Ignoring official C# method which returns a double for now.
        int rawDigit = ch - '0';
        return (rawDigit >= 0 && rawDigit < 10) ? rawDigit : null;
    }

    private static bool IsValidCoordinate(int? position)
    {
        return position != null && position >= 1 && position <= BoardSize;
    }

    internal void AddLine(string line)
    {
        var currentLine = (_board.Count == 0) ? 1 : _board.Keys.Select(x => x.Item2).Max() + 1;
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
        var voltorbs = cells.Count(x => x.IsVoltOrb);
        var score = cells.Where(x => !x.IsVoltOrb).ScoreAll();
        return $"V{voltorbs}/S{score}";
    }
}