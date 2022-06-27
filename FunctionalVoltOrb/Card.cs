internal class Card
{
    public int Value { get; init; }

    public bool IsFlipped { get; private set; } = false;

    public void Flip() => IsFlipped = true;

    public override string ToString() => IsFlipped ? Value.ToString() : "?";
}