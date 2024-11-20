using System;

namespace LastByteStanding
{
    public class GameManager
    {
        private Player player;
        private Enemy aslab;

        public void StartGame()
        {
            Console.WriteLine("=== Welcome to Last Byte Standing ===");
            Console.Write("Enter your player name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName);
            aslab = new Enemy();

            MainMenu();
        }

        private void MainMenu()
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.WriteLine("\n== Main Menu ==");
                Console.WriteLine("1. Solve LeetCode Challenge");
                Console.WriteLine("2. Manage Farm");
                Console.WriteLine("3. Check Inventory");
                Console.WriteLine("4. End Day");
                Console.WriteLine("5. Exit Game");
                Console.Write("Choose an action: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SolveChallenge();
                        break;
                    case "2":
                        ManageFarm();
                        break;
                    case "3":
                        player.Inventory.ShowInventory();
                        break;
                    case "4":
                        EndDay();
                        break;
                    case "5":
                        gameRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        private void SolveChallenge()
        {
            Console.WriteLine("Solving LeetCode challenge...");
            Random random = new Random();
            bool success = random.Next(0, 2) == 1;

            if (success)
            {
                int reward = random.Next(10, 30);
                player.AddCoins(reward);
                Console.WriteLine($"Challenge completed! You earned {reward} coins.");
            }
            else
            {
                Console.WriteLine("Challenge failed!");
            }
        }

        private void ManageFarm()
        {
            Console.WriteLine("\n== Manage Farm ==");
            Console.WriteLine("1. Plant Crop");
            Console.WriteLine("2. Harvest Crop");
            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                if (player.Coins >= 10)
                {
                    player.DeductCoins(10);
                    player.Inventory.AddItem("Crop");
                    Console.WriteLine("You planted a crop.");
                }
                else
                {
                    Console.WriteLine("Not enough coins to plant!");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("You harvested a crop.");
                player.Inventory.AddItem("Harvested Crop");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private void EndDay()
        {
            Console.WriteLine("Ending the day...");
            Random random = new Random();
            bool completedChallenge = random.Next(0, 2) == 1;

            if (!completedChallenge)
            {
                Console.WriteLine("You failed to complete today's challenge!");
                aslab.DestroyInventory(player);
            }

            player.UseEnergy(20);
            if (player.Energy <= 0)
            {
                Console.WriteLine("You ran out of energy and lost a life!");
                // Handle losing a life logic here.
            }
        }
    }
}
