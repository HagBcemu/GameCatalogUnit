using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BrandRepository> _logger;

        public BrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = name
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<RemoveItemResponce> RemoveItemById(int idItem)
        {
            var removeItem = await _dbContext.CatalogBrands
                .Where(i => i.Id == idItem).FirstOrDefaultAsync();
            if (removeItem == null)
            {
                return new RemoveItemResponce { StatusRemove = false, StatusRemoveString = "Item doesn't exist" };
            }

            _dbContext.Remove(removeItem);
            await _dbContext.SaveChangesAsync();

            return new RemoveItemResponce { StatusRemove = true, StatusRemoveString = "Item was remove" };
        }

        public async Task<UpdateStatusItemResponce> UpdateItem(int id, string name)
        {
            var updateItem = await _dbContext.CatalogBrands
               .Where(i => i.Id == id).FirstOrDefaultAsync();
            if (updateItem == null)
            {
                return new UpdateStatusItemResponce { StatusUpdate = false, StatusUpdateString = "Item doesn't exist" };
            }

            updateItem.Brand = name;
            await _dbContext.SaveChangesAsync();

            return new UpdateStatusItemResponce { StatusUpdate = true, StatusUpdateString = "Item was updeted" };
        }
    }
}
