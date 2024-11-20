using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

public class GameManager
{
    private Player _player;
    private List<Question> _questions;

    public GameManager()
    {
        _player = new Player();
        LoadQuestions();
    }

    public void StartGame()
    {
        Console.WriteLine("Welcome to the game!");
        _player.DisplayStatus();
        ShowMenu();
    }

    private void LoadQuestions()
    {
        string jsonData = File.ReadAllText("Questions.json");
        _questions = JsonSerializer.Deserialize<List<Question>>(jsonData);
    }

    private void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nWhere would you like to go?");
            Console.WriteLine("1. I-Cell");
            Console.WriteLine("2. Exit");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                GoToICell();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private void GoToICell()
    {
        Console.WriteLine("\nWelcome to I-Cell!");
        foreach (var question in _questions)
        {
            if (!question.IsSolved)
            {
                PlayQuestion(question);
                return; // Only allow solving one question at a time.
            }
        }
        Console.WriteLine("You've solved all the questions in I-Cell!");
    }

    private void PlayQuestion(Question question)
    {
        Console.WriteLine("\n" + question.Title);
        Console.WriteLine(question.Description);
        Console.WriteLine($"Example Input: {question.ExampleInput}");
        Console.WriteLine($"Example Output: {question.ExampleOutput}");

        Console.WriteLine("\nPlease write your code and type 'run' when you're done:");

        string code = "";
        while (true)
        {
            string line = Console.ReadLine();
            if (line == "run")
                break;
            code += line + Environment.NewLine;
        }

        File.WriteAllText("PlayerCode.cs", code);
        CompileAndRunCode(question);
    }

    private void CompileAndRunCode(Question question)
    {
        string filePath = "PlayerCode.cs";
        string outputExe = "PlayerCode.exe";

        // Compile the code
        var compiler = new System.Diagnostics.Process();
        compiler.StartInfo.FileName = "csc";
        compiler.StartInfo.Arguments = $"{filePath} -out:{outputExe}";
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.StartInfo.RedirectStandardError = true;
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.CreateNoWindow = true;
        compiler.Start();

        string compilerOutput = compiler.StandardOutput.ReadToEnd();
        string compilerError = compiler.StandardError.ReadToEnd();
        compiler.WaitForExit();

        if (File.Exists(outputExe))
        {
            Console.WriteLine("Compilation successful!");
            RunCode(outputExe, question);
        }
        else
        {
            Console.WriteLine("Compilation failed. Error:");
            Console.WriteLine(compilerError);
        }
    }

    private void RunCode(string outputExe, Question question)
    {
        var runner = new System.Diagnostics.Process();
        runner.StartInfo.FileName = outputExe;
        runner.StartInfo.RedirectStandardOutput = true;
        runner.StartInfo.RedirectStandardError = true;
        runner.StartInfo.UseShellExecute = false;
        runner.StartInfo.CreateNoWindow = true;
        runner.Start();

        string output = runner.StandardOutput.ReadToEnd();
        runner.WaitForExit();

        Console.WriteLine("Execution Result:");
        Console.WriteLine(output);

        foreach (var testCase in question.TestCases)
        {
            if (output.Trim() == testCase.ExpectedOutput.Trim())
            {
                Console.WriteLine("Correct! You've earned coins.");
                _player.Coins += question.RewardCoins;
                question.IsSolved = true;
                break;
            }
            else
            {
                Console.WriteLine("Incorrect output. Try again.");
            }
        }

        _player.DisplayStatus();
    }
}
