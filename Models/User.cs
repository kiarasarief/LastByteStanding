using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class User
{
    public int Lives { get; set; } = 5;
    public int Energy { get; set; } = 100;
    public int Coins { get; set; } = 0;
    public int CurrentLevel { get; set; } = 1;

    public void DisplayStats()
    {
        Console.WriteLine($"Lives: {Lives} | Energy: {Energy} | Coins: {Coins} | Level: {CurrentLevel}");
    }
}}
