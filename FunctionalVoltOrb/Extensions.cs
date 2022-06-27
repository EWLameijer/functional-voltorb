namespace LocalFunctionalVoltorb
{
    internal static class Extensions
    {
        public static int ScoreAll(this IEnumerable<Card> cards) =>
            cards.Any() ? cards.Aggregate(1, (a, b) => a * b.Value) : 0;

        public static int ScoreFlipped(this IEnumerable<Card> cards) =>
            cards.Where(c => c.IsFlipped).ScoreAll();
    }
}
