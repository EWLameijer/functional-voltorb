

internal class Program
{
    private static void Main()
    {
        Game game = Game.Load(@"C:\Users\eric_\source\repos\LocalFunctionalVoltorb\LocalFunctionalVoltorb\example_boards.txt");
        do
        {
            Console.WriteLine(game);
            var flips = Console.ReadLine();
            game.ExecuteFlips(flips!);
        } while (true);
    }
}