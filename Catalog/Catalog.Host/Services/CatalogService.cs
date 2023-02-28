using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<CatalogResponce> GetCatalogById(int catalogId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetCatalogById(catalogId);

            return _mapper.Map<CatalogResponce>(result);
        });
    }

    public async Task<List<CatalogItemDto>> GetByBrand(int brandId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetCatalogByBrandId(brandId);

            return _mapper.Map<List<CatalogItemDto>>(result!);
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(int pageSize, int pageIndex, int typeId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByCatalogByTypeAsync(pageIndex, pageSize, typeId);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByBrandsAsync(int pageSize, int pageIndex, List<int> brandsId)
    {
        var result = await _catalogItemRepository.GetByCatalogByByBrandsAsync(pageIndex, pageSize, brandsId);
        return new PaginatedItemsResponse<CatalogItemDto>()
        {
            Count = result.TotalCount,
            Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypesAsync(int pageSize, int pageIndex, List<int> typesId)
    {
        var result = await _catalogItemRepository.GetByCatalogByTypesAsync(pageIndex, pageSize, typesId);
        return new PaginatedItemsResponse<CatalogItemDto>()
        {
            Count = result.TotalCount,
            Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}