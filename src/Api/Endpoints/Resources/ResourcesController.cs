using Chronos.Resources.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.Api.Endpoints.Resources;

public record CreateResourceRequest(string Name, int Capacity);

[ApiController]
[Route("api/resources")]
[Authorize]
public class ResourcesController : ControllerBase {
    private readonly ResourceService _resourceService;

    public ResourcesController(ResourceService resourceService) {
        _resourceService = resourceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceRequest request) {
        var resourceId = await _resourceService.CreateAsync(request.Name, request.Capacity);
        return CreatedAtAction(nameof(GetById), new { id = resourceId }, null);
    }
    
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id) {
        var resource = await _resourceService.GetByIdAsync(id);
        return resource is null ? NotFound() : Ok(resource);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() {
        var resources = await _resourceService.GetAllAsync();
        return Ok(resources);
    }
}