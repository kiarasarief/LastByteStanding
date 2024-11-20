using System;

namespace LastByteStanding
{
    public class Enemy
    {
        public string Name { get; } = "Aslab";

        public void DestroyInventory(Player player)
        {
            if (player.Inventory.Items.Count > 0)
            {
                string itemDestroyed = player.Inventory.RemoveRandomItem();
                Console.WriteLine($"{Name} destroyed your {itemDestroyed}!");
            }
            else
            {
                Console.WriteLine($"{Name} found nothing to destroy!");
            }
        }
    }
}
