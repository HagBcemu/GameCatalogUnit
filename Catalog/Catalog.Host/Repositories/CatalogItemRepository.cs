using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem> GetCatalogById(int catalogId)
    {
        var catalogItem = await _dbContext.CatalogItems
            .Where(i => i.Id == catalogId)
            .FirstOrDefaultAsync();

        if (catalogItem == null)
        {
            return new CatalogItem();
        }

        return catalogItem!;
    }

    public async Task<List<CatalogItem>> GetCatalogByBrandId(int brandId)
    {
        var catalogByBrand = await _dbContext.CatalogItems
           .Where(i => i.CatalogBrandId == brandId)
           .ToListAsync();

        if (catalogByBrand == null)
        {
            return new List<CatalogItem>();
        }

        return catalogByBrand!;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByCatalogByTypeAsync(int pageIndex, int pageSize, int idType)
    {
        var totalItems = await _dbContext.CatalogItems
           .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(i => i.CatalogTypeId == idType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogItem>> GetByCatalogByByBrandsAsync(int pageIndex, int pageSize, List<int> idBrands)
    {
        var totalItems = await _dbContext.CatalogItems
           .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(i => idBrands.Contains(i.CatalogBrandId))
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogItem>> GetByCatalogByTypesAsync(int pageIndex, int pageSize, List<int> idTypes)
    {
        var totalItems = await _dbContext.CatalogItems
           .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(i => idTypes.Contains(i.CatalogBrandId))
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<RemoveItemResponce> RemoveItemById(int idItem)
    {
        var removeItem = await _dbContext.CatalogItems
            .Where(i => i.Id == idItem).FirstOrDefaultAsync();
        if (removeItem == null)
        {
            return new RemoveItemResponce { StatusRemove = false, StatusRemoveString = "Item doesn't exist" };
        }

        _dbContext.Remove(removeItem);
        await _dbContext.SaveChangesAsync();

        return new RemoveItemResponce { StatusRemove = true, StatusRemoveString = "Item was remove" };
    }

    public async Task<UpdateStatusItemResponce> UpdateItem(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var updateItem = await _dbContext.CatalogItems
           .Where(i => i.Id == id).FirstOrDefaultAsync();
        if (updateItem == null)
        {
            return new UpdateStatusItemResponce { StatusUpdate = false, StatusUpdateString = "Item doesn't exist" };
        }

        updateItem.CatalogBrandId = catalogBrandId;
        updateItem.CatalogTypeId = catalogTypeId;
        updateItem.Description = description;
        updateItem.Name = name;
        updateItem.PictureFileName = pictureFileName;
        updateItem.Price = price;
        await _dbContext.SaveChangesAsync();

        return new UpdateStatusItemResponce { StatusUpdate = true, StatusUpdateString = "Item was updeted" };
    }
}