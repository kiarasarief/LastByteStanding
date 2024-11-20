public class GameManager
{
    private User _user;
    private ProblemManager _problemManager;
    private AsLabManager _asLabManager;
    private int _daysElapsed = 0;
    private bool _icellProblemSolvedToday = false;
    private AuthenticationManager _authManager;
    private PlayerProfile _currentPlayer;

    public GameManager(ProblemManager problemManager, AuthenticationManager authManager)
    {
        _problemManager = problemManager;
        _authManager = authManager;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    return;
            }
        }
    }

    private void Login()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        _currentPlayer = _authManager.Login(username, password);
        if (_currentPlayer != null)
        {
            RunGame();
        }
    }

    private void Register()
    {
        Console.Write("Choose a Username: ");
        string username = Console.ReadLine();
        Console.Write("Choose a Password: ");
        string password = Console.ReadLine();

        _currentPlayer = _authManager.Register(username, password);
        if (_currentPlayer != null)
        {
            RunGame();
        }
    }

    private void SaveGame()
    {
        if (_currentPlayer == null) return;

        // Update player's game state before saving
        _currentPlayer.GameState = _user;
        _currentPlayer.DaysElapsed = _daysElapsed;
        _currentPlayer.CurrentLevel = _user.CurrentLevel;

        // Reload profiles to ensure we have the latest data
        _authManager = new AuthenticationManager();

        // Find and update the current player's profile
        var existingProfile = _authManager._playerProfiles
            .FirstOrDefault(p => p.Username == _currentPlayer.Username);

        if (existingProfile != null)
        {
            existingProfile.GameState = _currentPlayer.GameState;
            existingProfile.DaysElapsed = _currentPlayer.DaysElapsed;
            existingProfile.CurrentLevel = _currentPlayer.CurrentLevel;
        }

        // Save all profiles
        _authManager.SaveProfiles();

        Console.WriteLine("Game saved successfully!");
    }

    private void VisitICell()
    {
        Problem currentProblem = _problemManager.GetProblemByLevel(_user.CurrentLevel);

        if (currentProblem == null)
        {
            Console.WriteLine("No problem found for the current level.");
            return;
        }

        Console.Clear();
        Console.WriteLine($"Level {currentProblem.Id}: {currentProblem.Title}");
        Console.WriteLine($"Description: {currentProblem.Description}");
        Console.WriteLine($"Example Input: {currentProblem.ExampleInput}");
        Console.WriteLine($"Example Output: {currentProblem.ExampleOutput}");

        Console.WriteLine("\nWrite your solution method. Use the method name 'Solution'.");
        Console.WriteLine("Example: public int[] TwoSum(int[] nums, int target) { ... }");
        Console.WriteLine("Type 'run' to test your solution.");

        StringBuilder codeBuilder = new StringBuilder();
        while (true)
        {
            string line = Console.ReadLine();
            if (line.ToLower() == "run")
            {
                bool allTestsPassed = true;
                foreach (var testCase in currentProblem.TestCases)
                {
                    var (success, output) = CodeCompiler.CompileAndRun(
                        codeBuilder.ToString(),
                        "Solution",
                        testCase.Input
                    );

                    if (!success)
                    {
                        Console.WriteLine("Compilation Error: " + output);
                        allTestsPassed = false;
                        break;
                    }

                    if (output.Trim() != testCase.ExpectedOutput.Trim())
                    {
                        Console.WriteLine($"Test Case Failed:");
                        Console.WriteLine($"Input: {testCase.Input}");
                        Console.WriteLine($"Expected: {testCase.ExpectedOutput}");
                        Console.WriteLine($"Your Output: {output}");
                        allTestsPassed = false;
                        break;
                    }
                }

                if (allTestsPassed)
                {
                    _icellProblemSolvedToday = true;
                    _user.Coins += currentProblem.RewardCoins;
                    _user.CurrentLevel++;
                    Console.WriteLine($"Congratulations! You earned {currentProblem.RewardCoins} coins!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Try again!");
                }
            }
            else
            {
                codeBuilder.AppendLine(line);
            }
        }
    }

    private void GameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("GAME OVER");
        Console.WriteLine($"You survived {_daysElapsed} days");
        Console.WriteLine($"Final Score: {_user.Coins} coins");
        Console.ResetColor();
    }

    private void DisplayMainMenu()
    {
        Console.WriteLine("\nChoose a location:");
        Console.WriteLine("1. I-Cell");
        Console.WriteLine("2. Kantek");
        Console.WriteLine("3. Farm");
        Console.WriteLine("4. Exit Game");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                VisitICell();
                break;
            case "4":
                Environment.Exit(0);
                break;
        }
    }
}