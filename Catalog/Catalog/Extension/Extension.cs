using Catalog.Dtos;
using Catalog.Models;

namespace Catalog.Extension
{
    public static class Extension
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate,
            };
        }

        public static Item AsItem(this CreateItemDto item)
        {
            return new Item
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Price = item.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
