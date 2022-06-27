using System.Text;

internal class Game
{
    private readonly List<Level> _levels = new();

    private Game(List<Level> levels)
    {
        _levels = levels;
    }

    public Level CurrentLevel
    {
        get
        {
            foreach (var level in _levels)
            {
                if (!level.HasFlippedCards ||
                    (level.HasFlippedCards && level.LevelScore != level.MaxScore)) return level;
            }
            throw new Exception("Game is over!");
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var totalScore = _levels.Sum(lvl => lvl.LevelScore);
        var currentLevelScore = CurrentLevel.LevelScore;
        var level = _levels.IndexOf(CurrentLevel) + 1;

        sb.Append($"Level {level}, Total Score: {totalScore}, Score for this level: {currentLevelScore}\n\n");
        sb.Append(CurrentLevel);
        sb.Append('\n');
        return sb.ToString();
    }

    internal void ExecuteFlips(string flipsString)
    {
        var flips = flipsString.Split(" ");
        var currentLevel = CurrentLevel;
        foreach (var flip in flips)
        {
            var changeLevel = ProcessFlip(currentLevel, flip);
            if (changeLevel) break;
        }
    }

    private bool ProcessFlip(Level currentLevel, string flip)
    {
        currentLevel.Flip(flip);
        if (currentLevel.IsVoltorbFlipped)
        {
            HandleVoltorb(currentLevel);
            return true;
        }
        else if (currentLevel.LevelScore == currentLevel.MaxScore)
        {
            Console.WriteLine("You have won this level!");
            return true;
        }
        else return false;
    }

    private void HandleVoltorb(Level currentLevel)
    {
        Console.WriteLine("Boom! You hit a VoltOrb!");
        currentLevel.Reset();
        _levels.AsEnumerable().Reverse().First(lvl => lvl.HasFlippedCards).Reset();
    }

    static internal Game Load(string path)
    {
        var levels = new List<Level>();
        var lines = File.ReadLines(path);
        ProcessLines(levels, lines);
        return new Game(levels);
    }

    private static void ProcessLines(List<Level> levels, IEnumerable<string> lines)
    {
        var currentLevel = new Level();
        levels.Add(currentLevel);
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
    }
}