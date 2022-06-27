

class Program
{
    static void Main()
    {
        Game game = Game.Load(@"D:\Development\C#\functional-voltorb\FunctionalVoltOrb\example_boards.txt");
        do
        {
            Console.WriteLine(game);
            var flips = Console.ReadLine();
            game.ExecuteFlips(flips);

        } while (true);
    }
}