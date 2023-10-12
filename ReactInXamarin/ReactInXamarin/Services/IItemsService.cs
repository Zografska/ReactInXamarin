using System.Collections.Generic;
using ReactInXamarin.Models;

namespace ReactInXamarin.Services
{
    public interface IItemsService
    { 
        List<Item> GetItems(string listId);
    }
}