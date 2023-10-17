using System.Collections.Generic;
using ReactInXamarin.Models;

namespace ReactInXamarin.Services
{
    public class ItemsService : IItemsService
    {
        public List<Item> GetItems(string listId)
        {
            return new List<Item>
            {
                new Item { Id = 1, ListId = 1, Title = "Item 1", Description = "Item 1 Description", ListName = "List 1" },
                new Item { Id = 2, ListId = 1, Title = "Item 2", Description = "Item 2 Description", ListName = "List 1" },
                new Item { Id = 3, ListId = 1, Title = "Item 3", Description = "Item 3 Description", ListName = "List 1" },
                new Item { Id = 4, ListId = 1, Title = "Item 4", Description = "Item 4 Description", ListName = "List 1" },
                new Item { Id = 1, ListId = 1, Title = "Item 1", Description = "Item 1 Description", ListName = "List 1" },
                new Item { Id = 2, ListId = 1, Title = "Item 2", Description = "Item 2 Description", ListName = "List 1" },
                new Item { Id = 3, ListId = 1, Title = "Item 3", Description = "Item 3 Description", ListName = "List 1" },
                new Item { Id = 4, ListId = 1, Title = "Item 4", Description = "Item 4 Description", ListName = "List 1" },
                new Item { Id = 1, ListId = 1, Title = "Item 1", Description = "Item 1 Description", ListName = "List 1" },
                new Item { Id = 2, ListId = 1, Title = "Item 2", Description = "Item 2 Description", ListName = "List 1" },
                new Item { Id = 3, ListId = 1, Title = "Item 3", Description = "Item 3 Description", ListName = "List 1" },
                new Item { Id = 4, ListId = 1, Title = "Item 4", Description = "Item 4 Description", ListName = "List 1" },
            };
        }
    }
}