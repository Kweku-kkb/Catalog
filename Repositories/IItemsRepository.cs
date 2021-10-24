using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
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
}