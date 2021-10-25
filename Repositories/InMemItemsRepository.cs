using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository: IItemsRepository
    {
        //contains a list of items that will serve as initializers
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow}
        };

        //Implementing asynchronous methods

        //get all items
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await Task.FromResult(items);
        }

        //gets a single item with Id
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        //creates an item
        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        //updates an item
        public async Task UpdateItemAsync(Item item)
        {
            //we use the index of the item to update it
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
             await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
             await Task.CompletedTask;
        }
    }


    /*
    *****Before implementing asynchronous metpublic class InMemItemsRepository: IItemsRepository
    {
        //contains a list of items that will serve as initializers
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow}
        };

        //get all items
        public IEnumerable<Item> GetAllItemsAsync()
        {
            return items;
        }

        //gets a single item with Id
        public Item GetItemAsync(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        //creates an item
        public void CreateItemAsync(Item item)
        {
            items.Add(item);
        }

        //updates an item
        public void UpdateItemAsync(Item item)
        {
            //we use the index of the item to update it
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }hods

    
    */
}