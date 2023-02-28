using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);

    Task<CatalogResponce> GetCatalogById(int catalogId);

    Task<List<CatalogItemDto>> GetByBrand(int brandId);

    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(int pageSize, int pageIndex, int typeId);

    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByBrandsAsync(int pageSize, int pageIndex, List<int> brandsId);

    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypesAsync(int pageSize, int pageIndex, List<int> typesId);
}