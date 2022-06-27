internal class Card
{
    public int Value { get; init; }

    public bool IsVoltOrb => Value == 0;

    public bool IsFlipped { get; private set; } = false;

    public void Flip() => IsFlipped = true;

    public void Reset() => IsFlipped = false;

    public override string ToString() => IsFlipped ? Value.ToString() : "?";
}