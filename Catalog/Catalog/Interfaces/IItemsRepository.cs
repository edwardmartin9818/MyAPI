using Catalog.Models;

namespace Catalog.Interfaces
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid Id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAync(Guid guid);
    }
}