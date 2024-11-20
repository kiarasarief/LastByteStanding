public class GameManager
{
    private User _user;
    private ProblemManager _problemManager;

    public GameManager(ProblemManager problemManager)
    {
        _user = new User();
        _problemManager = problemManager;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            _user.DisplayStats();
            DisplayMainMenu();
        }
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
}