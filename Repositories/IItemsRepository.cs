using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        //Implementing asynchronous methods

        //method declarations

        //get all items
         Task<IEnumerable<Item>> GetAllItemsAsync();

        //get a single item
        Task<Item> GetItemAsync(Guid id);

        //create an item
        Task CreateItemAsync(Item item);

        //update item
        Task UpdateItemAsync(Item item);

        //delete item
        Task DeleteItemAsync(Guid id);
    }

    /*
    ****Before Asychronously programming****
    // The changes made here affected all the other methods in Controller
        public interface IItemsRepository
    {
        //method declarations

        //get all items
        //IEnumerable<Item> GetItems();
         IEnumerable<Item> GetAllItems();

        //get a single item
        Item GetItem(Guid id);

        //create an item
        void CreateItem(Item item);

        //update item
        void UpdateItem(Item item);

        //delete item
        void DeleteItem(Guid id);
    }
    
    */
}