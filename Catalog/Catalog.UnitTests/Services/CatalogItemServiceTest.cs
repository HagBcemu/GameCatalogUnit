using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        var removeItemResponceSuccess = new RemoveItemResponce()
        {
            StatusRemove = true,
            StatusRemoveString = "Item was remove"
        };

        _catalogItemRepository.Setup(s => s.RemoveItemById(
            It.IsAny<int>())).ReturnsAsync(removeItemResponceSuccess);

        // act
        var result = await _catalogService.Remove(_testItem.Id);

        // assert
        result.Should().Be(removeItemResponceSuccess);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrange
        var removeItemResponceFailed = new RemoveItemResponce()
        {
            StatusRemove = false,
            StatusRemoveString = "Item doesn't exist"
        };

        _catalogItemRepository.Setup(s => s.RemoveItemById(
            It.IsAny<int>())).ReturnsAsync(removeItemResponceFailed);

        // act
        var result = await _catalogService.Remove(_testItem.Id);

        // assert
        result.Should().Be(removeItemResponceFailed);
    }

    [Fact]

    public async Task Update_Succes()
    {
        // arrange
        var updateItemSucces = new UpdateStatusItemResponce()
        {
            StatusUpdate = true,
            StatusUpdateString = "Item was updeted"
        };

        _catalogItemRepository.Setup(s => s.UpdateItem(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(updateItemSucces);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(updateItemSucces);
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

        _catalogItemRepository.Setup(s => s.UpdateItem(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(updateItemFailed);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(updateItemFailed);
    }
}