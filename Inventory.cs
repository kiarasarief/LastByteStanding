using System;
using System.Collections.Generic;

namespace LastByteStanding
{
    public class Inventory
    {
        public List<string> Items { get; private set; }

        public Inventory()
        {
            Items = new List<string>();
        }

        public void AddItem(string item)
        {
            Items.Add(item);
            Console.WriteLine($"Added {item} to inventory.");
        }

        public string RemoveRandomItem()
        {
            if (Items.Count == 0) return null;

            Random random = new Random();
            int index = random.Next(Items.Count);
            string item = Items[index];
            Items.RemoveAt(index);
            return item;
        }

        public void ShowInventory()
        {
            Console.WriteLine("\nInventory:");
            if (Items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }
    }
}
