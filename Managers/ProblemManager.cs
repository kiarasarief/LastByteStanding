using System.Text.Json;

public class ProblemManager
{
    private List<Problem> _problems;

    public ProblemManager(string jsonFilePath)
    {
        LoadProblems(jsonFilePath);
    }

    private void LoadProblems(string jsonFilePath)
    {
        try
        {
            var jsonText = File.ReadAllText(jsonFilePath);
            _problems = JsonSerializer.Deserialize<List<Problem>>(jsonText);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: Questions.json file not found.");
            _problems = new List<Problem>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Error: Invalid JSON format in Questions.json.");
            _problems = new List<Problem>();
        }
    }

    public Problem GetProblemByLevel(int level)
    {
        return _problems.FirstOrDefault(p => p.Id == level);
    }
}