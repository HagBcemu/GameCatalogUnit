using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ITypeService _typeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ITypeService brandService)
    {
        _logger = logger;
        _typeService = brandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateEntityNameRequest request)
    {
        var result = await _typeService.Add(request.Name);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveItemResponce), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveIdEntityRequest request)
    {
        var result = await _typeService.Remove(request.IdEntity);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateStatusItemResponce), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Udate(UpdateEntityNameRequest request)
    {
        var result = await _typeService.Update(request.Id, request.Name);
        return Ok(result);
    }
}