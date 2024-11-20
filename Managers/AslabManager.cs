public class AsLabManager
{
    private Random _random = new Random();
    private User _user;

    public AsLabManager(User user)
    {
        _user = user;
    }

    public void GenerateExtraTask()
    {
        if (_random.Next(2) == 0) // 50% chance of extra task
        {
            int extraCoins = _random.Next(10, 50);
            int challengeDifficulty = _random.Next(1, 5);

            Console.WriteLine("\n[ASLAB CHALLENGE]");
            Console.WriteLine($"Aslab offers you an extra challenge for {extraCoins} coins!");
            Console.WriteLine($"Challenge Difficulty: {challengeDifficulty}");
            Console.WriteLine("Do you want to accept? (yes/no)");

            string response = Console.ReadLine()?.ToLower();
            if (response == "yes")
            {
                // Simulate extra challenge (placeholder)
                Console.WriteLine("Challenge accepted!");
                _user.Coins += extraCoins;
            }
            else
            {
                Console.WriteLine("Challenge declined.");
            }
        }
    }

    public bool ShouldRuinFarm(bool icellProblemSolved, int daysElapsed)
    {
        return !icellProblemSolved && daysElapsed >= 1;
    }

    public void RuinFarm()
    {
        Console.WriteLine("\n[ASLAB ATTACK]");
        Console.WriteLine("Aslab has destroyed your farm for not solving the I-Cell challenge!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"
    /\___/\
   (  o o  )
   /   ^   \
  /  \___/  \
 /           \
|  |     |  |
|__|     |__|
   FARM RUINED!");
        Console.ResetColor();

        // Reduce user's resources as punishment
        _user.Coins = Math.Max(0, _user.Coins - 50);
        _user.Energy -= 20;
        _user.Lives--;
    }
}