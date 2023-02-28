using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services
{
    public class TypeServiceTest
    {
        private readonly ITypeService _typeService;

        private readonly Mock<ITypeRepository> _typeReposotory;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<TypeService>> _logger;

        private readonly CatalogType _testType = new CatalogType()
        {
            Id = 1,
            Type = "TypeTest"
        };

        public TypeServiceTest()
        {
            _typeReposotory = new Mock<ITypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<TypeService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(System.Threading.CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _typeService = new TypeService(_dbContextWrapper.Object, _logger.Object, _typeReposotory.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            int testResult = 1;

            _typeReposotory.Setup(s => s.Add(
           It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _typeService.Add(_testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            int? testResult = null;

            _typeReposotory.Setup(s => s.Add(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _typeService.Add(_testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Succes()
        {
            // arrange
            var updateStatusItemResponce = new UpdateStatusItemResponce()
            {
                StatusUpdate = true,
                StatusUpdateString = "Item was updeted"
            };

            _typeReposotory.Setup(s => s.UpdateItem(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(updateStatusItemResponce);

            // act
            var result = await _typeService.Update(_testType.Id, _testType.Type);

            // assert
            result.Should().Be(updateStatusItemResponce);
        }

        [Fact]
        public async Task Update_Failed()
        {
            // arrange
            var updateItemFailed = new UpdateStatusItemResponce()
            {
                StatusUpdate = false,
                StatusUpdateString = "Item wasn't updeted"
            };

            _typeReposotory.Setup(s => s.UpdateItem(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(updateItemFailed);

            // act
            var result = await _typeService.Update(_testType.Id, _testType.Type);

            // assert
            result.Should().Be(updateItemFailed);
        }
    }
}
