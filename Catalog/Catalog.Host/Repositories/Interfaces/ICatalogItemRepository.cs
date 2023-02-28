using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem> GetCatalogById(int catalogId);
    Task<List<CatalogItem>> GetCatalogByBrandId(int brandId);
    Task<PaginatedItems<CatalogItem>> GetByCatalogByTypeAsync(int pageIndex, int pageSize, int idType);
    Task<PaginatedItems<CatalogItem>> GetByCatalogByByBrandsAsync(int pageIndex, int pageSize, List<int> idBrands);
    Task<PaginatedItems<CatalogItem>> GetByCatalogByTypesAsync(int pageIndex, int pageSize, List<int> idTypes);
    Task<RemoveItemResponce> RemoveItemById(int idItem);
    Task<UpdateStatusItemResponce> UpdateItem(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}