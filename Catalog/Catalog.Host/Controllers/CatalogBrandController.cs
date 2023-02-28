using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly IBrandService _brandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        IBrandService brandService)
    {
        _logger = logger;
        _brandService = brandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateEntityNameRequest request)
    {
        var result = await _brandService.Add(request.Name);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveItemResponce), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveIdEntityRequest request)
    {
        var result = await _brandService.Remove(request.IdEntity);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateStatusItemResponce), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Udate(UpdateEntityNameRequest request)
    {
        var result = await _brandService.Update(request.Id, request.Name);
        return Ok(result);
    }
}