using System;

class Program
{
    static void Main(string[] args)
    {
        string jsonPath = "Questions.json";
        var problemManager = new ProblemManager(jsonPath);
        var gameManager = new GameManager(problemManager);
        gameManager.Start();
    }
}