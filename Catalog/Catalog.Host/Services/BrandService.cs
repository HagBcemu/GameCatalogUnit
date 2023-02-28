using Catalog.Host.Data;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class BrandService : BaseDataService<ApplicationDbContext>, IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(
       IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
       ILogger<BaseDataService<ApplicationDbContext>> logger,
       IBrandRepository brandRepository)
       : base(dbContextWrapper, logger)
        {
            _brandRepository = brandRepository;
        }

        public Task<int?> Add(string name)
        {
            return ExecuteSafeAsync(() => _brandRepository.Add(name));
        }

        public Task<RemoveItemResponce> Remove(int idItem)
        {
            return ExecuteSafeAsync(() => _brandRepository.RemoveItemById(idItem));
        }

        public Task<UpdateStatusItemResponce> Update(int id, string name)
        {
            return ExecuteSafeAsync(() => _brandRepository.UpdateItem(id, name));
        }
    }
}
