using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Problem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("exampleInput")]
    public string ExampleInput { get; set; }

    [JsonPropertyName("exampleOutput")]
    public string ExampleOutput { get; set; }

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; }

    [JsonPropertyName("rewardCoins")]
    public int RewardCoins { get; set; }
}

public class TestCase
{
    [JsonPropertyName("input")]
    public string Input { get; set; }

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; }
}