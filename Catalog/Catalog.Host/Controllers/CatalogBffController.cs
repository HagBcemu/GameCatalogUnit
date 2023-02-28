using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(CatalogIdRequest request)
    {
        var result = await _catalogService.GetCatalogById(request.CatalogId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrand(CatalogGetByBrandRequest request)
    {
        var result = await _catalogService.GetByBrand(request.BrandId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(PaginatedItemsByTypeRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByTypeAsync(request.PageSize, request.PageIndex, request.TypeIdRequest);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands(PaginatedItemsByBrandsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByBrandsAsync(request.PageSize, request.PageIndex, request.Data.ToList());
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes(PaginatedItemsByBrandsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByTypesAsync(request.PageSize, request.PageIndex, request.Data.ToList());
        return Ok(result);
    }
}