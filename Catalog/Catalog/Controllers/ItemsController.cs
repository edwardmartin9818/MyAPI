using Catalog.Interfaces;
using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Catalog.Extension;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet(Name = "GetItems")]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            IEnumerable<Item> items = (await repository.GetItemsAsync());
            return items.Select(item => item.AsDto());
        }

        //GET /items/{guid}
        [HttpGet("/{guid}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid guid)
        {
            var item = await repository.GetItemAsync(guid);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto CreateItemDto)
        {
            if (CreateItemDto == null)
            {
                return BadRequest();
            }


            Item item = CreateItemDto.AsItem();
            await repository.CreateItemAsync(item);
            return CreatedAtRoute(new { id = item.Id }, item.AsDto());

            
        }

        //PUT /items/{id}
        [HttpPut("/{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item existingItem = await repository.GetItemAsync(id);
            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return Ok();
        } 

        //DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            if (await repository.GetItemAsync(id) == null)
            {
                return NotFound();
            }

            await repository.DeleteItemAync(id);
            return Ok();
        }
    }
}
