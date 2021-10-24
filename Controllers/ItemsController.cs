using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    //making class declaration as an API controller
    [ApiController]
    [Route("items")]
    public class ItemsController: ControllerBase
    {
        //*****using DI
        private readonly IItemsRepository repository; // instance of IItemsRepository class

        //private readonly IItemsRepository _repository; // instance of IItemsRepository class(not using this keyword)

        //using constructor injection through DI
        public ItemsController(IItemsRepository repository)
        {
            this.repository =  repository;

            //same as above without this keyword
            //_repository = repository;
        }

        //gets all items
        //GET /items
        [HttpGet]
        public IEnumerable<ItemDto> GetAllItems()
        {
            //using the Extension class
            var items = repository.GetAllItems().Select(item => item.AsDto());
            return items;
            
            /*
            //without using the Extensions class
            //convert items into Dtos(we can use automapper to take care of this part)
            var items = repository.GetItems().Select(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            });
            //In this above implementation, we will have to do this for every method that uses Items
            //Instead of this, we can create a one time Extensions class that implements this Dto list and use it instead
            return items;
            */
        }

        // public ActionResult<IEnumerable<Item>> GetItems()
        // {
        //     var items = repository.GetItems();
        //     return Ok(items);
        //     return items;
        // }

        //gets a single item with id
        // GET /items/{id}
        [HttpGet("{id}")] //specifying the route
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if(item == null)
            {
                return NotFound();
            }
            //return Ok(item); // does the same as above and the better syntax
            return item.AsDto();
        }

        //create items
        //POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id}, item.AsDto());
        }

        //update item
        //PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);

            return NoContent();
        }

        //update a specific entry
        //PATCH /items/id
        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(Guid id, JsonPatchDocument<Item> patchDoc)
        {
            var existingItem = repository.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(existingItem, ModelState);
            repository.UpdateItem(existingItem);
            return NoContent();
        }

        //delete item
        //DELETE /items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);
            if(existingItem is null)
            {
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }
    }


    /*
    //   ********Before using DI********
     public class ItemsController: ControllerBase
    {
        private readonly InMemItemsRepository repository; // instance of InMemItemsRepository class

        //currently not the best way to do things, use DI in future
        public ItemsController()
        {
            repository =  new InMemItemsRepository();
        }

        //gets all items
        //GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        // public ActionResult<IEnumerable<Item>> GetItems()
        // {
        //     var items = repository.GetItems();
        //     return Ok(items);
        //     return items;
        // }

        //gets a single item with id
        // GET /items/{id}
        [HttpGet("{id}")] //specifying the route
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if(item == null)
            {
                return NotFound();
            }
            //return Ok(item); // does the same as above and the better syntax
            return item;
        }


    }
    
    */
}