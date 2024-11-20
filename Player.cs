using System;

public class Player
{
    public int Health { get; set; }
    public int Energy { get; set; }
    public int Coins { get; set; }

    public Player()
    {
        Health = 5;  // Initial health
        Energy = 100;  // Initial energy
        Coins = 0;  // Initial coins
    }

    public void DisplayStatus()
    {
        Console.WriteLine($"Health: {Health} | Energy: {Energy} | Coins: {Coins}");
    }
}
