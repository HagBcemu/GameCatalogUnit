using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface IBrandService
    {
        Task<int?> Add(string nameName);
        Task<RemoveItemResponce> Remove(int idItem);
        Task<UpdateStatusItemResponce> Update(int id, string name);
    }
}
