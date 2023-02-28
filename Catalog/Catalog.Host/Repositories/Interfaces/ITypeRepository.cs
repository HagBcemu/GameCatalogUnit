using Catalog.Host.Models.Response;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        Task<int?> Add(string name);
        Task<RemoveItemResponce> RemoveItemById(int idItem);
        Task<UpdateStatusItemResponce> UpdateItem(int id, string name);
    }
}
