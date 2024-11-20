using System;

namespace LastByteStanding
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; private set; } = 1;
        public int Energy { get; set; } = 100;
        public int Coins { get; set; } = 50;
        public Inventory Inventory { get; private set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new Inventory();
        }

        public void LevelUp()
        {
            Level++;
            Energy += 20;
            Console.WriteLine($"{Name} leveled up to Level {Level}!");
        }

        public void UseEnergy(int amount)
        {
            Energy -= amount;
            if (Energy < 0) Energy = 0;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
        }

        public void DeductCoins(int amount)
        {
            Coins -= amount;
            if (Coins < 0) Coins = 0;
        }
    }
}
