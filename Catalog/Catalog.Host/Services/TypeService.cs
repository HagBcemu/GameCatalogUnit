using Catalog.Host.Data;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class TypeService : BaseDataService<ApplicationDbContext>, ITypeService
    {
        private readonly ITypeRepository _typeRepository;

        public TypeService(
       IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
       ILogger<BaseDataService<ApplicationDbContext>> logger,
       ITypeRepository typeRepository)
       : base(dbContextWrapper, logger)
        {
            _typeRepository = typeRepository;
        }

        public Task<int?> Add(string name)
        {
            return ExecuteSafeAsync(() => _typeRepository.Add(name));
        }

        public Task<RemoveItemResponce> Remove(int idItem)
        {
            return ExecuteSafeAsync(() => _typeRepository.RemoveItemById(idItem));
        }

        public Task<UpdateStatusItemResponce> Update(int id, string name)
        {
            return ExecuteSafeAsync(() => _typeRepository.UpdateItem(id, name));
        }
    }
}