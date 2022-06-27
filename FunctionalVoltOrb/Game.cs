using System.Text;

internal class Game
{
    readonly List<Level> _levels = new();

    private Game(List<Level> levels)
    {
        _levels = levels;
    }

    public Level CurrentLevel => _levels.AsEnumerable().Reverse().FirstOrDefault(b => b.HasFlippedCards) ?? _levels[0];

    public override string ToString()
    {
        var sb = new StringBuilder();
        var totalScore = _levels.Sum(lvl => lvl.LevelScore);
        var currentLevelScore = CurrentLevel.LevelScore;

        sb.Append($"Total Score: {totalScore}, Score for this level: {currentLevelScore}\n\n");
        sb.Append(CurrentLevel);
        sb.Append('\n');
        return sb.ToString();
    }

    internal void ExecuteFlips(string flipsString)
    {
        var flips = flipsString.Split(" ");
        foreach (var flip in flips)
        {
            CurrentLevel.HasFlippedCards()
        }
    }

    static internal Game Load(string path)
    {
        var levels = new List<Level>();
        var currentLevel = new Level();
        levels.Add(currentLevel);
        var lines = File.ReadLines(path);
        foreach (var line in lines) Console.WriteLine(line);
        foreach (var line in lines)
        {
            if (line == "")
            {
                currentLevel = new Level();
                levels.Add(currentLevel);
            }
            else
            {
                currentLevel.AddLine(line);
            }

        }
        return new Game(levels);
    }
}