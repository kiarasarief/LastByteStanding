using System.Collections.Generic;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ExampleInput { get; set; }
    public string ExampleOutput { get; set; }
    public List<TestCase> TestCases { get; set; }
    public int RewardCoins { get; set; }
    public bool IsSolved { get; set; }

    public Question(int id, string title, string description, string exampleInput, string exampleOutput, List<TestCase> testCases, int rewardCoins)
    {
        Id = id;
        Title = title;
        Description = description;
        ExampleInput = exampleInput;
        ExampleOutput = exampleOutput;
        TestCases = testCases;
        RewardCoins = rewardCoins;
        IsSolved = false;
    }
}

public class TestCase
{
    public string Input { get; set; }
    public string ExpectedOutput { get; set; }

    public TestCase(string input, string expectedOutput)
    {
        Input = input;
        ExpectedOutput = expectedOutput;
    }
}
