using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;
using DirectoryService.Application;

namespace DirectoryService.Web;

[ApiController]
[Route("locations")]
public class LocationController : ControllerBase
{
    private readonly CreateLocationUseCase _createLocationUseCase;
    private readonly UpdateLocationUseCase _updateLocationUseCase;
    
    public LocationController(CreateLocationUseCase createLocationUseCase, UpdateLocationUseCase updateLocationUseCase)
    {
        _createLocationUseCase = createLocationUseCase;
        _updateLocationUseCase = updateLocationUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDto createLocationDto, CancellationToken ct)
    {
        var result = await _createLocationUseCase.Create(createLocationDto, ct);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return NotFound();
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateLocationDto updateLocationDto, CancellationToken ct)
    {
         var result = await _updateLocationUseCase.Handle(id, updateLocationDto, ct);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
     public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
    }
}