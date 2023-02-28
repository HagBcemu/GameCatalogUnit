using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TypeRepository> _logger;

        public TypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<TypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Type = name
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<RemoveItemResponce> RemoveItemById(int idItem)
        {
            var removeItem = await _dbContext.CatalogTypes
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
            var updateItem = await _dbContext.CatalogTypes
               .Where(i => i.Id == id).FirstOrDefaultAsync();
            if (updateItem == null)
            {
                return new UpdateStatusItemResponce { StatusUpdate = false, StatusUpdateString = "Item doesn't exist" };
            }

            updateItem.Type = name;
            await _dbContext.SaveChangesAsync();

            return new UpdateStatusItemResponce { StatusUpdate = true, StatusUpdateString = "Item was updeted" };
        }
    }
}
